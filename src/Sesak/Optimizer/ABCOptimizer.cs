using Sesak.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sesak.Optimizer
{
    //public delegate double CostFunction(double y);
    //public delegate void SetSimTimeoutFunction(double timeoutsec);
    public class OnIterationLoopMessageEventArgs : EventArgs
    {
        public OnIterationLoopMessageEventArgs(string msg)
        {
            message = msg;
            iteration = -1;
            maxiteration = -1;
        }
        public OnIterationLoopMessageEventArgs(string msg, int it, int maxit)
        {
            message = msg;
            iteration = it;
            maxiteration = maxit;
        }
        public string message;
        public int iteration;
        public int maxiteration;
    }
    public class ABCOptimizer
    {
        public static ABCOptimizer Instance;
        public static bool MultiObjective = false;

        //MODIFIED 20170906
        public static int Y_PARAMCOUNT = 2;
        const int POSITIONROUNDING = 4;

        const double DefaultSimTimeout = 120;
        public event EventHandler<OnIterationLoopMessageEventArgs> OnIterationLoopMessage;
        public event EventHandler<OnOptimizerResultEventArgs> OnOptimizerResult;
        public event EventHandler<OnOptimizerIterationUpdateEventArgs> OnOptimizerIterationLoopResult;
        public SimulationInstance[] SimulationInstances;
        //public CostFunction[] SimCostFunctions;

        //public SetSimTimeoutFunction SetSimTimeout;
        public const int nVar = 1;
        public double VarMin { get; set; }
        public double VarMax { get; set; }


        public int MaxIt { get; set; }
        public int nPop { get; set; }
        public int nOnlooker { get; set; }
        public double L { get; set; }
        public double a { get; set; }
        public double r { get; set; }


        public bool StopFlag { get; set; }

        public BeeStruct[] BestBees = null;
        public BeeStruct? CurrentBestBee = null;
        public ABCOptimizer()
        {
            Instance = this;
            //OPTIMIZER INITIAL PARAMETER, CURRENTLY HARDCODED FOR VARSIZE=1 (NOTE_20170223)
            VarMin = 7;
            VarMax = 18;
            MaxIt = 300;
            nPop = 50;
            L = 500;
            nOnlooker = nPop;
            //L = Math.Round(0.6 * nVar * nPop);
            a = 1;
            r = 1; //Tambah r

            
        }



        void workerRunSim(object data)
        {
            WorkerTaskData taskData = (WorkerTaskData)data;

            if (!double.IsNaN(taskData.SimulationTimeout))
                taskData.SimInstance.SetSimTimeout(taskData.SimulationTimeout);

            taskData.Result = new Tuple<double, double>(taskData.SimInstance.RunSimulationForOptimizer(taskData.Parameter), taskData.SimInstance.Discomfort);
            taskData.DNF = taskData.SimInstance.DNF;
            taskData.IsCompleted = true;
        }

        public void RunOptimizer(int workerCount)
        {
            SimulationInstance.GlobalSimulationTimeout = 600; //reset global simulation timeout to 10 minutes

            StopFlag = false;
            BeeStruct[] pop = new BeeStruct[nPop];
            //MODIFIED 20170906
            for (int n = 0; n < pop.Length; n++)
            {
                pop[n].Position = new double[Y_PARAMCOUNT]; //Predefine array size to parameter count
            }
            BeeStruct BestSol;

            BestSol.Cost = new Tuple<double, double>(double.PositiveInfinity, double.PositiveInfinity);
            BestSol.Position = new double[Y_PARAMCOUNT];

            for (int y = 0; y < Y_PARAMCOUNT; y++)
            {
                BestSol.Position[y] = double.NaN;
            }


            Random rnd = new Random();
            //double deltaVar = VarMax - VarMin;
            //List<Thread> workerThreads = new List<Thread>();
            List<WorkerTaskData> ActiveTaskDataList = new List<WorkerTaskData>();
            bool[] freeSimInstanceFlag = new bool[workerCount];

            for (int i = 0; i < freeSimInstanceFlag.Length; i++)
            {
                freeSimInstanceFlag[i] = true;
            }

            for (int i = 0; i < nPop; i++)
            {


                if (StopFlag)
                {
                    OnIterationLoopMessage.Invoke(this, new OnIterationLoopMessageEventArgs("Stopping..."));
                    break;
                }

                for (int y = 0; y < Y_PARAMCOUNT; y++)
                {
                    //pop[i].Position[y] = VarMin + rnd.NextDouble() * deltaVar;
                    pop[i].Position[y] = rnd.NextDouble();
                    pop[i].Position[y] = Math.Round(pop[i].Position[y], POSITIONROUNDING); //do rounding
                }
                int workerIndex = -1;


                //Find free worker
                for (int k = 0; k < freeSimInstanceFlag.Length; k++)
                {
                    if (freeSimInstanceFlag[k])
                    {
                        workerIndex = k;
                        break;
                    }
                }

                Thread th = new Thread(workerRunSim)
                {
                    IsBackground = true
                };
                //run worker task
                WorkerTaskData taskData = new WorkerTaskData(Y_PARAMCOUNT)
                {
                    //Parameter = pop[i].Position,
                    PopIndex = i,
                    SimInstance = SimulationInstances[workerIndex],
                    WorkerIndex = workerIndex,
                    WorkerThread = th,
                };
                Array.Copy(pop[i].Position, taskData.Parameter, Y_PARAMCOUNT);

                freeSimInstanceFlag[workerIndex] = false;
                //OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("[Worker " + workerIndex.ToString() + "] Iteration nPop Bees " + (i + 1).ToString(),0,MaxIt));
                ActiveTaskDataList.Add(taskData);
                th.Start(taskData);


                //CREATE INITIAL POPULATION
                do
                {
                    //Check worker
                    for (int k = ActiveTaskDataList.Count - 1; k >= 0; k--)
                    {
                        if (!ActiveTaskDataList[k].IsCompleted)
                            continue;


                        WorkerTaskData taskDataResult = ActiveTaskDataList[k];

                        pop[taskDataResult.PopIndex].Cost = new Tuple<double, double>(taskDataResult.Result.Item1, taskDataResult.Result.Item2);
                        OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("[Worker " + taskDataResult.WorkerIndex.ToString() + "] Iteration nPop Bees " + (taskDataResult.PopIndex + 1).ToString() + " completed (Pos:" + PrintArray(taskDataResult.Parameter) + ", Cost:" + taskDataResult.Result.ToString() + (taskDataResult.DNF ? " (DNF)" : "") + ")", 0, MaxIt));

                        /*
                        if (pop[taskDataResult.PopIndex].Cost <= BestSol.Cost)
                        {
                            BestSol = pop[taskDataResult.PopIndex];
                        }
                        
                        if ((MultiObjective && Pareto.Dominate(taskDataResult.Result, BestSol.Cost)) || (!MultiObjective && (BestSol.Cost.Item1 > taskDataResult.Result.Item1)))//if (BestSol.Cost > taskDataResult.Result)
                        {
                            BestSol.Cost = new Tuple<double, double>(taskDataResult.Result.Item1, taskDataResult.Result.Item2);
                            //taskDataResult.SimInstance.SimulationTimeOut = BestSol.Cost.Item1 * 2;
                            //BestSol.Position = taskDataResult.Parameter;
                            Array.Copy(taskDataResult.Parameter, BestSol.Position, Y_PARAMCOUNT);
                        }
                        */
                        //OnOptimizerIterationLoopResult?.Invoke(this, new OnOptimizerIterationUpdateEventArgs() { Bees = pop }); //TEST

                        freeSimInstanceFlag[taskDataResult.WorkerIndex] = true;
                        ActiveTaskDataList.RemoveAt(k);
                    }
                    if (ActiveTaskDataList.Count == 0)
                        break;
                    Thread.Sleep(1);
                }
                while (ActiveTaskDataList.Count >= workerCount || i == nPop - 1);

            }

            int[] C = new int[nPop];
            //double[] BestCost = new double[MaxIt];
            Tuple<double, double>[] BestCost = new Tuple<double, double>[MaxIt];
            BestBees = new BeeStruct[MaxIt];

            for (int i = 0; i < BestCost.Length; i++)
            {
                BestCost[i] = new Tuple<double, double>(double.NaN, double.NaN );
                BestBees[i] = new BeeStruct();
                BestBees[i].Cost = BestCost[i];
                BestBees[i].Position = new double[Y_PARAMCOUNT];

            }
            Random rndK = new Random();
            BeeStruct[] IttrBestBees = new BeeStruct[MaxIt];
            for (int i = 0; i < MaxIt; i++)
            {
                IttrBestBees[i].Cost = new Tuple<double, double>(double.PositiveInfinity, double.PositiveInfinity);
                IttrBestBees[i].Position = new double[Y_PARAMCOUNT];

            }
            for (int it = 0; it < MaxIt; it++)
            {

                if (StopFlag)
                {
                    OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Stopping..."));
                    break;
                }
                OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Iteration " + (it + 1).ToString(), it + 1, MaxIt));


                //% Recruited Bees %
                BeeStruct[] Xj = new BeeStruct[nPop]; //Tambah Xj

                for (int i = 0; i < nPop; i++)
                {
                    if (StopFlag)
                    {
                        OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Stopping..."));
                        break;
                    }

                    //% Evaluation - Cost Function %
                    //newbee.Cost = CostFunction(newbee.Position);


                    //% Choose K randomly, not equal to i %
                    List<int> KPool = new List<int>();
                    for (int n = 0; n < nPop; n++)
                    {
                        if (n == i)
                            continue;
                        KPool.Add(n);
                    }

                    int k = KPool[rndK.Next(0, KPool.Count - 1)];



                    //% Define Acceleration Coefficient %
                    double[] phi = new double[Y_PARAMCOUNT];


                    //% Calculate Bee New Position %
                    //newbee.Position = pop(i).Position + phi.* (pop(i).Position - pop(k).Position);
                    BeeStruct newBee = new BeeStruct();
                    newBee.Position = new double[Y_PARAMCOUNT];
                    for (int y = 0; y < Y_PARAMCOUNT; y++)
                    {
                        double yval;
                        do
                        {
                            phi[y] = a * (-1 + (rnd.NextDouble() * 2));
                            yval = pop[i].Position[y] + phi[y] * (pop[i].Position[y] - pop[k].Position[y]);

                        }
                        while (yval > 1 || yval < 0);
                        newBee.Position[y] = yval;
                        newBee.Position[y] = Math.Round(newBee.Position[y], POSITIONROUNDING); //do rounding
                    }



                    int workerIndex = -1;


                    //Find free worker
                    for (int j = 0; j < freeSimInstanceFlag.Length; j++)
                    {
                        if (freeSimInstanceFlag[j])
                        {
                            workerIndex = j;
                            break;
                        }
                    }

                    Thread th = new Thread(workerRunSim)
                    {
                        IsBackground = true
                    };
                    //run worker task
                    WorkerTaskData taskData = new WorkerTaskData(Y_PARAMCOUNT)
                    {
                        //Parameter = newBee.Position,
                        PopIndex = i,
                        SimInstance = SimulationInstances[workerIndex],
                        WorkerIndex = workerIndex,
                        WorkerThread = th,
                        SimulationTimeout = pop[i].Cost.Item1 * 1.2 //BestSol.Cost.Item1 * 1.2 //pop[i].Cost * 1.2
                    };
                    Array.Copy(newBee.Position, taskData.Parameter, Y_PARAMCOUNT);

                    freeSimInstanceFlag[workerIndex] = false;
                    //OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("[Worker " + workerIndex.ToString() + "] Iteration nPop Bees " + (i + 1).ToString()));
                    ActiveTaskDataList.Add(taskData);
                    th.Start(taskData);


                    do
                    {
                        //Check worker
                        for (int j = ActiveTaskDataList.Count - 1; j >= 0; j--)
                        {
                            if (!ActiveTaskDataList[j].IsCompleted)
                                continue;

                            WorkerTaskData taskDataResult = ActiveTaskDataList[j];
                            OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("[Worker " + taskDataResult.WorkerIndex.ToString() + "] Iteration nPop Bees " + (taskDataResult.PopIndex + 1).ToString() + " completed (Pos:" + PrintArray(taskDataResult.Parameter) + ", Cost:" + taskDataResult.Result.ToString() + (taskDataResult.DNF ? " (DNF)" : "") + ")"));
                            //% Comparison %

                            Xj[taskDataResult.PopIndex] = new BeeStruct(); //STORE NEWBEE INTO XJ
                            Xj[taskDataResult.PopIndex].Position = new double[Y_PARAMCOUNT];
                            Array.Copy(taskDataResult.Parameter, Xj[taskDataResult.PopIndex].Position, Y_PARAMCOUNT);
                            Xj[taskDataResult.PopIndex].Cost = new Tuple<double, double>(taskDataResult.Result.Item1, taskDataResult.Result.Item2);


                            if ((MultiObjective && Pareto.Dominate(taskDataResult.Result, pop[taskDataResult.PopIndex].Cost)) || (!MultiObjective && (taskDataResult.Result.Item1 <= pop[taskDataResult.PopIndex].Cost.Item1)))//if (taskDataResult.Result <= pop[taskDataResult.PopIndex].Cost)
                            {
                                pop[taskDataResult.PopIndex].Cost = new Tuple<double, double>(taskDataResult.Result.Item1, taskDataResult.Result.Item2);
                                Array.Copy(taskDataResult.Parameter, pop[taskDataResult.PopIndex].Position, Y_PARAMCOUNT);
                                //OnOptimizerIterationLoopResult?.Invoke(this, new OnOptimizerIterationUpdateEventArgs() { Bees = pop }); //TEST
                            }
                            else
                            {
                                C[taskDataResult.PopIndex] = C[taskDataResult.PopIndex] + 1;
                            }

                            if ((MultiObjective && Pareto.Dominate(taskDataResult.Result, IttrBestBees[it].Cost)) || (!MultiObjective && (IttrBestBees[it].Cost.Item1 > taskDataResult.Result.Item1)))//if (BestSol.Cost > taskDataResult.Result)
                            {
                                IttrBestBees[it].Cost = new Tuple<double, double>(taskDataResult.Result.Item1, taskDataResult.Result.Item2);
                                //BestSol.Position = taskDataResult.Parameter;
                                Array.Copy(taskDataResult.Parameter, IttrBestBees[it].Position, Y_PARAMCOUNT);
                            }

                            freeSimInstanceFlag[taskDataResult.WorkerIndex] = true;
                            ActiveTaskDataList.RemoveAt(j);
                        }
                        if (ActiveTaskDataList.Count == 0)
                            break;
                        Thread.Sleep(1);
                    }
                    while (ActiveTaskDataList.Count >= workerCount || i == nPop - 1);

                }
                if (StopFlag)
                {
                    OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Stopping..."));
                    break;
                }
                //% Calculate Fitness Values and Selection Probabilities %
                double[] P = new double[nPop];
                if (!MultiObjective)
                {

                    double[] F = new double[nPop];

                    double MeanCost = 0;
                    for (int i = 0; i < pop.Length; i++)
                    {
                        MeanCost += pop[i].Cost.Item1;
                    }
                    MeanCost /= pop.Length;

                    double SumF = 0;


                    /* Ubah Ref Line 129 ------
                    for (int i = 0; i < nPop; i++)
                    {
                        F[i] = Math.Exp(-pop[i].Cost / MeanCost);
                        SumF += F[i];

                    }
                    */

                    for (int i = 0; i < nPop; i++)
                    {
                        if (pop[i].Cost.Item1 >= 0)
                        {
                            F[i] = 1 / (1 + pop[i].Cost.Item1);
                        }
                        else
                        {
                            F[i] = 1 + Math.Abs(pop[i].Cost.Item1);
                        }
                    }
                    // -------

                    for (int i = 0; i < nPop; i++)
                    {
                        P[i] = F[i] / SumF;
                    }
                }
                else
                {
                    double[] F = new double[nPop];
                    double sumFit = 0;
                    for (int i = 0; i < nPop; i++)
                    {
                        int dominated = 0;
                        for (int j = 0; j < nPop; j++)
                        {
                            if (i == j)
                                continue;
                            if (Pareto.Dominate(pop[i].Cost, pop[j].Cost))
                                dominated++;
                        }
                        F[i] = (double)dominated / nPop;
                        sumFit += F[i];
                    }

                    for (int i = 0; i < nPop; i++)
                    {
                        P[i] = F[i] / sumFit;

                        if (P[i] >= 1)
                        {
                            P[i].ToString(); //TMP BREAKPOINT
                        }
                    }
                }

                //% Onlooker Bees %
                for (int m = 0; m < nOnlooker; m++)
                {
                    if (StopFlag)
                    {
                        OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Stopping..."));
                        break;
                    }



                    //% Select Source Site %
                    int i = RouletteWheelSelection(P);
                    //% Choose k randomly, not equal to i %
                    //Debugger.Log(0,"ONLOOKER", "ONLOOKER " + m.ToString() + "," + i.ToString());

                    List<int> KPool = new List<int>();
                    for (int n = 0; n < nPop; n++)
                    {
                        if (n == i)
                            continue;
                        KPool.Add(n);
                    }

                    int k = KPool[rndK.Next(0, KPool.Count - 1)];

                    //% Define Acceleration Coefficient %
                    double[] phi = new double[Y_PARAMCOUNT];

                    //TAMBAH REF LINE 159 - 186
                    double[][] dist = new double[Y_PARAMCOUNT][];
                    for (int y = 0; y < Y_PARAMCOUNT; y++)
                    {
                        dist[y] = new double[nPop];
                        for (int q = 0; q < nPop; q++)
                        {
                            dist[y][q] = Math.Abs(pop[i].Position[y] - Xj[q].Position[y]);
                        }
                    }


                    //Calculate Mean Euclidean Distance
                    /*
                    double sum_dist = dist.Sum();
                    double Mean_dm = sum_dist / (nPop - 1);
                    double mean_dm_r = r * Mean_dm;
                    List<double> Pos_new_list = new List<double>();


                    for (int q = 0; q < nPop; q++)
                    {
                        if (dist[q] <= mean_dm_r)
                        {
                            Pos_new_list.Add(dist[q]);
                        }
                    }

                    double Pos_new = Pos_new_list.Min();
                    */
                    double[] Pos_new = new double[Y_PARAMCOUNT];
                    for (int y = 0; y < Y_PARAMCOUNT; y++)
                    {
                        Pos_new[y] = dist[y].Min();
                    }
                    //--


                    //% New Bee Position %
                    BeeStruct newBee = new BeeStruct();
                    newBee.Position = new double[Y_PARAMCOUNT];
                    //newBee.Position = pop[i].Position + phi * (pop[i].Position - pop[k].Position);
                    for (int y = 0; y < Y_PARAMCOUNT; y++)
                    {
                        double yval;
                        do
                        {

                            phi[y] = a * (-1 + (rnd.NextDouble() * 2));
                            yval = Pos_new[y] + phi[y] * (pop[i].Position[y] - pop[k].Position[y]);
                        }
                        while (yval < 0 || yval > 1);
                        newBee.Position[y] = yval;
                        newBee.Position[y] = Math.Round(newBee.Position[y], POSITIONROUNDING); //do rounding
                    }

                    //% Evaluation %

                    int workerIndex = -1;

                    //Find free worker
                    for (int j = 0; j < freeSimInstanceFlag.Length; j++)
                    {
                        if (freeSimInstanceFlag[j])
                        {
                            workerIndex = j;
                            break;
                        }
                    }

                    Thread th = new Thread(workerRunSim)
                    {
                        IsBackground = true
                    };
                    //run worker task
                    WorkerTaskData taskData = new WorkerTaskData(Y_PARAMCOUNT)
                    {
                        //Parameter = newBee.Position,
                        PopIndex = i,
                        SimInstance = SimulationInstances[workerIndex],
                        WorkerIndex = workerIndex,
                        WorkerThread = th,
                        SimulationTimeout = pop[i].Cost.Item1 * 1.2//BestSol.Cost.Item1 * 1.2//BestCost[//pop[i].Cost * 1.2 //Optimize simulation timing, terminate if cost exceed
                    };
                    Array.Copy(newBee.Position, taskData.Parameter, Y_PARAMCOUNT);
                    freeSimInstanceFlag[workerIndex] = false;
                    //OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("[Worker " + workerIndex.ToString() + "] Iteration nOnlooker Bees " + (m + 1).ToString()));
                    ActiveTaskDataList.Add(taskData);
                    th.Start(taskData);



                    do
                    {
                        //Check worker
                        for (int t = ActiveTaskDataList.Count - 1; t >= 0; t--)
                        {
                            if (!ActiveTaskDataList[t].IsCompleted)
                                continue;

                            WorkerTaskData taskDataResult = ActiveTaskDataList[t];
                            OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("[Worker " + taskDataResult.WorkerIndex.ToString() + "] Iteration nOnlooker Bees " + (taskDataResult.PopIndex + 1).ToString() + " completed (Pos:" + PrintArray(taskDataResult.Parameter) + ", Cost:" + taskDataResult.Result.ToString() + (taskDataResult.DNF ? " (DNF)" : "") + ")"));

                            //pop[taskDataResult.PopIndex].Cost = taskDataResult.Result;
                            //% Comparison %
                            if ((MultiObjective && Pareto.Dominate(taskDataResult.Result, pop[taskDataResult.PopIndex].Cost)) || (!MultiObjective && (taskDataResult.Result.Item1 <= pop[taskDataResult.PopIndex].Cost.Item1)))//if (taskDataResult.Result <= pop[taskDataResult.PopIndex].Cost)
                            {
                                pop[taskDataResult.PopIndex].Cost = new Tuple<double, double>(taskDataResult.Result.Item1, taskDataResult.Result.Item2);
                                //pop[taskDataResult.PopIndex].Position = taskDataResult.Parameter;
                                Array.Copy(taskDataResult.Parameter, pop[taskDataResult.PopIndex].Position, Y_PARAMCOUNT);
                                //OnOptimizerIterationLoopResult?.Invoke(this, new OnOptimizerIterationUpdateEventArgs() { Bees = pop }); //TEST
                            }
                            else
                            {
                                C[taskDataResult.PopIndex] = C[taskDataResult.PopIndex] + 1;
                            }

                            //if (double.IsInfinity(taskDataResult.Result))
                            //taskDataResult.Result.ToString();

                            if ((MultiObjective && Pareto.Dominate(taskDataResult.Result, IttrBestBees[it].Cost)) || (!MultiObjective && (IttrBestBees[it].Cost.Item1 > taskDataResult.Result.Item1)))//if (BestSol.Cost > taskDataResult.Result)
                            {
                                IttrBestBees[it].Cost = new Tuple<double, double>(taskDataResult.Result.Item1, taskDataResult.Result.Item2);
                                //BestSol.Position = taskDataResult.Parameter;
                                Array.Copy(taskDataResult.Parameter, IttrBestBees[it].Position, Y_PARAMCOUNT);
                            }


                            freeSimInstanceFlag[taskDataResult.WorkerIndex] = true;
                            ActiveTaskDataList.RemoveAt(t);
                        }
                        if (ActiveTaskDataList.Count == 0)
                            break;
                        Thread.Sleep(1);
                    }
                    while (ActiveTaskDataList.Count >= workerCount || m == nOnlooker - 1);


                }
                if (StopFlag)
                {
                    OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Stopping..."));
                    break;
                }
                //% Scout Bees %
                for (int i = 0; i < nPop; i++)
                {
                    if (StopFlag)
                    {
                        OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Stopping..."));
                        break;
                    }

                    if (C[i] >= L)
                    {


                        for (int y = 0; y < Y_PARAMCOUNT; y++)
                        {
                            //pop[i].Position = VarMin + rnd.NextDouble() * deltaVar;
                            pop[i].Position[y] = rnd.NextDouble();
                            //pop[i].Position = Math.Round(pop[i].Position, 3); //do rounding
                            pop[i].Position[y] = Math.Round(pop[i].Position[y], POSITIONROUNDING); //do rounding

                        }


                        int workerIndex = -1;

                        //Find free worker
                        for (int k = 0; k < freeSimInstanceFlag.Length; k++)
                        {
                            if (freeSimInstanceFlag[k])
                            {
                                workerIndex = k;
                                break;
                            }
                        }

                        Thread th = new Thread(workerRunSim)
                        {
                            IsBackground = true
                        };
                        //run worker task
                        WorkerTaskData taskData = new WorkerTaskData(Y_PARAMCOUNT)
                        {
                            //Parameter = pop[i].Position,
                            PopIndex = i,
                            SimInstance = SimulationInstances[workerIndex],
                            WorkerIndex = workerIndex,
                            WorkerThread = th,
                            SimulationTimeout = -1// double.IsInfinity(BestSol.Cost) ? DefaultSimTimeout : BestSol.Cost * 1.5
                        };
                        Array.Copy(pop[i].Position, taskData.Parameter, Y_PARAMCOUNT);
                        freeSimInstanceFlag[workerIndex] = false;
                        ActiveTaskDataList.Add(taskData);
                        th.Start(taskData);





                        do
                        {
                            //Check worker
                            for (int k = ActiveTaskDataList.Count - 1; k >= 0; k--)
                            {
                                if (!ActiveTaskDataList[k].IsCompleted)
                                    continue;

                                WorkerTaskData taskDataResult = ActiveTaskDataList[k];
                                C[i] = 0;
                                pop[taskDataResult.PopIndex].Cost = new Tuple<double, double>(taskDataResult.Result.Item1, taskDataResult.Result.Item2);
                                freeSimInstanceFlag[taskDataResult.WorkerIndex] = true;
                                ActiveTaskDataList.RemoveAt(k);
                            }
                            if (ActiveTaskDataList.Count == 0)
                                break;
                            Thread.Sleep(1);
                        }
                        while (ActiveTaskDataList.Count >= workerCount || i == nPop - 1);
                    }

                }
                if (StopFlag)
                {
                    OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Stopping..."));
                    break;
                }
                /*
                //% Update Best Solution Ever Found %
                for (int i = 0; i < nPop; i++)
                {
                    if (pop[i].Cost <= BestSol.Cost)
                    {
                        BestSol = pop[i];
                    }
                }
                */

                OnOptimizerIterationLoopResult?.Invoke(this, new OnOptimizerIterationUpdateEventArgs() { Bees = IttrBestBees }); //TEST
                //% Store Best Cost Ever Found %
                if (Pareto.Dominate(IttrBestBees[it].Cost, BestSol.Cost))
                {
                    BestSol.Cost = new Tuple<double, double>(IttrBestBees[it].Cost.Item1, IttrBestBees[it].Cost.Item2);
                    Array.Copy(IttrBestBees[it].Position, BestSol.Position, Y_PARAMCOUNT);
                }

                BestCost[it] = BestSol.Cost;
                BestBees[it].Cost = BestSol.Cost;
                Array.Copy(BestSol.Position, BestBees[it].Position, Y_PARAMCOUNT);
                CurrentBestBee = BestBees[it];

                //% Display Iteration Information
                //OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Iteration " + (it + 1).ToString() + ": Ittr Best Sol { Cost = " + IttrBestBees[it].Cost.ToString() + " , Best Sol { Cost = " + BestSol.Cost.ToString() + " , Pos = " + PrintArray(BestSol.Position) + " }"));
                OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Iteration " + (it + 1).ToString() + ": Ittr Best Sol { Cost = " + IttrBestBees[it].Cost.ToString() + " , Pos = " + PrintArray(IttrBestBees[it].Position) + " } , Best Sol { Cost = " + BestSol.Cost.ToString() + " , Pos = " + PrintArray(BestSol.Position) + " }"));
                //disp(['Iteration ' num2str(it) ': Best Cost = ' num2str(BestCost(it))]);

                //OnOptimizerIterationLoopResult?.Invoke(this, new OnOptimizerIterationUpdateEventArgs() { Bees = pop });
            }

            List<Tuple<double, double>> BestCostList = new List<Tuple<double, double>>();
            List<BeeStruct> BestBeeList = new List<BeeStruct>();

            for (int i = 0; i < BestCost.Length; i++)
            {
                if (double.IsNaN(BestCost[i].Item1) || double.IsNaN(BestCost[i].Item2))
                    break;
                BestCostList.Add(BestCost[i]);

                BestBeeList.Add(BestBees[i]);
            }

            //Show result
            OnIterationLoopMessage?.Invoke(this, new OnIterationLoopMessageEventArgs("Done!"));
            OnOptimizerResult?.Invoke(this, new OnOptimizerResultEventArgs() { BestCostResult = BestCostList.ToArray(), BestBeeResult = BestBeeList.ToArray() });

        }
        string PrintArray(double[] a)
        {
            string s = "{";
            for (int i = 0; i < a.Length; i++)
            {
                if (i != 0)
                    s += ", ";
                s += a[i].ToString();
            }
            s += "}";

            return s;
        }

        // if generated position outside boundary // 

        // buang part ni tak diperlukan, just untuk reinitialized kalau terkeluar dari range // 
        //if (pop[i].Position <= (VarMin + SizeDoor / 2))
        //{
        //    pop[i].Position = (VarMin + SizeDoor / 2) - pop[i].Position;
        //    pop[i].Position = (VarMin + SizeDoor / 2) + pop[i].Position;
        //}

        //else if (pop[i].Position >= (VarMax - SizeDoor / 2))
        //{
        //    pop[i].Position = pop[i].Position - (VarMax - SizeDoor / 2);
        //    pop[i].Position = (VarMax - SizeDoor / 2) - pop[i].Position; 
        //}

        /*
        public void RunOptimizer()
        {
            StopFlag = false;
            BeeStruct[] pop = new BeeStruct[nPop];
            BeeStruct BestSol;
            BestSol.Cost = double.PositiveInfinity;
            BestSol.Position = double.NaN;

            Random rnd = new Random();
            double deltaVar = VarMax - VarMin;

            for (int i = 0; i < nPop; i++)
            {
                OnIterationLoopMessage?.Invoke(this, "Iteration for nPop " + (i + 1).ToString());

                pop[i].Position = VarMin + rnd.NextDouble() * deltaVar;
                pop[i].Cost = SimCostFunction(pop[i].Position);
                if (pop[i].Cost <= BestSol.Cost)
                {
                    BestSol = pop[i];
                }

            }

            int[] C = new int[nPop];
            double[] BestCost = new double[MaxIt];
            for (int i = 0; i < BestCost.Length; i++)
            {
                BestCost[i] = double.NaN;
            }
            Random rndK = new Random();
            for (int it = 0; it < MaxIt; it++)
            {
                if (StopFlag)
                {
                    OnIterationLoopMessage?.Invoke(this, "Stopping...");
                    break;
                }
                OnIterationLoopMessage?.Invoke(this, "Max iteration " + (it + 1).ToString());
                //% Recruited Bees %
                for (int i = 0; i < nPop; i++)
                {
                    if (StopFlag)
                    {
                        OnIterationLoopMessage?.Invoke(this, "Stopping...");
                        break;
                    }

                    OnIterationLoopMessage?.Invoke(this, "iteration nPop Bees " + (i + 1).ToString());


                    //% Choose K randomly, not equal to i %
                    List<int> KPool = new List<int>();
                    for (int n = 0; n < nPop; n++)
                    {
                        if (n == i)
                            continue;
                        KPool.Add(n);
                    }

                    int k = KPool[rndK.Next(0, KPool.Count - 1)];



                    //% Define Acceleration Coefficient %
                    double phi = a * (-1 + (rnd.NextDouble() * 2));


                    //% Calculate Bee New Position %
                    //newbee.Position = pop(i).Position + phi.* (pop(i).Position - pop(k).Position);
                    BeeStruct newBee;
                    newBee.Position = pop[i].Position + phi * (pop[i].Position - pop[k].Position);


                    //% Evaluation - Cost Function %
                    //newbee.Cost = CostFunction(newbee.Position);

                    //
                    SetSimTimeout(pop[i].Cost * 1.2); //Optimize simulation timing, terminate if cost exceed
                    newBee.Cost = SimCostFunction(newBee.Position);


                    //% Comparison %
                    if (newBee.Cost <= pop[i].Cost)
                    {
                        pop[i] = newBee;
                    }
                    else
                    {
                        C[i] = C[i] + 1;
                    }
                }
                if (StopFlag)
                {
                    OnIterationLoopMessage?.Invoke(this, "Stopping...");
                    break;
                }
                //% Calculate Fitness Values and Selection Probabilities %
                double[] F = new double[nPop];
                double MeanCost = 0;
                for (int i = 0; i < pop.Length; i++)
                {
                    MeanCost += pop[i].Cost;
                }
                MeanCost /= pop.Length;

                double SumF = 0;
                double[] P = new double[nPop];
                for (int i = 0; i < nPop; i++)
                {
                    F[i] = Math.Exp(-pop[i].Cost / MeanCost);
                    SumF += F[i];

                }
                for (int i = 0; i < nPop; i++)
                {
                    P[i] = F[i] / SumF;
                }


                //% Onlooker Bees %
                for (int m = 0; m < nOnlooker; m++)
                {
                    if (StopFlag)
                    {
                        OnIterationLoopMessage?.Invoke(this, "Stopping...");
                        break;
                    }
                    OnIterationLoopMessage?.Invoke(this, "iteration nOnlooker Bees " + (m + 1).ToString());
                    //% Select Source Site %
                    int i = RouletteWheelSelection(P);
                    //% Choose k randomly, not equal to i %

                    List<int> KPool = new List<int>();
                    for (int n = 0; n < nPop; n++)
                    {
                        if (n == i)
                            continue;
                        KPool.Add(n);
                    }

                    int k = KPool[rndK.Next(0, KPool.Count - 1)];

                    //% Define Acceleration Coefficient %
                    double phi = a * (-1 + (rnd.NextDouble() * 2));

                    //% New Bee Position %
                    BeeStruct newBee;
                    newBee.Position = pop[i].Position + phi * (pop[i].Position - pop[k].Position);


                    //% Evaluation %

                    SetSimTimeout(pop[i].Cost * 1.2);//Optimize simulation timing, terminate if cost exceed

                    newBee.Cost = SimCostFunction(newBee.Position);

                    //% Comparison %
                    if (newBee.Cost <= pop[i].Cost)
                    {
                        pop[i] = newBee;
                    }
                    else
                    {
                        C[i] = C[i] + 1;
                    }
                }
                if (StopFlag)
                {
                    OnIterationLoopMessage?.Invoke(this, "Stopping...");
                    break;
                }
                //% Scout Bees %
                for (int i = 0; i < nPop; i++)
                {
                    if (StopFlag)
                    {
                        OnIterationLoopMessage?.Invoke(this, "Stopping...");
                        break;
                    }

                    if (C[i] >= L)
                    {
                        pop[i].Position = VarMin + rnd.NextDouble() * deltaVar;
                        SetSimTimeout(double.IsInfinity(BestSol.Cost) ? DefaultSimTimeout : BestSol.Cost * 1.5);

                        pop[i].Cost = SimCostFunction(pop[i].Position);
                        C[i] = 0;
                    }
                }
                if (StopFlag)
                {
                    OnIterationLoopMessage?.Invoke(this, "Stopping...");
                    break;
                }
                //% Update Best Solution Ever Found %
                for (int i = 0; i < nPop; i++)
                {
                    if (pop[i].Cost <= BestSol.Cost)
                    {
                        BestSol = pop[i];
                    }
                }
    

                //% Store Best Cost Ever Found %
                BestCost[it] = BestSol.Cost;


                //% Display Iteration Information
                OnIterationLoopMessage?.Invoke(this, "Iteration " + (it + 1).ToString() + ": Best Sol { Cost = " + BestCost[it].ToString() + " , Pos = " + BestSol.Position.ToString() + " }");
                //disp(['Iteration ' num2str(it) ': Best Cost = ' num2str(BestCost(it))]);
            }

            List<double> BestCostList = new List<double>();
            for (int i = 0; i < BestCost.Length; i++)
            {
                if (double.IsNaN(BestCost[i]))
                    break;
                BestCostList.Add(BestCost[i]);
            }

            //Show result
            OnIterationLoopMessage?.Invoke(this, "Done!");
            OnOptimizerResult?.Invoke(this, new OnOptimizerResultEventArgs() { BestCostResult = BestCostList.ToArray() });

        }
        */
        public static int RouletteWheelSelection(double[] P)
        {
            Random rnd = new Random();
            double r = rnd.NextDouble();
            double[] C = new double[P.Length];
            C[0] = P[0];


            for (int i = 1; i < P.Length; i++)
            {
                C[i] = C[i - 1] + P[i];
                if (r <= C[i])
                    return i;
            }

            return -1;
        }

    }
    public struct BeeStruct
    {
        public double[] Position;
        //public double Cost;
        public Tuple<double, double> Cost;

    }
    public class OnOptimizerResultEventArgs : EventArgs
    {
        public Tuple<double, double>[] BestCostResult;
        public BeeStruct[] BestBeeResult;
    }
    public class OnOptimizerIterationUpdateEventArgs : EventArgs
    {
        public BeeStruct[] Bees;
    }
    public class WorkerTaskData
    {
        public WorkerTaskData(int parameterCount)
        {
            Parameter = new double[parameterCount];
        }
        public SimulationInstance SimInstance;
        public Thread WorkerThread;
        public bool DNF;
        //public double Result;
        public Tuple<double, double> Result;
        public double[] Parameter = null;
        public int PopIndex;
        public int WorkerIndex;
        public double SimulationTimeout = double.NaN;
        public bool IsCompleted = false;
    }
}
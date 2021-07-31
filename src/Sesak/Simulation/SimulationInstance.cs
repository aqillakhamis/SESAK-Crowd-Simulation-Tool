using Sesak.Commons;
using Sesak.Path;
using Sesak.SimulationObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class SimulationInstance
    {
        //public static bool MultiObjective = false;
        public static RectangleF ComfortTestZone = new RectangleF();
        public static List<RectangleF> EvacuationAreas = new List<RectangleF>();

        public static bool CalculateDiscomformFromStart = false;
        //public static bool UseOptimized = false;
        public static bool ParallelLoop = false;
        public static bool BatchSimulate = false;

        public static double GlobalSimulationTimeout;
        public int ThreadCount = 4;
        public const double ReachDestinationRange = 1;
        public bool LimitSimulationTiming = false;

        public double TickProcessTime = 0;

        public bool IsRunning { get; private set; }


        public static double PrevValueTest = 0;

        public Line[] obstacleLines = null;



        public SimulationInstance CreateCopy()
        {
            SimulationInstance copy = new SimulationInstance();
            /*
            copy.Agents = new OldAgent[this.Agents.Length];
            for (int i = 0; i < this.Agents.Length; i++)
            {
                copy.Agents[i] = this.Agents[i].Copy();
            }

            copy.Obstacles = new OldObstacle[this.Obstacles.Length];
            for (int i = 0; i < this.Obstacles.Length; i++)
            {
                copy.Obstacles[i] = this.Obstacles[i].Copy();
            }

            copy.Zones = new ZoneData[this.Zones.Length];
            for (int i = 0; i < this.Zones.Length; i++)
            {
                copy.Zones[i] = this.Zones[i].Copy();
            }
            */

            
            copy.SimParam = SimParam.Copy();
            copy.simEnvironment = simEnvironment.Copy();
            //copy.zoneMap = zoneMap;

            return copy;
        }

        public event EventHandler<OnDrawFrameEventArgs> OnDrawFrame;
        public event EventHandler OnSimulationStarted;
        public event EventHandler OnSimulationEnded;
        public event EventHandler<string> OnSimulationError;
        public event EventHandler<OnSimulationTickEventArgs> OnSimulationTick;


        public SimulationEnvironment simEnvironment;
        public ZoneMap zoneMap = new ZoneMap();

        public List<OldAgent> ActiveAgent = new List<OldAgent>();
        public int EndingStatus = 0;
        public OldAgent[] Agents;
        public OldObstacle[] Obstacles;
        public ZoneData[] Zones;

        public SimulationParameters SimParam;
        Stopwatch swTimer = new Stopwatch();

        public void Reset()
        {
            StopSim();

            ThreadCount = 4;

            LimitSimulationTiming = false;

            TickProcessTime = 0;
            IsRunning = false;
            obstacleLines = null;
            zoneMap = new ZoneMap();

            if (ActiveAgent != null)
                ActiveAgent.Clear();

            EndingStatus = 0;
            Agents = null;
            Obstacles = null;
            Zones = null;
            swTimer.Reset();

        }
    public bool Prep()
        {
            zoneMap.GenerateZoneMap(simEnvironment);

            List<OldAgent> oa = new List<OldAgent>();
            bool e = false;
            foreach (Agent a in simEnvironment.Agents)
            {
                oa.Add(a.CreateOldAgent(SimParam, zoneMap));
                if (a.Waypoints == null)
                    e = true;
            }

            Agents = oa.ToArray();

            //load obstacle
            List<OldObstacle> ob = new List<OldObstacle>();
            List<Line> obLine = new List<Line>();
            foreach (IObstacle obs in simEnvironment.Walls)
            {
                ob.AddRange(obs.CreateOldObstacles());
                obLine.AddRange(obs.GetCollisionLines());
            }

            foreach (IObstacle obs in simEnvironment.Doors)
            {
                ob.AddRange(obs.CreateOldObstacles());
                obLine.AddRange(obs.GetCollisionLines());
            }

            obstacleLines = obLine.ToArray();
            Obstacles = ob.ToArray();

            if (simEnvironment.ComfortTestZone.IsEmpty)
            {
                CalculateDiscomformFromStart = true;
            }
            else
            {
                ComfortTestZone = simEnvironment.ComfortTestZone; ;
                CalculateDiscomformFromStart = false;
            }

            EvacuationAreas.Clear();
            foreach(EvacuationArea evac in simEnvironment.EvacuationAreas)
            {
                EvacuationAreas.Add(evac.Area);
            }

            return !e;
        }

        public bool IsStarted
        {
            get
            { return !stopFlag; }
        }
        bool stopFlag = true;
        Thread thSimThread = null;

        public Thread GetWorkerThread()
        {
            return thSimThread;
        }
        public void StopSim()
        {
            stopFlag = true;
        }
        public void ResetTimer()
        {
            swTimer.Reset();
        }
        public double ElapsedTime
        {
            get
            {
                return swTimer.Elapsed.TotalSeconds;
            }
        }
        public bool DNF { get; set; }
        public double SimulationTimeOut
        { get; set; }


        public double SimulationTime
        {
            get; private set;
        }

        public double AverageAgentSpeed
        {
            get; private set;
        }
        public double Discomfort
        {
            get; private set;
        }
        public double EvacuationTime
        {
            get; private set;
        }

        public int TickCount
        {
            get; private set;
        }
        public double testValue;
        public void StartSim(double[] doorPosition)
        {
            if (!stopFlag)
                return;

            ResetTimer();


            stopFlag = false;
            thSimThread = new Thread(delegate () { testValue = RunSimulationForOptimizer((doorPosition)); });
            thSimThread.IsBackground = true;
            thSimThread.Start();
            if (OnSimulationStarted != null)
                OnSimulationStarted(this, EventArgs.Empty);
        }

        public void StartSim()
        {
            if (!stopFlag)
                return;
            AverageAgentSpeed = double.NaN;

            //SetAutoCorridor();
            ResetTimer();

            stopFlag = false;
            thSimThread = new Thread(SimulateLoop);
            
            thSimThread.IsBackground = true;
            thSimThread.Start();

            if (OnSimulationStarted != null)
                OnSimulationStarted(this, EventArgs.Empty);
        }

        private void SetAutoCorridor() //auto find corridor if not marked
        {


            int lastZoneIndex = -1;
            for (int i = 0; i < Zones.Length; i++)
            {
                if (Zones[i].ExitZoneIndex < 0)
                    lastZoneIndex = i;


                if (Zones[i].IsCorridor)
                    return; //corridor already marked, exit
            }

            for (int i = 0; i < Zones.Length; i++)
            {
                if (Zones[i].ExitZoneIndex == lastZoneIndex)
                {
                    Zones[i].IsCorridor = true; //set second last zone as corridor
                    return;
                }
            }

        }

        public void SetSimTimeout(double timeoutSec)
        {
            SimulationTimeOut = timeoutSec;
        }
        public double RunSimulationForOptimizer(double[] y)
        {
            SimulationTime = 0;

            //recreate obstacle data using y values & zones
            /*
            //Group Door here!
            Zones[0].DoorOffset = y[0];
            Zones[1].DoorOffset = y[0];
            Zones[2].DoorOffset = y[0];

            Zones[3].DoorOffset = y[1];
            Zones[4].DoorOffset = y[1];
            Zones[5].DoorOffset = y[1];
            
            //main exit remain center
            Zones[6].DoorOffset = 0.5;
            */

            /*
            for (int i = 0; i < Zones.Length; i++)
            {
                //0: WEST, 1:NORTH, 2: EAST, 3: SOUTH
                switch (Zones[i].DoorLocation)
                {
                    case 1:
                        Zones[i].DoorOffset = y[0]; //North door follow y0
                        break;
                    case 3:
                        Zones[i].DoorOffset = y[1]; //South door follow y1
                        break;
                    default:
                        Zones[i].DoorOffset = 0.5;
                        break;
                }
            }

            Obstacles = ZoneHelper.GenerateObstacles(Zones);
            */

            //UPDATE DOOR POSITION HERE
            simEnvironment.SetOptimizerParameters(y);

            stopFlag = false;
            ResetTimer();
            SimulateLoop(true);
            /*
            if (UseOptimized)
                SimulateLoopOptimized(true);
            else
                SimulateLoop(true);
            */

            return SimulationTime;
        }
        void SimulateLoop()
        {

            SimulateLoop(true); //SimulateLoopOptimized(true);
            /*
            if (AutoManager.Instance.IsAutoMode)
            {
                SimulationTimeOut = AutoManager.Instance.SimulationTimeLimit;
            }
            else
            {
                SimulationTimeOut = -1;
            }
            if (UseOptimized)
                SimulateLoopOptimized(true);
            else
                SimulateLoop(true);

            */
        }
        void SimulateLoop(bool optimizer)
        {

            IsRunning = true;
            DNF = false;
            SimulationTime = 0;
            TickCount = 0;
            swTimer.Start();
            double drawTimingCounter = 0;

            #region Agent Initialize
            ActiveAgent.Clear();
            int tagindex = 0;
            List<OldAgent> agentInvolved = new List<OldAgent>();

            if(!Prep())
            {

                EndingStatus = -1; //DNF
                DNF = true;
                IsRunning = false;
                stopFlag = true;
                swTimer.Stop();

            //throw error
            if (OnSimulationError != null)
                    OnSimulationError(this, "Unable to resolve agent waypoints.");
                return;
            }



            foreach (OldAgent a in Agents)
            {
                OldAgent copy = a.Copy();
                copy.Po = copy.StartPos;



                copy.Vi = SimParam.AgentSpeed;
                copy.CurrentZoneIndex = -1;
                //copy.TargetPos = copy.Waypoint[0];
                copy.ReachDestination = false;
                copy.Next_ReachDestination = false;

                copy.Next_Vo = new Vector2D();
                copy.Next_Po = new Vector2D();
                copy.IndexTag = tagindex;
                tagindex++;

                copy.totalTimeTraveledInCorridor = 0;
                copy.startMeasureSpeed = CalculateDiscomformFromStart;
                copy.distanceTraveledInCorridor = 0;
                agentInvolved.Add(copy);
                ActiveAgent.Add(copy);

            }
            #endregion

            #region Obstacle Initialize
            foreach (OldObstacle obs in Obstacles)
            {
                obs.FixStartEnd();

                //optimize obstacle (reorder to make start position < end pos)
                if (obs.ObstacleType == ObstacleTypes.Vertical && obs.StartPos.X > obs.EndPos.X)
                {
                    double start = obs.EndPos.X;
                    obs.EndPos.X = obs.StartPos.X;
                    obs.StartPos.X = start;
                }
                else if (obs.ObstacleType == ObstacleTypes.Horizontal && obs.StartPos.Y > obs.EndPos.Y)
                {
                    double start = obs.EndPos.Y;
                    obs.EndPos.Y = obs.StartPos.Y;
                    obs.StartPos.Y = start;
                }

            }
            #endregion


            Stopwatch profiler = new Stopwatch();
            profiler.Start();
            EndingStatus = 1;


            while (!stopFlag)
            {
                TickProcessTime = profiler.Elapsed.TotalSeconds;
                profiler.Restart();
                #region Agent Loop

                foreach (OldAgent a in ActiveAgent)
                {
                    a.CurrentWaypointIndex = WaypointHelper.GetNextTarget(a.Waypoint, obstacleLines, a.Po, a.CurrentWaypointIndex);
                    if (a.CurrentWaypointIndex >= 0)
                        a.TargetPos = a.Waypoint[a.CurrentWaypointIndex];
                    else
                        a.TargetPos = a.Waypoint[a.Waypoint.Length - 1];

                    //% ----Agent Motivational to Move towards Target Destination, Fo ----%

                    double distance = SimHelper.GetMagnitude(a.Po, a.TargetPos);


                    Vector2D Fo = new Vector2D();



                    if (distance > 0)
                    {

                        Vector2D tmp = new Vector2D(a.TargetPos.X - a.Po.X, a.TargetPos.Y - a.Po.Y);


                        //% calculate motivational force, Fo %

                        double tmpMagnitude = tmp.GetMagnitude();
                        Vector2D targetdir = new Vector2D(tmp.X / tmpMagnitude, tmp.Y / tmpMagnitude);
                        Vector2D vt = new Vector2D(targetdir.X * a.Vi, targetdir.Y * a.Vi);

                        vt.X -= a.Vo.X;
                        vt.Y -= a.Vo.Y;


                        double tmpmul = a.Mass / SimParam.Delta;
                        Fo.X = (vt.X * tmpmul);
                        Fo.Y = (vt.Y * tmpmul);
                    }


                    if (distance <= ReachDestinationRange && a.CurrentWaypointIndex == a.Waypoint.Length - 1)// && currentZone != null && currentZone.ExitZoneIndex == -1)
                    {
                        a.Next_ReachDestination = true;
                        continue;
                    }


                    //% ----Agent Repulsion Force with Other Agent---- %

                    Vector2D Fij = new Vector2D();

                    foreach (OldAgent b in ActiveAgent)
                    {
                        if (b == a)
                            continue;


                        if (b.ReachDestination)
                            continue;

                        Vector2D tmpFij = SimHelper.FijAgent(SimParam.A, SimParam.B, SimParam.k, SimParam.kappa, a.Po, b.Po, a.BodyRad, b.BodyRad, a.Vo, b.Vo); //Fijagent(A, B, k, kappa, Agent(n).('Po'), Agent(p).('Po'), Agent(n).('BodyRad'), Agent(p).('BodyRad'), Agent(n).('Vo'), Agent(p).('Vo'));

                        Fij.X += tmpFij.X;
                        Fij.Y += tmpFij.Y;
                        //Console.WriteLine("X:" + Fij.X +", Y:"+ Fij.Y);


                    }


                    //% ----Agent Repulsion Force with Obstacle ----%

                    Vector2D Fiw = new Vector2D();
                    foreach (OldObstacle obs in Obstacles)
                    {

                        Vector2D tmpFiw = SimHelper.Fiwobs(SimParam.A, SimParam.B, SimParam.k, SimParam.kappa, a.Po, obs, a.BodyRad, a.MinRange, a.Vo);
                        Fiw.X += tmpFiw.X;
                        Fiw.Y += tmpFiw.Y;
                    }




                    Vector2D Ftot = new Vector2D(Fo.X / a.Mass + Fij.X / a.Mass + Fiw.X / a.Mass, Fo.Y / a.Mass + Fij.Y / a.Mass + Fiw.Y / a.Mass);




                    Vector2D Acc = new Vector2D(Ftot.X, Ftot.Y);


                    a.Next_Vo.X = a.Vo.X + Acc.X * SimParam.dt;
                    a.Next_Vo.Y = a.Vo.Y + Acc.Y * SimParam.dt;



                    double nextVelMag = a.Next_Vo.GetMagnitude();
                    if (nextVelMag > a.Vi)
                    {

                        double nextValMul = a.Vi / nextVelMag;
                        a.Next_Vo.X *= nextValMul;
                        a.Next_Vo.Y *= nextValMul;


                    }



                    a.Next_Po.X = a.Po.X + a.Next_Vo.X * SimParam.dt;
                    a.Next_Po.Y = a.Po.Y + a.Next_Vo.Y * SimParam.dt;

                    /*
-                    if (a == testAgent)
-                    {
-
-                        
-                        if (!Validator.SetValueA(a.NextPos.X, TickCount))
-                        {
-                            stopFlag = true;
-                            Debug.Print("PoX:" + a.Po.X.ToString());
-                            Debug.Print("PoY:" + a.Po.Y.ToString());
-                        }
-                        
-                    }
-                    */
                    a.Next_Vo.Rounding();
                    a.Next_Po.Rounding();

                    //average agent speed
                    if (!CalculateDiscomformFromStart)
                    {
                        a.startMeasureSpeed = ComfortTestZone.Contains((float)a.Po.X, (float)a.Po.Y);
                    }

                    if (a.startMeasureSpeed)
                    {
                        a.velocityTraveledInCorridor.Add(a.Next_Vo.GetMagnitude());
                        a.distanceTraveledInCorridor += a.Po.GetDistance(a.Next_Po);
                        a.totalTimeTraveledInCorridor += SimParam.dt;


                    }
                }
                #endregion




                // simulation time 
                SimulationTime += SimParam.dt;
                TickCount++;



                //Trigger OnSimulationTickEvent to handle logging in main form
                OnSimulationTickEventArgs tickEventArgs = new OnSimulationTickEventArgs() { Agents = agentInvolved.ToArray(), Obstacles = this.Obstacles, SimulationTime = this.SimulationTime, TickCount = this.TickCount };
                OnSimulationTick?.Invoke(this, tickEventArgs);

                foreach (OldAgent a in ActiveAgent)
                {
                    a.Po = a.Next_Po;
                    a.Vo = a.Next_Vo;
                    a.ReachDestination = a.Next_ReachDestination;

                    //evacuated
                    if(!a.Evacuated)
                    {
                        //test evac
                        for (int i = 0; i < EvacuationAreas.Count; i++)
                        {
                            if (EvacuationAreas[i].Contains((float)a.Po.X, (float)a.Po.Y))
                            {
                                a.Evacuated = true;
                                a.EvacuateTime = SimulationTime;

                                EvacuationTime = SimulationTime; //max evac time
                                break;
                            }
                        }
                    }

                }

                //Remove agent that already reach target
                for (int i = ActiveAgent.Count - 1; i >= 0; i--)
                {
                    if (ActiveAgent[i].ReachDestination)
                        ActiveAgent.RemoveAt(i);
                }


                //% ----Plot Agent---- %
                drawTimingCounter += SimParam.dt;
                if (drawTimingCounter >= SimParam.DrawInterval)//if (drawtimingcounter >= drawinterval)
                {
                    drawTimingCounter -= SimParam.DrawInterval;//drawtimingcounter = 0;


                    //DRAW CODE HANDLE AT FORM PLOT
                    if (OnDrawFrame != null)
                        OnDrawFrame(this, new OnDrawFrameEventArgs() { Data = FrameData.Generate(SimulationTime, ActiveAgent.ToArray(), Obstacles.ToArray()) });

                    if (LimitSimulationTiming)
                    {
                        while (SimulationTime > swTimer.Elapsed.TotalSeconds)
                        {
                            Thread.Sleep(1);
                        }
                    }

                }



                if (ActiveAgent.Count <= 0)
                    break; //no more active agent, complete sim

                if (tickEventArgs.StopFlag) //stopped by user through OnSimulationTickEvent, abort sim
                    break;

                if ((SimulationTimeOut > 0 && SimulationTime > SimulationTimeOut) || (GlobalSimulationTimeout > 0 && SimulationTime > GlobalSimulationTimeout))
                {
                    EndingStatus = -1; //DNF
                    DNF = true;
                    break; //Simulation DNF, terminate
                }

            }
            IsRunning = false;

            if (EndingStatus == 1)
                EndingStatus = 0; //Finished
            stopFlag = true;
            swTimer.Stop();

            //calculate average agent speed
            double totalAverage = 0;
            int averageCount = 0;
            double sumD = 0;
            for (int i = 0; i < agentInvolved.Count; i++)
            {
                if (agentInvolved[i].totalTimeTraveledInCorridor > 0)
                {
                    double agentAverageSpeed = agentInvolved[i].velocityTraveledInCorridor.Average();
                    totalAverage += agentAverageSpeed;
                    averageCount++;
                    double tmp = 0;
                    for (int j = 0; j < agentInvolved[i].velocityTraveledInCorridor.Count; j++)
                    {
                        tmp += agentInvolved[i].velocityTraveledInCorridor[j] * agentInvolved[i].velocityTraveledInCorridor[j];
                    }
                    tmp /= agentInvolved[i].velocityTraveledInCorridor.Count;

                    sumD += 1 - ((agentAverageSpeed * agentAverageSpeed) / tmp);
                }
            }
            if (averageCount > 0)
            {
                totalAverage /= averageCount;
            }
            else
            {
                totalAverage = double.NaN;
            }
            totalAverage.ToString();
            AverageAgentSpeed = totalAverage;

            //calculate D
            Discomfort = sumD / averageCount;

            

            if (OnSimulationEnded != null)
                OnSimulationEnded(this, EventArgs.Empty);

        }

        /*
        void SimulateLoopOptimized(bool optimizer)
        {


            DNF = false;
            SimulationTime = 0;
            TickCount = 0;
            swTimer.Start();
            double drawTimingCounter = 0;

            #region Agent Initialize
            ActiveAgent.Clear();
            int tagindex = 0;

            //AgentStruct[] agentsData = new AgentStruct[Agents.Length];
            //Agent[] agentsRef = new Agent[Agents.Length];

            SimParam.ReachDestinationRange = ReachDestinationRange;
            List<OldAgent> agentInvolved = new List<OldAgent>();
            foreach (OldAgent a in Agents)
            {
                OldAgent copy = a.Copy();
                copy.Po = copy.StartPos;



                copy.Vi = SimParam.AgentSpeed;
                copy.CurrentZoneIndex = -1;
                //copy.TargetPos = copy.Waypoint[0];
                copy.ReachDestination = false;
                copy.Next_ReachDestination = false;

                copy.Next_Vo = new Vector2D();
                copy.Next_Po = new Vector2D();
                copy.IndexTag = tagindex;
                tagindex++;

                copy.totalTimeTraveledInCorridor = 0;
                copy.startMeasureSpeed = CalculateDiscomformFromStart;
                copy.distanceTraveledInCorridor = 0;
                agentInvolved.Add(copy);
                ActiveAgent.Add(copy);

            }
            #endregion
            List<ObstacleStruct> obsHorizontalList = new List<ObstacleStruct>();
            List<ObstacleStruct> obsVerticalList = new List<ObstacleStruct>();

            ObstacleStruct[] obsHorizontal;
            ObstacleStruct[] obsVertical;

            #region Obstacle Initialize
            foreach (OldObstacle obs in Obstacles)
            {
                obs.FixStartEnd();

                //optimize obstacle (reorder to make start pos < end pos)
                if (obs.ObstacleType == ObstacleTypes.Vertical && obs.StartPos.X > obs.EndPos.X)
                {
                    double start = obs.EndPos.X;
                    obs.EndPos.X = obs.StartPos.X;
                    obs.StartPos.X = start;
                    obsVerticalList.Add(obs.ToStruct());
                }
                else if (obs.ObstacleType == ObstacleTypes.Horizontal && obs.StartPos.Y > obs.EndPos.Y)
                {
                    double start = obs.EndPos.Y;
                    obs.EndPos.Y = obs.StartPos.Y;
                    obs.StartPos.Y = start;
                    obsHorizontalList.Add(obs.ToStruct());
                }

            }
            #endregion

            obsHorizontal = obsHorizontalList.ToArray();
            obsVertical = obsVerticalList.ToArray();





            Stopwatch profiler = new Stopwatch();
            profiler.Start();

            EndingStatus = 1;
            while (!stopFlag)
            {
                    TickProcessTime = profiler.Elapsed.TotalSeconds;
                    profiler.Restart();
                    Action<int> agentloop = OptimizedSimulation.AgentLoop(ActiveAgent.ToArray(), obstacleLines, obsHorizontal, obsVertical,  SimParam);
                //Parallel.For(0, ActiveAgent.Count, agentloop);

                if (ParallelLoop)
                    Parallel.For(0, ActiveAgent.Count, agentloop);
                else
                {
                    for (int i = 0; i < ActiveAgent.Count; i++)
                    {
                        agentloop(i);
                    }
                }


                // simulation time 
                SimulationTime += SimParam.dt;
                TickCount++;

                //Remove agent that already reach target
                for (int i = ActiveAgent.Count - 1; i >= 0; i--)
                {
                    if (ActiveAgent[i].Next_ReachDestination)
                    {
                        ActiveAgent.RemoveAt(i);

                        if (ActiveAgent.Count <= 0)
                        {
                            stopFlag = true;
                            break; //no more active agent, complete sim
                        }
                        continue;
                    }
                    ActiveAgent[i].Po = ActiveAgent[i].Next_Po;
                    ActiveAgent[i].Vo = ActiveAgent[i].Next_Vo;
                    ActiveAgent[i].ReachDestination = ActiveAgent[i].Next_ReachDestination;
                }

                //% ----Plot Agent---- %
                drawTimingCounter += SimParam.dt;
                if (drawTimingCounter >= SimParam.DrawInterval)//if (drawtimingcounter >= drawinterval)
                {
                    drawTimingCounter -= SimParam.DrawInterval;//drawtimingcounter = 0;


                    //DRAW CODE HANDLE AT FORM PLOT
                    if (OnDrawFrame != null)
                        OnDrawFrame(this, new OnDrawFrameEventArgs() { Data = FrameData.Generate(SimulationTime, ActiveAgent.ToArray(), Obstacles.ToArray()) });

                    if (LimitSimulationTiming)
                    {
                        while (SimulationTime > swTimer.Elapsed.TotalSeconds)
                        {
                            Thread.Sleep(1);
                        }
                    }

                }

                OnSimulationTickEventArgs tickEventArgs = new OnSimulationTickEventArgs() { Agents = ActiveAgent.ToArray(), Obstacles = this.Obstacles, SimulationTime = this.SimulationTime, TickCount = this.TickCount };
                OnSimulationTick?.Invoke(this, tickEventArgs);
                if (BatchSimulate)
                {


                    if (tickEventArgs.StopFlag) //stopped by user through OnSimulationTickEvent, abort sim
                        break;
                }

                if ((SimulationTimeOut > 0 && SimulationTime > SimulationTimeOut) || (GlobalSimulationTimeout > 0 && SimulationTime > GlobalSimulationTimeout))
                {
                    EndingStatus = -1; //DNF
                    DNF = true;
                    break; //Simulation DNF, terminate
                }

            }
            if (EndingStatus == 1)
                EndingStatus = 0; //Finished
            stopFlag = true;
            swTimer.Stop();

            //calculate average agent speed

            double totalAverage = 0;
            int averageCount = 0;
            double sumD = 0;
            
            for (int i = 0; i < agentInvolved.Count; i++)
            {
                if (agentInvolved[i].totalTimeTraveledInCorridor > 0)
                {
                    double agentAverageSpeed = agentInvolved[i].velocityTraveledInCorridor.Average();
                    totalAverage += agentAverageSpeed;
                    averageCount++;
                    double tmp = 0;
                    for (int j = 0; j < agentInvolved[i].velocityTraveledInCorridor.Count; j++)
                    {
                        tmp += agentInvolved[i].velocityTraveledInCorridor[j] * agentInvolved[i].velocityTraveledInCorridor[j];
                    }
                    tmp /= agentInvolved[i].velocityTraveledInCorridor.Count;

                    sumD += 1 - ((agentAverageSpeed * agentAverageSpeed) / tmp);
                }
            }
            
            if (averageCount > 0)
            {
                totalAverage /= averageCount;
            }
            else
            {
                totalAverage = double.NaN;
            }
            totalAverage.ToString();
            AverageAgentSpeed = totalAverage;

            //calculate D
            Discomfort = sumD / averageCount;

            if (OnSimulationEnded != null)
                OnSimulationEnded(this, EventArgs.Empty);

        }
        */
    }

    public class OnSimulationTickEventArgs : EventArgs
    {
        public OldAgent[] Agents;
        public OldObstacle[] Obstacles;
        public double SimulationTime { get; set; }
        public int TickCount { get; set; }
        public bool StopFlag = false;
    }

    public class OnDrawFrameEventArgs :EventArgs
    {
        public FrameData Data;
    }
}

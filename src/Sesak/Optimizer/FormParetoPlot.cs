using Sesak.Simulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WeifenLuo.WinFormsUI.Docking;

namespace Sesak.Optimizer
{
    public partial class FormParetoPlot : DockContent
    {
        const float Inflation = 0.1f;

        public static FormParetoPlot Instance;
        public List<Tuple<double, double>> LineData = new List<Tuple<double, double>>();
        public List<BeeStruct> PlotData = new List<BeeStruct>();
        public List<BeeStruct> NonDominatedData = new List<BeeStruct>();

        public FormParetoPlot()
        {
            InitializeComponent();
            Instance = this;
        }

        private void FormParetoPlot_Load(object sender, EventArgs e)
        {
            if (ABCOptimizer.Instance != null)
                ABCOptimizer.Instance.OnOptimizerIterationLoopResult += Instance_OnOptimizerIterationLoopResult;
        }

        private void Instance_OnOptimizerIterationLoopResult(object sender, OnOptimizerIterationUpdateEventArgs e)
        {
            MethodInvoker mi = new MethodInvoker(delegate ()
            {
                //ParetoChart.Series[0].Points.Clear();
                int count = 0;
                PlotData.Clear();
                for (int i = 0; i < e.Bees.Length; i++)
                {
                    if (e.Bees[i].Cost == null ||
                    double.IsNaN(e.Bees[i].Cost.Item1) || double.IsInfinity(e.Bees[i].Cost.Item1) ||
                    double.IsNaN(e.Bees[i].Cost.Item2) || double.IsInfinity(e.Bees[i].Cost.Item2))
                        continue;
                    //ParetoChart.Series[0].Points.AddXY(e.Bees[i].Cost.Item1, e.Bees[i].Cost.Item2);
                    PlotData.Add(e.Bees[i]);
                    count++;
                }

                UpdatePareto();

                this.Text = count.ToString() + "/" + e.Bees.Length.ToString();
            });
            this.Invoke(mi);
        }
        /*
        private void UpdateParetoLine()
        {
            //UpdateLineData();
            double minX = 0;
            double minY = 0;
            double maxX = 0;
            double maxY = 0;
            ParetoChart.Series[1].Points.Clear();
            for (int i = 0; i < LineData.Count; i++)
            {
                if(i == 0)
                {
                    minX = LineData[i].Item1;
                    maxX = LineData[i].Item1;
                    maxX = LineData[i].Item2;
                    minY = LineData[i].Item2;
                }
                ParetoChart.Series[1].Points.AddXY(LineData[i].Item1, LineData[i].Item2);
                if (maxX < LineData[i].Item1)
                    maxX = LineData[i].Item1;
                else if (minX > LineData[i].Item1)
                    minX = LineData[i].Item1;

                if (maxY < LineData[i].Item2)
                    maxY = LineData[i].Item2;
                else if (minY > LineData[i].Item2)
                    minY = LineData[i].Item2;
            }

            ParetoChart.ChartAreas[0].AxisX.Maximum = double.NaN;
            ParetoChart.ChartAreas[0].AxisX.Minimum = double.NaN;
            ParetoChart.ChartAreas[0].AxisY.Maximum = double.NaN;
            ParetoChart.ChartAreas[0].AxisY.Minimum = double.NaN;
        }
        */
        /*
        private void UpdateLineData()
        {
            LineData.Clear();
            List<Tuple<double, double>> tmp = new List<Tuple<double, double>>();
            bool dataRemoved = true;
            tmp.AddRange(PlotData);


            ParetoChart.ChartAreas[0].AxisX.Maximum = double.NaN;
            ParetoChart.ChartAreas[0].AxisX.Minimum = double.NaN;
            ParetoChart.ChartAreas[0].AxisY.Maximum = double.NaN;
            ParetoChart.ChartAreas[0].AxisY.Minimum = double.NaN;

            dataRemoved = true;
            
            while (dataRemoved)
            {

                double xHigh = -1;
                int xHighIndex = 0;
                double yHigh = -2;
                int yHighIndex = 0;
                for (int i = 0; i < tmp.Count; i++)
                {
                    if (i == 0)
                    {
                        xHigh = tmp[i].Item1;
                        xHighIndex = 0;
                        yHigh = tmp[i].Item2;
                        yHighIndex = 0;
                        continue;
                    }
                    if (xHigh > tmp[i].Item1)
                    {
                        xHigh = tmp[i].Item1;
                        xHighIndex = i;
                    }
                    if (yHigh > tmp[i].Item2)
                    {
                        yHigh = tmp[i].Item2;
                        yHighIndex = i;
                    }

                }
                if (yHighIndex == xHighIndex && tmp.Count > xHighIndex)
                {
                    tmp.RemoveAt(xHighIndex);
                    continue;
                }
                break;


            }
            if (tmp.Count <= 2)
                return;
            tmp.ToArray();

            //get minmax
            double minX = 0;
            double minY = 0;
            double maxX = 0;
            double maxY = 0;

            for (int i = 0; i < tmp.Count; i++)
            {
                if (i == 0)
                {
                    maxX = tmp[i].Item1;
                    minX = tmp[i].Item1;
                    maxY = tmp[i].Item2;
                    minY = tmp[i].Item2;
                    continue;
                }
                if (maxX < tmp[i].Item1)
                    maxX = tmp[i].Item1;
                else if (minX > tmp[i].Item1)
                    minX = tmp[i].Item1;

                if (maxY < tmp[i].Item2)
                    maxY = tmp[i].Item2;
                else if (minY > tmp[i].Item2)
                    minY = tmp[i].Item2;
            }


            double deltaX = maxX - minX;
            double deltaY = maxY - minY;
            if (deltaX == 0 || deltaY == 0)
                return;

            int sliceCount = 50;

            double tmpminX = 0;
            double tmpminY = 0;
            int minIndexX = -1;
            int minIndexY = -1;
            List<Tuple<double, double>> tmp2 = new List<Tuple<double, double>>();
            for (int i = 1; i < sliceCount; i++)
            {
                double limX = deltaX / sliceCount * i + minX;


                int counter = 0;
                int targetIndex = -1;
                for (int n = tmp.Count - 1; n >= 0; n--)
                {
                    if (tmp[n].Item1 <= limX)
                    {
                        counter++;
                        targetIndex = n;
                    }

                }
                if(counter == 1)
                {
                    tmp.RemoveAt(targetIndex);
                }
            }
            for (int i = 1; i < sliceCount; i++)
            {

                double limY = deltaY / sliceCount * i + minY;

                int counter = 0;
                int targetIndex = -1;
                for (int n = tmp.Count - 1; n >= 0; n--)
                {
                    if (tmp[n].Item2 <= limY)
                    {
                        counter++;
                        targetIndex = n;
                    }

                }
                if (counter == 1)
                {
                    tmp.RemoveAt(targetIndex);
                }
            }
            for (int i = 1; i < sliceCount; i++)
            {
                double limX = deltaX / sliceCount * i + minX;
                double limY = deltaY / sliceCount * i + minY;
                bool foundX = false;
                bool foundY = false;

                for (int n = tmp.Count - 1; n >= 0; n--)
                {
                    if (tmp[n].Item1 >= limX || tmp[n].Item2 >= limY)
                        continue;

                    if(minIndexX < 0 || tmpminX > tmp[n].Item1)
                    {
                        tmpminX = tmp[n].Item1;
                        minIndexX = n;
                        foundX = true;
                    }
                    

                    if (minIndexY < 0 || tmpminY > tmp[n].Item2)
                    {
                        tmpminY = tmp[n].Item2;
                        minIndexY = n;
                        foundY = true;
                    }
                }
                if(foundX)
                {
                    tmp2.Add(tmp[minIndexX]);
                }
                if(foundY && minIndexX != minIndexY)
                {
                    tmp2.Add(tmp[minIndexY]);
                }

            }
            tmp.Clear();
            tmp.AddRange(tmp2);
           

            tmp.Sort(SortCost);
            LineData.AddRange(tmp);
        }
        */
        private int SortCost(Tuple<double, double> a, Tuple<double, double> b)
        {
            return a.Item1.CompareTo(b.Item1);
        }
        private void FormParetoPlot_FormClosing(object sender, FormClosingEventArgs e)
        {
            ABCOptimizer.Instance.OnOptimizerIterationLoopResult -= Instance_OnOptimizerIterationLoopResult;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                StreamWriter sw = new StreamWriter(dlg.FileName, false);
                sw.WriteLine("No,Time,Discomfort");
                for (int i = 0; i < PlotData.Count; i++)
                {
                    sw.WriteLine((i + 1).ToString() + "," + PlotData[i].Cost.Item1.ToString() + "," + PlotData[i].Cost.Item2.ToString());
                }
                sw.Close();
            }
        }
        private void UpdateNonDominatedMembers()
        {
            List<Tuple<BeeStruct, bool>> popData = new List<Tuple<BeeStruct, bool>>();

            for (int i = 0; i < PlotData.Count; i++)
            {
                popData.Add(new Tuple<BeeStruct, bool>(PlotData[i], false));
            }


            NonDominatedData.Clear();
            for (int i = 0; i < popData.Count; i++)
            {
                if (popData[i].Item2)
                    continue;

                for (int j = 0; j < popData.Count; j++)
                {
                    if (Pareto.Dominate(popData[j].Item1.Cost, popData[i].Item1.Cost))
                    {
                        popData[i] = new Tuple<BeeStruct, bool>(popData[i].Item1, true);
                        break;
                    }

                }
            }
            for (int i = 0; i < popData.Count; i++)
            {
                if (!popData[i].Item2)
                {
                    NonDominatedData.Add(popData[i].Item1); //select only non dominated
                }
            }
        }

        private void UpdatePareto()
        {
            UpdateNonDominatedMembers();
            UpdateGraph();
        }
        private void UpdateGraph()
        {
            double minX = 0;
            double minY = 0;
            double maxX = 0;
            double maxY = 0;
            ParetoChart.Series[0].Points.Clear();
            ParetoChart.Series[1].Points.Clear();

            if (!chkHideDominated.Checked)
            {
                for (int i = 0; i < PlotData.Count; i++)
                {
                    if (i == 0)
                    {
                        minX = PlotData[0].Cost.Item1;
                        maxX = PlotData[0].Cost.Item1;

                        minY = PlotData[0].Cost.Item2;
                        maxY = PlotData[0].Cost.Item2;
                    }

                    if (minX > PlotData[i].Cost.Item1)
                    {
                        minX = PlotData[i].Cost.Item1;
                    }
                    else if (maxX < PlotData[i].Cost.Item1)
                    {
                        maxX = PlotData[i].Cost.Item1;
                    }

                    if (minY > PlotData[i].Cost.Item2)
                    {
                        minY = PlotData[i].Cost.Item2;
                    }
                    else if (maxY < PlotData[i].Cost.Item2)
                    {
                        maxY = PlotData[i].Cost.Item2;
                    }

                    
                    DataPoint dp = new DataPoint(PlotData[i].Cost.Item1, PlotData[i].Cost.Item2);
                    dp.Tag = PlotData[i];
                    ParetoChart.Series[0].Points.Add(dp);
                }
            }
            for (int i = 0; i < NonDominatedData.Count; i++)
            {
                if (chkHideDominated.Checked)
                {
                    if (i == 0)
                    {
                        minX = NonDominatedData[0].Cost.Item1;
                        maxX = NonDominatedData[0].Cost.Item1;

                        minY = NonDominatedData[0].Cost.Item2;
                        maxY = NonDominatedData[0].Cost.Item2;
                    }

                    if (minX > NonDominatedData[i].Cost.Item1)
                    {
                        minX = NonDominatedData[i].Cost.Item1;
                    }
                    else if (maxX < NonDominatedData[i].Cost.Item1)
                    {
                        maxX = NonDominatedData[i].Cost.Item1;
                    }

                    if (minY > NonDominatedData[i].Cost.Item2)
                    {
                        minY = NonDominatedData[i].Cost.Item2;
                    }
                    else if (maxY < NonDominatedData[i].Cost.Item2)
                    {
                        maxY = NonDominatedData[i].Cost.Item2;
                    }
                }
                DataPoint dp = new DataPoint(NonDominatedData[i].Cost.Item1, NonDominatedData[i].Cost.Item2);
                dp.Tag = NonDominatedData[i];
                ParetoChart.Series[1].Points.Add(dp);

                //ParetoChart.Series[1].Points.AddXY(NonDominatedData[i].Cost.Item1, NonDominatedData[i].Cost.Item2);
            }
            //adjust scale
            double dx = maxX - minX;
            double dy = maxY - minY;
            if (dx > 0)
            {
                ParetoChart.ChartAreas[0].AxisX.Minimum = minX - dx * Inflation;
                ParetoChart.ChartAreas[0].AxisX.Maximum = maxX + dx * Inflation;
            }
            if (dy > 0)
            {
                ParetoChart.ChartAreas[0].AxisY.Minimum = minY - dy * Inflation;
                ParetoChart.ChartAreas[0].AxisY.Maximum = maxY + dy * Inflation;
            }
        }
        /*
        private void btnImport_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                string[] lines = File.ReadAllLines(dlg.FileName);

                PlotData.Clear();
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    string[] parts = line.Split(',');
                    if (parts.Length != 3)
                        continue;
                    int n;
                    double time;
                    double discomfort;
                    if (!int.TryParse(parts[0], out n) || !double.TryParse(parts[1], out time) || !double.TryParse(parts[2], out discomfort))
                        continue;

                    PlotData.Add(new Tuple<double, double>(time, discomfort));
                }

                UpdatePareto();
            }
        }
        */
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UpdateGraph();
        }

        private void chkHideDominated_CheckedChanged(object sender, EventArgs e)
        {
            btnRefresh_Click(this, EventArgs.Empty);
        }

        private void ParetoChart_Click(object sender, EventArgs e)
        {
            
        }

        private void ParetoChart_MouseDoubleClick(object sender, MouseEventArgs e)
        {
           HitTestResult hits =   ParetoChart.HitTest(e.X, e.Y);
            if (hits.PointIndex >= 0 && hits.Series != null && hits.Series.Points[hits.PointIndex].Tag is BeeStruct)
            {
                BeeStruct bee = (BeeStruct)hits.Series.Points[hits.PointIndex].Tag;

                FormViewPointEnvironmentDialog frm = new FormViewPointEnvironmentDialog(bee, OptimizerWorkspace.Instance.SimInstance.CreateCopy());
                frm.ShowDialog();
            }
                

        }
    }
}

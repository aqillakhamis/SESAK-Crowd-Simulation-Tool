using Sesak.Commons;
using Sesak.Simulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Sesak.Optimizer
{
    public partial class FormOptimizerLogs : DockContent
    {
        public ABCOptimizer optimizer = new ABCOptimizer();
        public SimulationInstance SimInstanceTemplate;
        public List<SimulationInstance> SimInstances = new List<SimulationInstance>();
        
        public bool ShowPlot { get; set; }
        int workerCount = 4;
        string logText = "";
        Stopwatch swSimTime = new Stopwatch();
        public int WorkerCount
        {
            get { return workerCount; }
            set
            {
                if (workerCount <= 0)
                    workerCount = 1;
                else if (workerCount > 32)
                    workerCount = 32;
                else
                    workerCount = value;
            }
        }
        public FormOptimizerLogs()
        {
            WorkerCount = 4;
            InitializeComponent();
        }

        private void FormOptimizerLogs_Load(object sender, EventArgs e)
        {

        }

        public void RunOptimizer()
        {
            optimizer.OnIterationLoopMessage += Optimizer_OnIterationLoopMessage;
            optimizer.OnOptimizerResult += Optimizer_OnOptimizerResult;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
            SimInstances.Clear();
            for (int i = 0; i < workerCount; i++)
            {
                SimInstances.Add(SimInstanceTemplate.CreateCopy());
            }
            optimizer.SimulationInstances = SimInstances.ToArray();

            if (ShowPlot)
            {
                ShowPlots();
                
            }
            logText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Simulation Started";
            ShouldUpdateLogText();

            swSimTime.Restart();
            bgWorker.RunWorkerAsync();
        }

        private void ShowPlots()
        {
            //tmp
            DockPanel dock = FormOptimizerWorkspace.Instance.GetDockContainer();


            if (ABCOptimizer.MultiObjective)
            {
                
                if (dock.ActiveDocumentPane == null)
                    new FormParetoPlot().Show(dock);
                
                else
                    new FormParetoPlot().Show(dock.ActiveDocumentPane, DockAlignment.Top, 0.5);
            }
            for (int i = 0; i < workerCount; i++)
            {


                if (dock.ActiveDocumentPane == null)
                    new FormPlot(i, true).Show(dock);
                else
                {
                    if(i == 0)
                        new FormPlot(i, true).Show(dock.ActiveDocumentPane, DockAlignment.Bottom, 0.5);
                    else
                        new FormPlot(i, true).Show(dock.ActiveDocumentPane, DockAlignment.Right, 1f / workerCount);
                }
                FormPlot.Instances[i].SetSimulationInstance(SimInstances[i]);


            }

        }
        private void MoveCaret()
        {
            txtLogs.SelectionStart = txtLogs.Text.Length;
            txtLogs.ScrollToCaret();
        }


        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MethodInvoker mi = new MethodInvoker(delegate ()
            {
                swSimTime.Stop();
                MessageBox.Show("Optimizer run completed!", "Optimizer Worker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            });
            this.Invoke(mi);
        }

        private void Optimizer_OnOptimizerResult(object sender, OnOptimizerResultEventArgs e)
        {
            MethodInvoker mi = new MethodInvoker(delegate ()
            {

                if (logText.Length > 0)
                    logText += Environment.NewLine;

                logText += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ";

                logText += "Result: \r\n";
                for (int i = 0; i < e.BestCostResult.Length; i++)
                {
                    if (i > 0)
                        logText += ",";

                    logText += e.BestCostResult[i].ToString();
                }
                FormLogGraph chartForm = new FormLogGraph();
                chartForm.ResultData = e.BestCostResult;
                chartForm.Show(FormOptimizerWorkspace.Instance.GetDockContainer(), DockState.Document);
                ShouldUpdateLogText();
            });
            this.Invoke(mi);
        }

        private void Optimizer_OnIterationLoopMessage(object sender, OnIterationLoopMessageEventArgs e)
        {
            MethodInvoker mi = new MethodInvoker(delegate ()
            {

                if (logText.Length > 0)
                    logText += Environment.NewLine;
                logText += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " ";
                logText += e.message;
                ShouldUpdateLogText();
                if (e.iteration >= 0 && e.maxiteration > 0)
                {
                    lblProgress.Text = e.iteration.ToString() + "/" + e.maxiteration.ToString();
                    int v = (int)((float)e.iteration / e.maxiteration * 100);
                    if (v > 100)
                        v = 100;
                    else if (v < 0)
                        v = 0;
                    simProgress.Value = v;
                }

            });
            this.Invoke(mi);
        }
        private void ShouldUpdateLogText()
        {
            if (!tmrLogUpdater.Enabled)
                tmrLogUpdater.Enabled = true;
        }
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            optimizer.RunOptimizer(WorkerCount);
        }

        private void btnShowPlot_Click(object sender, EventArgs e)
        {
            ShowPlots();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            optimizer.StopFlag = true;
        }

        private void tmrLogUpdater_Tick(object sender, EventArgs e)
        {
            const int TextLengthLimit = 10240;//1048576;

            if (logText.Length > TextLengthLimit)
                logText = logText.Substring(logText.Length - TextLengthLimit);

            txtLogs.Text = logText;

            MoveCaret();

            tmrLogUpdater.Enabled = false;
        }

        private void tmrUpdateTime_Tick(object sender, EventArgs e)
        {
            this.Text = "Optimizer [" + Math.Floor(swSimTime.Elapsed.TotalHours).ToString() + ":" + swSimTime.Elapsed.Minutes.ToString("00") + ":" + swSimTime.Elapsed.Seconds.ToString("00") + "]";
        }

        private void btnSaveEnvironment_Click(object sender, EventArgs e)
        {
            if(optimizer.BestBees == null || optimizer.BestBees.Length <= 0)
                MessageBox.Show("No data to export", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

            FormExportEnvironmentDialog expDlg = new FormExportEnvironmentDialog(optimizer.BestBees);
            expDlg.simInstanceCopy = SimInstanceTemplate.CreateCopy();
            expDlg.ShowDialog();
            
        }

        private void btnExportData_Click(object sender, EventArgs e)
        {
            try
            {
                using(SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "CSV|*.csv|All Files|*";
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    using (StreamWriter sw = new StreamWriter(dlg.FileName))
                    {
                        sw.Write("Ittr.,Cost[Time],Cost[Discomfort]"); //header
                        for(int i = 0;i<ABCOptimizer.Y_PARAMCOUNT;i++)
                        {
                            sw.Write(",Y [" + i.ToString() + "]");
                        }
                        sw.WriteLine();

                        for(int i = 0;i<optimizer.BestBees.Length;i++)
                        {
                            

                            int ittr = i +1;
                            sw.Write(ittr.ToString());
                            if (optimizer.BestBees[i].Position == null || double.IsNaN(optimizer.BestBees[i].Cost.Item1) || double.IsNaN(optimizer.BestBees[i].Cost.Item2))
                            {
                                sw.WriteLine();
                                continue;
                            }

                            sw.Write("," + optimizer.BestBees[i].Cost.Item1.ToString() + "," + optimizer.BestBees[i].Cost.Item2.ToString());
                            for (int j = 0; j < ABCOptimizer.Y_PARAMCOUNT; j++)
                            {
                                sw.Write("," + optimizer.BestBees[i].Position[j].ToString());
                            }
                            sw.WriteLine();
                            
                        }
                        sw.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Export Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveEnvironmentItteration_Click(object sender, EventArgs e)
        {
            SimulationEnvironment simEnv = SimInstanceTemplate.simEnvironment.Copy();
            //simEnv.SetOptimizerParameters(bee.Position);
        }
    }
}

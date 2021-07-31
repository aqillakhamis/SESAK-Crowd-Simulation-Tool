using Sesak.Commons;
using Sesak.Path;
using Sesak.Simulation;
using Sesak.SimulationObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Sesak
{
    public partial class FormSimulation : DockContent
    {
        public FundamentalDiagram FDiagram = new  FundamentalDiagram();
        
        public SimulationInstance SimInstance;

        //public SimulationParameters SimParam;
        //public SimulationEnvironment simEnvironment;
        
        //public ZoneMap zoneMap;
        public CanvasHelper canvas;

        object drawDataLock = new object();
        //List<OldAgent> agent = new List<OldAgent>();

        //Line[] obsLine = null;

        List<TickData> SimulationData = new List<TickData>();
        //bool flagRequestNewData = false;

        public FormSimulation()
        {
            InitializeComponent();
        }

        public void SetSimulationInstance(SimulationInstance simInstance)
        {
            SimInstance = simInstance;


            SimInstance.OnSimulationStarted += SimInstance_OnSimulationStarted;
            SimInstance.OnSimulationTick += SimInstance_OnSimulationTick;
            SimInstance.OnSimulationEnded += SimInstance_OnSimulationEnded;
            SimInstance.OnSimulationError += SimInstance_OnSimulationError;
            
        }

        private void FDiagram_OnDataUpdated(object sender, EventArgs e)
        {
            //fdTest.data = FDiagram.plotData;
            //fdTest.UpdateData();
        }

        private void SimInstance_OnSimulationStarted(object sender, EventArgs e)
        {
            MethodInvoker mi = new MethodInvoker(delegate ()
            {
                btnExport.Enabled = false;
                btnExportImageSequence.Enabled = false;
                btnExportVideo.Enabled = false;


                txtStatus.Text = "Running...";
                tmrUpdateStats.Enabled = true;
                FDiagram.ClearAllData();

                FormEditorWorkspace.Instance.InitializeFundamentalDiagramPlot();


            });
            this.Invoke(mi);
        }
        public void UpdatePlotOptions()
        {
            chkHeatMap.Checked = SimulationWorkspace.Instance.PlotShowHeatmap;
        }
        private void FormSimulation_Load(object sender, EventArgs e)
        {

            
        }

        private void SimInstance_OnSimulationError(object sender, string e)
        {
            MethodInvoker mi = new MethodInvoker(delegate ()
            {
                txtStatus.Text = "Error";
                MessageBox.Show(e,"Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            });
            this.Invoke(mi);
        }

        private void SimInstance_OnSimulationEnded(object sender, EventArgs e)
        {
            MethodInvoker mi = new MethodInvoker(delegate ()
            {
                if(SimInstance.DNF)
                    txtStatus.Text = "Completed (DNF)";
                else
                    txtStatus.Text = "Completed";

                txtDiscomfort.Text = SimInstance.Discomfort.ToString("0.000");
                txtEvacuationTime.Text = SimInstance.EvacuationTime.ToString("0.000");

                UpdateSimulationStatus();
                btnExport.Enabled = true;
                btnExportImageSequence.Enabled = true;
                btnExportVideo.Enabled = true;
                tmrUpdateStats.Enabled = false;

                if (FormEditorWorkspace.Instance.frmFd != null && !FormEditorWorkspace.Instance.IsDisposed)
                {
                    FormEditorWorkspace.Instance.frmFd.dataset[0] = FDiagram.plotAvgSpeedvsCrowdDensity;
                    FormEditorWorkspace.Instance.frmFd.dataset[1] = FDiagram.plotFlowvsCrowdDensity;
                    FormEditorWorkspace.Instance.frmFd.dataset[2] = FDiagram.plotEvacuatedvsTime;

                    FormEditorWorkspace.Instance.frmFd.UpdateData();
                }
            });
            this.Invoke(mi);
        }

        private void SimInstance_OnSimulationTick(object sender, OnSimulationTickEventArgs e)
        {
            TickData data = new TickData();
            data.Time = e.SimulationTime;
            List<AgentTickData> agentData = new List<AgentTickData>();
            foreach (OldAgent agent in e.Agents)
            {
                agentData.Add(agent.ToAgentTickData());
            }
            data.Agents = agentData.ToArray();

            MethodInvoker mi = new MethodInvoker(delegate ()
            {
                SimulationData.Add(data);
                FDiagram.ProcessTick(data, SimInstance);//.SimParam.Delta, SimInstance.simEnvironment.ComfortTestZone);

            });
            this.Invoke(mi);
            
        }



        private void tmrUpdateStats_Tick(object sender, EventArgs e)
        {
            
            if (SimInstance.IsRunning)
                UpdateSimulationStatus();
            else
                tmrUpdateStats.Enabled = false;
        }

        private void UpdateSimulationStatus()
        {
            MethodInvoker mi = new MethodInvoker(delegate()
            {
                if (SimInstance.Agents == null)
                    return;

                txtAgentCounter.Text = SimInstance.ActiveAgent.Count.ToString() + "/" + SimInstance.Agents.Length.ToString();

                txtTickCount.Text = SimInstance.TickCount.ToString();
                txtSimulationTime.Text = SimInstance.SimulationTime.ToString("0.000");
                txtProcessTime.Text = SimInstance.ElapsedTime.ToString("0.000");
            });
            this.Invoke(mi);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using(SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "CSV|*.csv|All Files|*";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;


                try
                {
                    //scan agent idx
                    List<int> AgentIndex = new List<int>();
                    for (int i = 0; i < SimulationData.Count; i++)
                    {
                        TickData data = SimulationData[i];
                        foreach(AgentTickData a in data.Agents)
                        {
                            if (AgentIndex.Contains(a.Index))
                                continue;
                            AgentIndex.Add(a.Index);
                        }
                    }

                    AgentIndex.Sort();

                    int[] idx = AgentIndex.ToArray();

                    
                    

                    using (StreamWriter sw = new StreamWriter(dlg.FileName))
                    {
                        sw.WriteLine("Simulation Time," + SimInstance.SimulationTime.ToString());   
                        sw.WriteLine("Process Time," + SimInstance.ElapsedTime.ToString());
                        sw.WriteLine("Discomfort," + SimInstance.Discomfort.ToString());
                        sw.WriteLine("Completed," + (SimInstance.DNF ? "NO" : "YES"));
                        sw.WriteLine();

                        string header = "Time";
                        //print header
                        for (int i = 0; i < idx.Length; i++)
                        {
                            header +=
                                ",[" + idx[i] + "] Position X" +
                                ",[" + idx[i] + "] Position Y" +
                                ",[" + idx[i] + "] Velocity X" +
                                ",[" + idx[i] + "] Velocity Y" +
                                ",[" + idx[i] + "] Speed";
                        }
                        sw.WriteLine(header);
                        for (int i = 0; i < SimulationData.Count; i++)
                        {
                            TickData data = SimulationData[i];
                            sw.WriteLine(data.ToLine(idx));
                        }
                        sw.Close();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (SimInstance.IsRunning)
            {
                SimInstance.StopSim();
            }
            FormEditorWorkspace.Instance.EndSimulationMode();
        }

        private void chkHeatMap_CheckedChanged(object sender, EventArgs e)
        {
            SimulationWorkspace.Instance.PlotShowHeatmap = chkHeatMap.Checked;
        }

        private void btnExportImageSequence_Click(object sender, EventArgs e)
        {
            SimulationWorkspace.Instance.ExportImageSequence();
        }

        private void btnExportVideo_Click(object sender, EventArgs e)
        {
            SimulationWorkspace.Instance.ExportVideo();
        }
        /*
private void Sim_OnSimulationTick(object sender, OnSimulationTickEventArgs e)
{
//Debug.Print(e.SimulationTime.ToString("0.0") + " " + e.Agents[0].Position.ToString()); ;
if (flagRequestNewData)
{
lock (drawDataLock)
{
agent.Clear();
foreach (SimAgent sa in e.Agents)
{
agent.Add(new Agent(sa));
}
//agentData = new SimAgent[e.Agents.Length];
flagRequestNewData = false;
}
this.Invoke(new MethodInvoker(delegate() { pbCanvas.Invalidate(); }));
}

}

private void pbCanvas_Paint(object sender, PaintEventArgs e)
{
Agent[] tmpAgent;
lock (drawDataLock)
{
tmpAgent = agent.ToArray();
}
Graphics g = e.Graphics;
g.Clear(Color.White);

foreach(Wall wall in simEnvironment.Walls)
{
wall.Draw(g, canvas);
}
foreach (Door door in simEnvironment.Doors)
{
door.Draw(g, canvas);
}
foreach (Agent agent in tmpAgent)
{
agent.Draw(g, canvas);
}

}
*/
    }
}

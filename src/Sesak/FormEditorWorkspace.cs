using Sesak.Simulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Sesak
{
    public partial class FormEditorWorkspace : DockContent
    {
        private static FormEditorWorkspace instance = null;
        public static FormEditorWorkspace Instance { get { return instance; } }


        public FormFundamentalDiagram frmFd = null;

        FormEditor frmEditor = null;
        FormSimulation frmSimulation = null;
        FormPlot frmPlot = null;

        public FormEditorWorkspace()
        {
            InitializeComponent();
            instance = this;
        }

        public void EndSimulationMode()
        {
            dockSimulationMode.Visible = false;
            dockEditorMode.Visible = true;
        }

        public void InitializeFundamentalDiagramPlot()
        {
            if(frmFd == null || frmFd.IsDisposed)
            {
                frmFd = new FormFundamentalDiagram();
                frmFd.Show(dockSimulationMode, DockState.DockRight);
            }
        }

        private void FormEditorWorkspace_Load(object sender, EventArgs e)
        {
            //Attach simulation events
            SimulationWorkspace.Instance.OnSimulationStarted += Instance_OnSimulationStarted;
            SimulationWorkspace.Instance.OnPlottingOptionChanged += Instance_OnPlottingOptionChanged;
            SimulationWorkspace.Instance.OnExportingAction += Instance_OnExportingAction;

            frmEditor = new FormEditor();
            frmEditor.Show(dockEditorMode);
            
            frmSimulation = new FormSimulation();
            frmSimulation.SetSimulationInstance(SimulationWorkspace.Instance.SimInstance);
            frmSimulation.Show(dockSimulationMode, DockState.DockLeft);
            
            frmPlot = new FormPlot();
            frmPlot.SetSimulationInstance(SimulationWorkspace.Instance.SimInstance);
            frmPlot.Show(dockSimulationMode, DockState.Document);



            frmSimulation.Width = 270;



            dockEditorMode.Dock = DockStyle.Fill;
            dockSimulationMode.Dock = DockStyle.Fill;
            dockSimulationMode.Visible = false;
        }

        private void Instance_OnExportingAction(object sender, bool e)
        {
            if (e)
                frmPlot.ExportVideo();
            else
                frmPlot.ExportImageSequence();
        }

        private void Instance_OnPlottingOptionChanged(object sender, EventArgs e)
        {
            frmPlot.DrawHeatMap = SimulationWorkspace.Instance.PlotShowHeatmap;
        }

        private void Instance_OnSimulationStarted(object sender, EventArgs e)
        {
            MethodInvoker mi = new MethodInvoker(delegate ()
            {
                frmPlot.Reset();

                //Set visibility for simulation mode
                dockSimulationMode.Visible = true;
                dockEditorMode.Visible = false;
                frmSimulation.UpdatePlotOptions();


            });

            this.Invoke(mi);
        }
    }
}

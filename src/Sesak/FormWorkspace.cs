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
    public partial class FormWorkspace : Form
    {
        private static FormWorkspace instance;
        public static FormWorkspace Instance { get { return instance; } }


        public FormWorkspace()
        {
            InitializeComponent();
            instance = this;

            InitializeWorkspace();
        }

        void InitializeWorkspace()
        {
            AppHelper.InitializeConfigPath();


            //initialize simulation workspace
            SimulationWorkspace.Initialize();

            //initialize optimizer workspace
            OptimizerWorkspace.Initialize();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit application?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            this.Close();

        }

        

        private void mainDock_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void FormWorkspace_Load(object sender, EventArgs e)
        {
            LoadSimulationForms();
            LoadOptimizerForms();
        }

        private void LoadSimulationForms()
        {
            FormEditorWorkspace frm = new FormEditorWorkspace();
            frm.Show(mainDock);

            /*
            frmEditor = new FormEditor();
            frmEditorVisualizer = new FormPlot();
            frmEditorSimulation = new FormSimulation();

            frmEditor.Show();
            frmEditorVisualizer.Show();
            frmEditorSimulation.Show();
            */

            /*
            frmEditor.Show(mainDock);
            frmEditorVisualizer.Show(mainDock);
            frmEditor.Visible = false;
            */
        }

        private void LoadOptimizerForms()
        {
            FormOptimizerWorkspace frm = new FormOptimizerWorkspace();
            frm.Show(dockOptimizerWorkspace);
        }
    }
}

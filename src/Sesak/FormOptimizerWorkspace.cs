using Sesak.Optimizer;
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
    public partial class FormOptimizerWorkspace : DockContent
    {
        private static FormOptimizerWorkspace instance = null;
        public static FormOptimizerWorkspace Instance { get { return instance; } }


        FormOptimizer frmOptimizer = null;

        public FormOptimizerLogs frmOptimizerLogs = null;

        public FormOptimizerWorkspace()
        {
            InitializeComponent();
            instance = this;
        }


        public DockPanel GetDockContainer()
        {
            return dockOptimizer;
        }
        private void FormOptimizerWorkspace_Load(object sender, EventArgs e)
        {
            frmOptimizer = new FormOptimizer();
            frmOptimizerLogs = new FormOptimizerLogs();

            frmOptimizerLogs.Show(dockOptimizer, DockState.DockBottom);
            frmOptimizer.Show(dockOptimizer, DockState.DockLeft);
        }

        private void dockOptimizer_ActiveContentChanged(object sender, EventArgs e)
        {

        }
    }
}

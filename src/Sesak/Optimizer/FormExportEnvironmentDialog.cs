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

namespace Sesak.Optimizer
{
    public partial class FormExportEnvironmentDialog : Form
    {
        
        public int SelectedIndex = -1;
        public BeeStruct[] BestBees = null;
        public SimulationInstance simInstanceCopy;
        FormPlot previewPlot;
        public FormExportEnvironmentDialog(BeeStruct[] bestBees)
        {
            InitializeComponent();
            BestBees = bestBees;
            
        }

        void updateIttrCmb()
        {
            if (BestBees == null || BestBees.Length <= 0)
                return;

            for(int i = 0;i<BestBees.Length;i++)
            {
                BeeStruct bee = BestBees[i];
                if (bee.Position == null || double.IsNaN(bee.Cost.Item1) || double.IsNaN(bee.Cost.Item2))
                    break;

                cmbExport.Items.Add((i + 1).ToString());
            }
            if (cmbExport.Items.Count > 0)
                cmbExport.SelectedIndex = cmbExport.Items.Count - 1;

            updateIttrStats();

        }
        private void updateIttrStats()
        {
            int idx = cmbExport.SelectedIndex;
            if (BestBees == null || BestBees.Length <= 0 || idx < 0 || idx >= BestBees.Length)
                return;

            BeeStruct bee = BestBees[idx];
            if (bee.Position == null || double.IsNaN(bee.Cost.Item1) || double.IsNaN(bee.Cost.Item2))
                return;

            txtCostTime.Text = bee.Cost.Item1.ToString();
            txtCostDiscomfort.Text = bee.Cost.Item2.ToString();

            lstPositions.Items.Clear();
            for(int i = 0;i<bee.Position.Length;i++)
            {
                string s = "[" + i.ToString() + "] " + bee.Position[i].ToString();
                lstPositions.Items.Add(s);
            }
            
        }
        private void FormExportEnvironmentDialog_Load(object sender, EventArgs e)
        {
            previewPlot = new FormPlot(0, true);
            previewPlot.Show(dockPreview);

            updateIttrCmb();
        }

        private void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (previewPlot == null)
                return;

            updateIttrStats();
            SelectedIndex = cmbExport.SelectedIndex;
            BeeStruct bee = BestBees[cmbExport.SelectedIndex];

            simInstanceCopy.simEnvironment.SetOptimizerParameters(bee.Position);

            previewPlot.SetSimulationInstance(simInstanceCopy);
            FrameData frm = new FrameData();
            frm.Obstacles = simInstanceCopy.simEnvironment.GetObstacleLines();
            previewPlot.OptimizerFrame = frm;
            previewPlot.ForceRedraw();
        }

        private void btnExportData_Click(object sender, EventArgs e)
        {
            
            try
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "Sim Environment|*.jsv|All Files|*";
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    File.WriteAllText(dlg.FileName, simInstanceCopy.simEnvironment.Export());


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
    }
}

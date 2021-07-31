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
    public partial class FormViewPointEnvironmentDialog : Form
    {
        public BeeStruct bee;
        public SimulationInstance simInstanceCopy;
        FormPlot previewPlot;
        public FormViewPointEnvironmentDialog(BeeStruct bee,SimulationInstance simInstanceCopy)
        {
            InitializeComponent();
            this.bee = bee;
            this.simInstanceCopy = simInstanceCopy;

        }

        private void updateIttrStats()
        {

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

            simInstanceCopy.simEnvironment.SetOptimizerParameters(bee.Position);

            previewPlot.SetSimulationInstance(simInstanceCopy);
            FrameData frm = new FrameData();
            frm.Obstacles = simInstanceCopy.simEnvironment.GetObstacleLines();
            previewPlot.OptimizerFrame = frm;
            previewPlot.ForceRedraw();

            updateIttrStats();
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

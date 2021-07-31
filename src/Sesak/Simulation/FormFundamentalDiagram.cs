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
using WeifenLuo.WinFormsUI.Docking;

namespace Sesak.Simulation
{
    public partial class FormFundamentalDiagram : DockContent
    {
        public List<PointF>[] dataset = new List<PointF>[3] { new List<PointF>(), new List<PointF>(), new List<PointF>() };
        
        public FormFundamentalDiagram()
        {
            InitializeComponent();
        }

        private void FormFundamentalDiagram_Load(object sender, EventArgs e)
        {

        }

        public void UpdateData()
        {
            
            chartSpeedDensity.Series[0].Points.Clear();
            for (int i = 0;i< dataset[0].Count;i++)
            {
                chartSpeedDensity.Series[0].Points.AddXY(dataset[0][i].X, dataset[0][i].Y);
            }

            chartFlowDensity.Series[0].Points.Clear();
            for (int i = 0; i < dataset[1].Count; i++)
            {
                chartFlowDensity.Series[0].Points.AddXY(dataset[1][i].X, dataset[1][i].Y);
            }

            chartEvacuationTime.Series[0].Points.Clear();
            for (int i = 0; i < dataset[2].Count; i++)
            {
                chartEvacuationTime.Series[0].Points.AddXY(dataset[2][i].X, dataset[2][i].Y);
            }
        }

        private void exportDatapointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int selected = tabControl1.SelectedIndex;

            if (selected < 0 || selected > 2)
                return;

            string[] headers = new string[] { "Crowd Density,Average Speed", "Crowd Density,Flow", "Time,Evacuated" };
            string[] titles = new string[] { "Average Speed vs Crowd Density", "Flow vs Crowd Density", "Crowd Evacuated vs Time" };

            List<PointF> datapoints = new List<PointF>();
            datapoints.AddRange(dataset[selected]);
            
            using(SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Title = "Export " + titles[selected];
                dlg.Filter = "CSV|*.csv|All Files|*";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    string s = "";
                    s = headers[selected] + Environment.NewLine;
                    for(int i = 0;i<datapoints.Count;i++)
                    {
                        s += datapoints[i].X.ToString() + "," + datapoints[i].Y.ToString() +  Environment.NewLine;
                        
                    }
                    File.WriteAllText(dlg.FileName, s);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
                }
                
            }
        }
        
    }
}

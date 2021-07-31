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

namespace Sesak.Optimizer
{
    public partial class FormLogGraph : DockContent
    {
        public Tuple<double, double>[] ResultData;
        public FormLogGraph()
        {
            InitializeComponent();
        }

        private void FormLogGraph_Load(object sender, EventArgs e)
        {
            if (ResultData == null || ResultData.Length <= 0)
                return;

            //update chart
            for (int i = 0; i < ResultData.Length; i++)
            {
                chartMain.Series[0].Points.AddXY(i + 1, ResultData[i].Item1);
            }

        }
    }
}

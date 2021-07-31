using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak
{
    public partial class FormMain : Form
    {
        FormEditor frmEditor = null;
        FormOptimizer frmOptimizer = null;

        public FormMain()
        {
            InitializeComponent();
        }

        private void btnFormEditor_Click(object sender, EventArgs e)
        {
            frmEditor = new FormEditor();
            frmEditor.Disposed += FrmEditor_Disposed;
            frmEditor.FormClosed += FrmEditor_FormClosed;
            this.Visible = false;
            frmEditor.Show();
            
        }

        private void FrmEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = true;
        }

        private void FrmEditor_Disposed(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void btnOptimizer_Click(object sender, EventArgs e)
        {
            frmOptimizer = new FormOptimizer();
            frmOptimizer.Disposed += FrmEditor_Disposed;
            frmOptimizer.FormClosed += FrmEditor_FormClosed;
            this.Visible = false;
            frmOptimizer.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

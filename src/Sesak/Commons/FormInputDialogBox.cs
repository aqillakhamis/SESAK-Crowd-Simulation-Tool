using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak.Commons
{
    public partial class FormInputDialogBox : Form
    {
        public string InputText { get { return txtInput.Text; } set { txtInput.Text = value; } }
        public string Title { get { return Text; } set { Text = value; } }
        public string Message { get { return lblMessage.Text; } set { lblMessage.Text = value; } }
        public FormInputDialogBox()
        {
            InitializeComponent();
        }
        public FormInputDialogBox(string title,string message,string defaultValue)
        {
            InitializeComponent();
            Text = title;
            lblMessage.Text = message;
            txtInput.Text = defaultValue;
            
        }
        private void FormInputDialogBox_Load(object sender, EventArgs e)
        {
            txtInput.Focus();
            txtInput.SelectAll();
        }
    }
}

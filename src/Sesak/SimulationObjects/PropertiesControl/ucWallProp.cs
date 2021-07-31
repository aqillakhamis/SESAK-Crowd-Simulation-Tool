using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak.SimulationObjects.PropertiesControl
{
    public partial class ucWallProp : UserControl, IPropControl
    {
        public event Action<object> OnPropertiesChanged;
        public event Action<object> OnPropertiesNameChanged;
        Wall propObj = null;

        
        public PointF P1
        {
            get
            {
                if (propObj == null)
                    return new PointF();

                return propObj.P1;
            }
            set
            {
                if (propObj == null)
                    return;

                propObj.SetPoints(value, P2,0);
                if(OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }
        public PointF P2
        {
            get
            {
                if (propObj == null)
                    return new PointF();

                return propObj.P2;
            }
            set
            {
                if (propObj == null)
                    return;

                propObj.SetPoints(P1, value, 1);

                if(OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }

        public ucWallProp()
        {
            InitializeComponent();
        }

        

        public void SetObject(object obj)
        {
            if(obj is Wall)
            {
                propObj = (Wall)obj;

                RefreshValues();
            }
        }

        #region Field_Validate
        private void txtX1_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = P1.X;

            if (!float.TryParse(txt.Text,out v))
                v = prevValue;
            

            if (v != prevValue)
            {
                P1 = new PointF(v, P1.Y);
                v = P1.X;
            }
            txt.Text = v.ToString();

        }

        private void txtY1_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = P1.Y;

            if (!float.TryParse(txt.Text, out v))
                v = prevValue;


            if (v != prevValue)
            {
                P1 = new PointF(P1.X,v);
                v = P1.Y;
            }
            txt.Text = v.ToString();
        }

        private void txtX2_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = P2.X;

            if (!float.TryParse(txt.Text, out v))
                v = prevValue;


            if (v != prevValue)
            {
                P2 = new PointF(v, P2.Y);
                v = P2.X;
            }
            txt.Text = v.ToString();
        }

        private void txtY2_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = P2.Y;

            if (!float.TryParse(txt.Text, out v))
                v = prevValue;


            if (v != prevValue)
            {
                P2 = new PointF(P2.X, v);
                v = P2.Y;
            }
            txt.Text = v.ToString();
        }

        #endregion

        private void txtX1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtX1.Tag != null)
            {
                e.Handled = true;
                Keys k = (Keys)txtX1.Tag;

                switch (k)
                {
                    case Keys.Enter:
                        txtX1_Validated(sender, null);
                        break;
                    case Keys.Escape:
                        txtX1.Text = P1.X.ToString();
                        break;
                }
                txtX1.Tag = null;
            }
        }
        private void txtY1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtY1.Tag != null)
            {
                e.Handled = true;
                Keys k = (Keys)txtY1.Tag;
                
                switch(k)
                {
                    case Keys.Enter:
                        txtY1_Validated(sender, null);
                    break;
                    case Keys.Escape:
                        txtY1.Text = P1.Y.ToString();
                        break;
                }
                txtY1.Tag = null;
            }
        }



        private void txtX2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtX2.Tag != null)
            {
                e.Handled = true;
                Keys k = (Keys)txtX2.Tag;

                switch (k)
                {
                    case Keys.Enter:
                        txtX2_Validated(sender, null);
                        break;
                    case Keys.Escape:
                        txtX2.Text = P2.X.ToString();
                        break;
                }
                txtX2.Tag = null;
            }
        }

        private void txtY2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtY2.Tag != null)
            {
                e.Handled = true;
                Keys k = (Keys)txtY2.Tag;

                switch (k)
                {
                    case Keys.Enter:
                        txtY2_Validated(sender, null);
                        break;
                    case Keys.Escape:
                        txtY2.Text = P2.Y.ToString();
                        break;
                }
                txtY2.Tag = null;
            }
        }
        private void TextBoxField_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                txt.Tag = e.KeyCode;
            }
        }

        public void RefreshValues()
        {
            txtX1.Text = P1.X.ToString();
            txtY1.Text = P1.Y.ToString();

            txtX2.Text = P2.X.ToString();
            txtY2.Text = P2.Y.ToString();
        }
    }
}

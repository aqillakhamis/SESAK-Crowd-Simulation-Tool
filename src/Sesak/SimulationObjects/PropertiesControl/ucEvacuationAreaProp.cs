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
    public partial class ucEvacuationAreaProp : UserControl, IPropControl
    {
        public event Action<object> OnPropertiesChanged;
        public event Action<object> OnPropertiesNameChanged;
        EvacuationArea propObj = null;

        public ucEvacuationAreaProp()
        {
            InitializeComponent();
        }

        public RectangleF Area { get { return propObj.Area; } }

        public void SetObject(object obj)
        {
            if(obj is EvacuationArea)
            {
                propObj = (EvacuationArea)obj;

                RefreshValues();
            }
        }

        public void RefreshValues()
        {
            txtTop.Text = Area.Bottom.ToString();
            txtLeft.Text = Area.Left.ToString();
            txtRight.Text = Area.Right.ToString();
            txtBottom.Text = Area.Top.ToString();
        }



        private void txtTop_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            if (!float.TryParse(txt.Text, out v))
            {
                v = Area.Bottom; //y-axis reversed
            }
            else
            {
                if(propObj.SetTop(v))
                    propObj.Recalculate();

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(this);
            }
            RefreshValues();
        }

        private void txtLeft_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            if (!float.TryParse(txt.Text, out v))
            {
                v = Area.Left; //y-axis reversed
            }
            else
            {
                if(propObj.SetLeft(v))
                    propObj.Recalculate();

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(this);
            }
            RefreshValues();
        }

        private void txtBottom_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            if (!float.TryParse(txt.Text, out v))
            {
                v = Area.Top; //y-axis reversed
            }
            else
            {
                if(propObj.SetBottom(v))
                    propObj.Recalculate();
                
                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(this);
            }
            RefreshValues();
        }

        private void txtRight_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            if (!float.TryParse(txt.Text, out v))
            {
                v = Area.Right;
            }
            else
            {
                if (propObj.SetRight(v))
                    propObj.Recalculate();

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(this);
            }
            RefreshValues();
        }

        private void TextBoxField_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                txt.Tag = e.KeyCode;
            }
        }

        private void TextBoxField_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if(txt.Tag == null)
            {
                return;
            }

            e.Handled = true;

            Keys key = (Keys)txt.Tag;

            switch (key)
            {
                case Keys.Enter:
                    txt.Parent.Focus();
                    txt.Focus();
                    break;
                case Keys.Escape:
                    RefreshValues();
                    break;
            }
            txt.Tag = null;
        }
    }
}

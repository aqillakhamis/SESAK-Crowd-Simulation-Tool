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
    public partial class ucDoorProp : UserControl, IPropControl
    {
        public event Action<object> OnPropertiesChanged;
        public event Action<object> OnPropertiesNameChanged;

        Door propObj = null;


        public string ObjectName
        {
            get
            {
                if (propObj == null)
                    return "";
                return propObj.Name;
            }
            set
            {
                if (propObj == null)
                    return;

                propObj.Name = value;

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);

                if (OnPropertiesNameChanged != null)
                    OnPropertiesNameChanged(propObj);
            }
        }
        
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

        public float DoorWidthMeter
        {
            get {
                if (propObj == null)
                    return 0;

                return propObj.DoorWidth;
            }
            set 
            {
                if (propObj == null)
                    return;

                propObj.DoorWidth = value;

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }

        public float DoorWidthPercent
        {
            get
            {
                if (propObj == null)
                    return 0;
                return propObj.DoorWidthPercentage;
            }
            set {
                if (propObj == null)
                    return;

                propObj.DoorWidthPercentage = value;
                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }

        public float DoorOffsetMeter
        {
            get
            {
                if (propObj == null)
                    return 0;

                return propObj.DoorPositionMeter;
            }
            set
            {
                if (propObj == null)
                    return;

                propObj.DoorPositionMeter = value;

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }

        public float DoorOffsetPercent
        {
            get
            {
                if (propObj == null)
                    return 0;
                return propObj.DoorPosition;
            }
            set
            {
                if (propObj == null)
                    return;

                propObj.DoorPosition = value;
                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }
        public ucDoorProp()
        {
            InitializeComponent();
        }

        

        public void SetObject(object obj)
        {
            if(obj is Door)
            {
                propObj = (Door)obj;

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

            txtDoorOffset.Text = DoorOffsetMeter.ToString();
            txtDoorOffsetPercent.Text = (DoorOffsetPercent * 100).ToString();
            txtDoorWidth.Text = DoorWidthMeter.ToString();
            txtDoorWidthPercent.Text = (DoorWidthPercent * 100).ToString();

            txtLabel.Text = ObjectName;
        }

        private void txtY1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDoorWidth_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = DoorWidthMeter;

            if (!float.TryParse(txt.Text, out v))
                v = prevValue;


            if (v != prevValue)
            {
                DoorWidthMeter = v;
                v = DoorWidthMeter;
            }
            txt.Text = v.ToString();
            txtDoorWidthPercent.Text = (DoorWidthPercent * 100).ToString();
            txtDoorOffsetPercent.Text = (DoorOffsetPercent * 100).ToString();
            txtDoorOffset.Text = DoorOffsetMeter.ToString();
        }

        private void txtDoorWidthPercent_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = DoorWidthPercent;

            if (!float.TryParse(txt.Text, out v))
                v = prevValue;
            else
                v /= 100;

            if (v != prevValue)
            {
                DoorWidthPercent = v;
                v = DoorWidthPercent;
            }
            txt.Text = (v * 100).ToString();
            txtDoorWidth.Text = DoorWidthMeter.ToString();
            txtDoorOffsetPercent.Text = (DoorOffsetPercent * 100).ToString();
            txtDoorOffset.Text = DoorOffsetMeter.ToString();
        }

        private void txtDoorOffset_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = DoorOffsetMeter;

            if (!float.TryParse(txt.Text, out v))
                v = prevValue;


            if (v != prevValue)
            {
                DoorOffsetMeter = v;
                v = DoorOffsetMeter;
            }
            txt.Text = v.ToString();
            txtDoorOffsetPercent.Text = (DoorOffsetPercent * 100).ToString();

        }

        private void txtDoorOffsetPercent_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = DoorOffsetPercent;

            if (!float.TryParse(txt.Text, out v))
                v = prevValue;
            else
                v /= 100;

            if (v != prevValue)
            {
                DoorOffsetPercent = v;
                v = DoorOffsetPercent;
            }
            txt.Text = (v * 100).ToString();
            txtDoorOffset.Text = DoorOffsetMeter.ToString();
        }

        private void txtDoorWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDoorWidth.Tag != null)
            {
                e.Handled = true;
                Keys k = (Keys)txtDoorWidth.Tag;

                switch (k)
                {
                    case Keys.Enter:
                        txtDoorWidth_Validated(sender, null);
                        break;
                    case Keys.Escape:
                        txtDoorWidth.Text = DoorWidthMeter.ToString();
                        break;
                }
                txtDoorWidth.Tag = null;
            }
        }

        private void txtDoorWidthPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDoorWidthPercent.Tag != null)
            {
                e.Handled = true;
                Keys k = (Keys)txtDoorWidthPercent.Tag;

                switch (k)
                {
                    case Keys.Enter:
                        txtDoorWidthPercent_Validated(sender, null);
                        break;
                    case Keys.Escape:
                        txtDoorWidthPercent.Text = (DoorWidthPercent * 100).ToString();
                        break;
                }
                txtDoorWidthPercent.Tag = null;
            }
        }

        private void txtDoorOffset_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDoorOffset.Tag != null)
            {
                e.Handled = true;
                Keys k = (Keys)txtDoorOffset.Tag;

                switch (k)
                {
                    case Keys.Enter:
                        txtDoorOffset_Validated(sender, null);
                        break;
                    case Keys.Escape:
                        txtDoorOffset.Text = DoorOffsetMeter.ToString();
                        break;
                }
                txtDoorOffset.Tag = null;
            }
        }

        private void txtDoorOffsetPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDoorOffsetPercent.Tag != null)
            {
                e.Handled = true;
                Keys k = (Keys)txtDoorOffsetPercent.Tag;

                switch (k)
                {
                    case Keys.Enter:
                        txtDoorOffsetPercent_Validated(sender, null);
                        break;
                    case Keys.Escape:
                        txtDoorOffsetPercent.Text = (DoorOffsetPercent * 100).ToString();
                        break;
                }
                txtDoorOffsetPercent.Tag = null;
            }
        }

        private void txtLabel_Validated(object sender, EventArgs e)
        {
            if (txtLabel.Text.Length <= 0)
                txtLabel.Text = "Door";


            ObjectName = txtLabel.Text;
                

        }
    }
}

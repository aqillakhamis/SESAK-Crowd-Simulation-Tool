using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sesak.Commons;

namespace Sesak.SimulationObjects.PropertiesControl
{
    public partial class ucAgentProp : UserControl, IPropControl
    {
        public event Action<object> OnPropertiesChanged;
        public event Action<object> OnPropertiesNameChanged;
        Agent propObj = null;

        
        public PointF P1
        {
            get
            {
                if (propObj == null)
                    return new PointF();

                return propObj.StartPosition;
            }
            set
            {
                if (propObj == null)
                    return;

                propObj.StartPosition = value;
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

                return propObj.Destination;
            }
            set
            {
                if (propObj == null)
                    return;

                propObj.Destination = value;

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }

        public bool UseDefaultAgentParameters
        {
            get
            {
                if (propObj == null)
                    return true;

                return propObj.DefaultAgentParameters;
            }
            set
            {
                propObj.DefaultAgentParameters = value;

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }

        public float WalkSpeed
        {
            get
            {
                if (propObj == null)
                    return 0;

                return propObj.WalkSpeed;
            }
            set
            {
                propObj.WalkSpeed = value;

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }

        public float BodyRadius
        {
            get
            {
                if (propObj == null)
                    return 0;

                return propObj.BodyRadius;
            }
            set
            {
                propObj.BodyRadius = value;

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }

        public float Heading
        {
            get
            {
                if (propObj == null)
                    return 0;

                return propObj.Heading;
            }
            set
            {
                propObj.Heading = value;

                if (OnPropertiesChanged != null)
                    OnPropertiesChanged(propObj);
            }
        }
        public ucAgentProp()
        {
            InitializeComponent();
            /*
            toolTips.SetToolTip(txtBodyRadius, "Agent Body Radius");
            toolTips.SetToolTip(txtWalkSpeed, "Agent Maximum Walk Speed");


            toolTips.SetToolTip(txtX1, "Agent Start Position (X)");
            toolTips.SetToolTip(txtX2, "Agent Destination (X)");

            toolTips.SetToolTip(txtY1, "Agent Start Position (Y)");
            toolTips.SetToolTip(txtY2, "Agent Destination (Y)");
            */
        }

        

        public void SetObject(object obj)
        {
            if(obj is Agent)
            {
                propObj = (Agent)obj;

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

            txtBodyRadius.Text = BodyRadius.ToString();
            txtStartHeading.Text = MathHelper.RadianToDegree(Heading).ToString();

            txtWalkSpeed.Text = WalkSpeed.ToString();
            chkUseDefaultAgentParameters.Checked = UseDefaultAgentParameters;

            updateParameterSwitch();
        }

        private void txtStartHeading_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtStartHeading_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = (float)MathHelper.RadianToDegree(Heading);

            if (!float.TryParse(txt.Text, out v))
                v = prevValue;


            if (v != prevValue)
            {
                Heading = (float)MathHelper.DegreeToRadian(v);

                v = (float)MathHelper.RadianToDegree(Heading);
            }
            txt.Text = v.ToString();
        }

        private void txtBodyRadius_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = BodyRadius;

            if (!float.TryParse(txt.Text, out v))
                v = prevValue;


            if (v != prevValue)
            {
                BodyRadius = v;
                v = BodyRadius;
            }

            txt.Text = v.ToString();
        }

        private void txtStartHeading_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtStartHeading.Tag != null)
            {
                e.Handled = true;
                Keys k = (Keys)txtStartHeading.Tag;

                switch (k)
                {
                    case Keys.Enter:
                        txtStartHeading_Validated(sender, null);
                        break;
                    case Keys.Escape:
                        txtStartHeading.Text = MathHelper.RadianToDegree(Heading).ToString();
                        break;
                }
                txtStartHeading.Tag = null;
            }
        }

        private void txtBodyRadius_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtBodyRadius.Tag != null)
            {
                e.Handled = true;
                Keys k = (Keys)txtBodyRadius.Tag;

                switch (k)
                {
                    case Keys.Enter:
                        txtBodyRadius_Validated(sender, null);
                        break;
                    case Keys.Escape:
                        txtBodyRadius.Text = BodyRadius.ToString();
                        break;
                }
                txtBodyRadius.Tag = null;
            }
        }

        private void txtWalkSpeed_Validated(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            float v;
            float prevValue = WalkSpeed;

            if (!float.TryParse(txt.Text, out v))
                v = prevValue;


            if (v != prevValue)
            {
                WalkSpeed = v;
                v = WalkSpeed;
            }

            txt.Text = v.ToString();
        }

        private void chkUseDefaultAgentParameters_CheckedChanged(object sender, EventArgs e)
        {
            UseDefaultAgentParameters = chkUseDefaultAgentParameters.Checked;
            updateParameterSwitch();
        }
        private void updateParameterSwitch()
        {

            grpParameters.Visible = !UseDefaultAgentParameters;
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ucAgentProp_Load(object sender, EventArgs e)
        {

        }

        private void toolTips_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}

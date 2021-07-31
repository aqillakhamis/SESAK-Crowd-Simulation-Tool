using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak.Simulation
{
    public partial class FormSimulationParameter : Form
    {

        private double timeStep;
        public double TimeStep
        {
            get
            {
                return timeStep;
            }
            set
            {
                timeStep = value;
                if (timeStep < 0.0000001)
                    timeStep = 0.0000001;
                else if (timeStep > 0.1)
                    timeStep = 0.1;
            }
        }
        private const double DefaultTimeStep = 0.01;


        private double a;
        public double A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
                if (a < 1)
                    a = 1;
                else if (a > 10000)
                    a = 10000;
            }
        }
        private const double DefaultA = 0.01;

        private double b;
        public double B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;

                if (b < 0.001)
                    b = 0.001;
                else if (b > 10)
                    b = 10;
            }
        }
        private const double DefaultB = 0.01;

        private double kappa;
        public double Kappa
        {
            get
            {
                return kappa;
            }
            set
            {
                kappa = value;
                if (kappa < 1)
                    kappa = 1;
                else if (kappa > 500000)
                    kappa = 500000;
            }
        }
        private const double DefaultKappa = 0.01;

        private double k;
        public double K
        {
            get
            {
                return k;
            }
            set
            {
                k = value;
                if (k < 1)
                    k = 1;
                else if (k > 500000)
                    k = 500000;
            }
        }
        private const double DefaultK = 0.01;


        private double agentWalkSpeed;
        public double AgentWalkSpeed
        {
            get
            {
                return agentWalkSpeed;
            }
            set
            {
                agentWalkSpeed = value;
                if (agentWalkSpeed < 0.1)
                    agentWalkSpeed = 0.1;
                else if (agentWalkSpeed > 100)
                    agentWalkSpeed = 100;
            }
        }
        private const double DefaultAgentWalkSpeed = 1.5;

        private double agentBodyRadius;
        public double AgentBodyRadius
        {
            get
            {
                return agentBodyRadius;
            }
            set
            {
                agentBodyRadius = value;
                if (agentBodyRadius < 0.1)
                    agentBodyRadius = 0.1;
                else if (agentBodyRadius > 3)
                    agentBodyRadius = 3;
            }
        }
        private const double DefaultAgentBodyRadius = 0.25;

        public FormSimulationParameter()
        {
            InitializeComponent();
        }

        private void FormSimulationParameter_Load(object sender, EventArgs e)
        {
            UpdateTextFields();
        }

        private void UpdateTextFields()
        {
            txtTimeStep.Text = TimeStep.ToString();
            txtA.Text = A.ToString();
            txtB.Text = B.ToString();
            txtkappa.Text = Kappa.ToString();
            txtk.Text = k.ToString();
            txtWalkSpeed.Text = AgentWalkSpeed.ToString();
            txtBodyRadius.Text = AgentBodyRadius.ToString();
        }
        


        private void txtTimeStep_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length == 0)
                tb.Text = DefaultTimeStep.ToString();

            double v;
            if(!double.TryParse(tb.Text,out v))
            {
                v = TimeStep;
            }
            else
            {
                TimeStep = v;
            }

            tb.Text = TimeStep.ToString();

        }

        private void txtA_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text.Length == 0)
                tb.Text = DefaultA.ToString();

            double v;
            if (!double.TryParse(tb.Text, out v))
            {
                v = A;
            }
            else
            {
                A = v;
            }

            tb.Text = A.ToString();
        }

        private void txtB_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double v;
            if (tb.Text.Length == 0)
                tb.Text = DefaultB.ToString();

            if (!double.TryParse(tb.Text, out v))
            {
                v = B;
            }
            else
            {
                B = v;
            }

            tb.Text = B.ToString();
        }

        private void txtkappa_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double v;
            if (tb.Text.Length == 0)
                tb.Text = DefaultKappa.ToString();

            if (!double.TryParse(tb.Text, out v))
            {
                v = Kappa;
            }
            else
            {
                Kappa = v;
            }

            tb.Text = Kappa.ToString();
        }

        private void txtk_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double v;
            if (tb.Text.Length == 0)
                tb.Text = DefaultK.ToString();

            if (!double.TryParse(tb.Text, out v))
            {
                v = K;
            }
            else
            {
                K = v;
            }

            tb.Text = K.ToString();
        }


        public void LoadParameter(SimulationParameters simParam)
        {
            TimeStep = simParam.dt;
            A = simParam.A;
            B = simParam.B;
            Kappa = simParam.kappa;
            K = simParam.k;
            AgentWalkSpeed = simParam.AgentSpeed;
            AgentBodyRadius = simParam.AgentBodyRadius;
        }

        public void ApplyParameter(ref SimulationParameters simParam)
        {
            simParam.dt = TimeStep;
            simParam.A = A;
            simParam.B = B;
            simParam.kappa = Kappa;
            simParam.k = K;
            simParam.AgentSpeed = AgentWalkSpeed;
            simParam.AgentBodyRadius = AgentBodyRadius;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            SimulationParameters simParamDefault = new SimulationParameters();
            LoadParameter(simParamDefault);
            UpdateTextFields();
        }

        private void txtWalkSpeed_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double v;
            if (tb.Text.Length == 0)
                tb.Text = DefaultAgentWalkSpeed.ToString();

            if (!double.TryParse(tb.Text, out v))
            {
                v = AgentWalkSpeed;
            }
            else
            {
                AgentWalkSpeed = v;
            }

            tb.Text = AgentWalkSpeed.ToString();
        }

        private void txtBodyRadius_Validated(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double v;
            if (tb.Text.Length == 0)
                tb.Text = DefaultAgentBodyRadius.ToString();

            if (!double.TryParse(tb.Text, out v))
            {
                v = AgentBodyRadius;
            }
            else
            {
                AgentBodyRadius = v;
            }

            tb.Text = AgentBodyRadius.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }
    }
}

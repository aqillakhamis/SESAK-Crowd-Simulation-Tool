using Newtonsoft.Json;
using Sesak.Commons;
using Sesak.Optimizer;
using Sesak.Path;
using Sesak.Simulation;
using Sesak.SimulationObjects;
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

namespace Sesak
{
    public partial class FormOptimizer : DockContent
    {
        //public SimulationInstance SimInstance = new SimulationInstance();
        //public SimulationParameters simParam = new SimulationParameters();
        //SimulationEnvironment simEnvironment = new SimulationEnvironment();

        
        ZoneData[] zones;
        OldAgent[] agents = null;
        OldObstacle[] obstacles;
        Line[] obsLine = null;

        int OptimizerParameterCount = 2;


        public FormOptimizer()
        {
            InitializeComponent();
        }


        private void btnBrowseEnvironment_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Sim Environment|*.jsv|All Files|*";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    string s = File.ReadAllText(dlg.FileName);
                    OptimizerWorkspace.Instance.SimEnvironment.Import(s);

                    OptimizerWorkspace.Instance.UpdateZoneMap();
                    RefreshEnvironmentStats();
                    
                    txtEnvironment.Text = dlg.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Open", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void RefreshEnvironmentStats()
        {
            SimulationEnvironment simEnvironment = OptimizerWorkspace.Instance.SimEnvironment;
            
            txtAgentCount.Text = simEnvironment.Agents.Count.ToString();
            txtDoorCount.Text = simEnvironment.Doors.Count.ToString();


            lstStaticDoor.Items.Clear();

            foreach(Door door in simEnvironment.Doors)
            {
                lstStaticDoor.Items.Add(door);
            }
        }

        private void btnRunOptimizer_Click(object sender, EventArgs e)
        {
            SimulationEnvironment simEnvironment = OptimizerWorkspace.Instance.SimEnvironment;
            SimulationInstance simInstance = OptimizerWorkspace.Instance.SimInstance;
            SimulationParameters simParam = OptimizerWorkspace.Instance.SimParameter;

            //check agent exist
            if (simEnvironment.Agents.Count <= 0)
            {
                MessageBox.Show("No agent loaded", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            //check door optimize
            {
                if(OptimizerParameterCount <= 0)
                {
                    MessageBox.Show("Invalid Y Parameter Count", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                int[] optimizeDoorCount = new int[OptimizerParameterCount];

                foreach(Door door in simEnvironment.Doors)
                {
                    if (door.Optimizer)
                    {
                        if(door.OptimizerIndex >= OptimizerParameterCount || door.OptimizerIndex < 0)
                        {
                            MessageBox.Show("Invalid door parameter index", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        optimizeDoorCount[door.OptimizerIndex]++;
                    }
                }
                
                int paramError = -1;
                for(int i = 0;i<optimizeDoorCount.Length;++i)
                {
                    if (optimizeDoorCount[i] == 0)
                    {
                        paramError = i;
                        break;
                    }
                }
                if(paramError >= 0)
                {
                    MessageBox.Show("No door assigned to parameter index [" + paramError.ToString() + "]", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            }
            simInstance.ResetTimer();
            simInstance.SimParam = simParam;
            simInstance.simEnvironment = simEnvironment;

            ABCOptimizer.MultiObjective = chkMultiObjective.Checked;
            ABCOptimizer.Y_PARAMCOUNT = OptimizerParameterCount;

            //FormOptimizerLogs frm = new FormOptimizerLogs();
            FormOptimizerLogs frm = FormOptimizerWorkspace.Instance.frmOptimizerLogs;

            frm.optimizer.MaxIt = int.Parse(MaxIterationInput.Text);
            frm.optimizer.L = int.Parse(LimitInput.Text);
            frm.optimizer.nPop = int.Parse(ColonySizeInput.Text);
            frm.optimizer.nOnlooker = int.Parse(OnlookerInput.Text);

            frm.WorkerCount = int.Parse(cmbWorkerThread.SelectedItem.ToString());
            frm.ShowPlot = chkShowSimPlot.Checked;
            frm.SimInstanceTemplate = simInstance;
            //frm.ShowDialog();
            this.DockState = DockState.DockLeftAutoHide;
            frm.RunOptimizer();
            
        }

        private void FormOptimizer_Load(object sender, EventArgs e)
        {
            OptimizerWorkspace.Instance.LoadLastSimParameter();
            cmbWorkerThread.SelectedItem = "2";
            UpdateYIndexComboBox();
        }

    
        private void txtYParameterCount_Validated(object sender, EventArgs e)
        {
            TextBox tb = txtYParameterCount;

            int v;
            if(!int.TryParse(tb.Text,out v))
            {
                v = OptimizerParameterCount;
            }
            else if(v < 1)
            {
                v = 1;
            }

            OptimizerParameterCount = v;

            tb.Text = v.ToString();

            UpdateYIndexComboBox();

        }

        private void UpdateYIndexComboBox()
        {
            string s = cmbYIndex.Text;
            cmbYIndex.Items.Clear();
            for (int i = 0; i < OptimizerParameterCount; i++)
            {
                cmbYIndex.Items.Add(i.ToString());
            }
            cmbYIndex.Text = s;
        }

        private void chkEnableOptimizer_CheckedChanged(object sender, EventArgs e)
        {
            

            Door door = (Door)lstStaticDoor.SelectedItem;
            if (door == null)
                return;

            door.Optimizer = chkEnableOptimizer.Checked;
            UpdateDoorOptimizerControl();
        }

        private void cmbYIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            Door door = (Door)lstStaticDoor.SelectedItem;
            if (door == null)
                return;

            int yIndex;
            if(int.TryParse(cmbYIndex.Text,out yIndex))
            {
                if (yIndex < 0)
                    yIndex = 0;

                door.OptimizerIndex = yIndex;
            }

            cmbYIndex.Text = door.OptimizerIndex.ToString();

            
        }

        private void lstStaticDoor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Door door = (Door)lstStaticDoor.SelectedItem;
            if (door == null)
                return;


            chkEnableOptimizer.Checked = door.Optimizer;
            cmbYIndex.Text = door.OptimizerIndex.ToString();
            UpdateDoorOptimizerControl();
        }

        void UpdateDoorOptimizerControl()
        {
            cmbYIndex.Enabled = chkEnableOptimizer.Checked;
        }

        private void MaxIterationInput_Validated(object sender, EventArgs e)
        {
            const int maxVal = 1000000;
            const int minVal = 1;
            const int defaultVal = 300;
            TextBox tb = (TextBox)sender;
            int v;
            if(!int.TryParse(tb.Text,out v))
            {
                v = defaultVal;
            }

            if (v < minVal)
                v = minVal;
            else if (v > maxVal)
                v = maxVal;

            tb.Text = v.ToString();
        }

        private void LimitInput_Validated(object sender, EventArgs e)
        {
            const int maxVal = 1000000;
            const int minVal = 1;
            const int defaultVal = 500;
            TextBox tb = (TextBox)sender;
            int v;
            if (!int.TryParse(tb.Text, out v))
            {
                v = defaultVal;
            }

            if (v < minVal)
                v = minVal;
            else if (v > maxVal)
                v = maxVal;

            tb.Text = v.ToString();
        }

        private void ColonySizeInput_Validated(object sender, EventArgs e)
        {
            const int maxVal = 1000000;
            const int minVal = 1;
            const int defaultVal = 50;
            TextBox tb = (TextBox)sender;
            int v;
            if (!int.TryParse(tb.Text, out v))
            {
                v = defaultVal;
            }

            if (v < minVal)
                v = minVal;
            else if (v > maxVal)
                v = maxVal;

            tb.Text = v.ToString();
        }

        private void OnlookerInput_Validated(object sender, EventArgs e)
        {
            const int maxVal = 1000000;
            const int minVal = 1;
            const int defaultVal = 50;
            TextBox tb = (TextBox)sender;
            int v;
            if (!int.TryParse(tb.Text, out v))
            {
                v = defaultVal;
            }

            if (v < minVal)
                v = minVal;
            else if (v > maxVal)
                v = maxVal;

            tb.Text = v.ToString();
        }

        private void btnSaveEnvironment_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "Sim Environment|*.jsv|All Files|*";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    File.WriteAllText(dlg.FileName, OptimizerWorkspace.Instance.SimEnvironment.Export());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSimulationParameter dlg = new FormSimulationParameter();
            SimulationParameters simParam = OptimizerWorkspace.Instance.SimParameter;

            dlg.LoadParameter(simParam);
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            dlg.ApplyParameter(ref simParam);

            OptimizerWorkspace.Instance.SaveLastSimParameter();
        }

        private void btnSimulationParameters_Click(object sender, EventArgs e)
        {
            FormSimulationParameter dlg = new FormSimulationParameter();
            SimulationParameters simParam = OptimizerWorkspace.Instance.SimParameter;

            dlg.LoadParameter(simParam);
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            dlg.ApplyParameter(ref simParam);

            OptimizerWorkspace.Instance.SaveLastSimParameter();
        }
    }
}

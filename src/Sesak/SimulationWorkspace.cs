using Newtonsoft.Json;
using Sesak.Commons;
using Sesak.Simulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak
{
    public class SimulationWorkspace
    {
        
        public event EventHandler OnSimulationStarted;
        public event EventHandler OnSimulationEnded;
        public event EventHandler OnPlottingOptionChanged;

        public event EventHandler<bool> OnExportingAction;



        private static SimulationWorkspace instance;
        public static SimulationWorkspace Instance { get { return instance; } }


        public SimulationParameters SimParameter = new SimulationParameters();
        public SimulationEnvironment SimEnvironment = new SimulationEnvironment();
        public SimulationInstance SimInstance = new SimulationInstance();

        public void ExportVideo()
        {
            if (OnExportingAction != null)
                OnExportingAction(this, true);
        }
        public void ExportImageSequence()
        {
            if (OnExportingAction != null)
                OnExportingAction(this, false);
        }

        private bool plotShowHeatmap = false;
        public bool PlotShowHeatmap
        {
            get { return plotShowHeatmap; }
            set
            {
                if (value == plotShowHeatmap)
                    return;

                plotShowHeatmap = value;
                if(OnPlottingOptionChanged != null)
                    OnPlottingOptionChanged(this, EventArgs.Empty);
            }
        }

        public static void Initialize()
        {
            instance = new SimulationWorkspace();

            //Load last simulation parameter from file
            instance.LoadLastSimParameter();
        }


        public void LoadLastSimParameter()
        {
            string fn = AppHelper.GetConfigPath("simParam.json");

            if (File.Exists(fn))
            {
                try
                {
                    string s = File.ReadAllText(fn);
                    SimulationParameters p = JsonConvert.DeserializeObject<SimulationParameters>(s);

                    SimParameter.A = p.A;
                    SimParameter.AgentBodyRadius = p.AgentBodyRadius;
                    SimParameter.AgentSpeed = p.AgentSpeed;
                    SimParameter.B = p.B;
                    SimParameter.Delta = p.Delta;

                    SimParameter.DrawInterval = p.DrawInterval;

                    SimParameter.dt = p.dt;
                    SimParameter.k = p.k;
                    SimParameter.kappa = p.kappa;
                    SimParameter.Mass = p.Mass;
                    SimParameter.ReachDestinationRange = p.ReachDestinationRange;


                }
                catch (Exception ex) { }
            }
        }


        public void SaveLastSimParameter()
        {
            string s = JsonConvert.SerializeObject(SimParameter);
            string fn = AppHelper.GetConfigPath("simParam.json");
            try
            {
                File.WriteAllText(fn, s);
            }
            catch (Exception ex) { }
        }


        public void RunSimulation()
        {
            SimInstance.Reset();

            SimInstance.LimitSimulationTiming = false;// chkTrySimulateRealTime.Checked;
            
            SimulationInstance.BatchSimulate = false;

            
            SimInstance.ResetTimer();
            SimInstance.SimParam = SimParameter;
            SimInstance.simEnvironment = SimEnvironment;
            SimInstance.StartSim();

            if (OnSimulationStarted != null)
                OnSimulationStarted(this, EventArgs.Empty);


        }
    }
}

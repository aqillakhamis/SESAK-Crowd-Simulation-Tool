using Newtonsoft.Json;
using Sesak.Commons;
using Sesak.Path;
using Sesak.Simulation;
using Sesak.SimulationObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak
{
    public class OptimizerWorkspace
    {
        private static OptimizerWorkspace instance;
        public static OptimizerWorkspace Instance { get { return instance; } }

        public SimulationInstance SimInstance = new SimulationInstance();
        public SimulationParameters SimParameter = new SimulationParameters();
        public SimulationEnvironment SimEnvironment = new SimulationEnvironment();

        ZoneMap zoneMap = new ZoneMap();

        public static void Initialize()
        {
            instance = new OptimizerWorkspace();
        }
        public void UpdateZoneMap()
        {
            zoneMap.GenerateZoneMap(SimEnvironment);
            InvalidateAgentWaypoints();
        }
        void InvalidateAgentWaypoints()
        {
            foreach (Agent agent in SimEnvironment.Agents)
            {

                agent.Waypoints = null;
            }
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


    }
}

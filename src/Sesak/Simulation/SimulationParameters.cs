using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SimulationParameters
    {
        public double DrawInterval { get; set; }
        [JsonProperty]
        public double Delta { get; set; }
        [JsonProperty]
        public double dt { get; set; }
        [JsonProperty]
        public double Mass { get; set; }
        [JsonProperty]
        public double AgentBodyRadius { get; set; }
        [JsonProperty]
        public double A { get; set; }
        [JsonProperty]
        public double B { get; set; }
        [JsonProperty]
        public double kappa { get; set; }
        [JsonProperty]
        public double k { get; set; }
        [JsonProperty]
        public double AgentSpeed { get; set; }
        
        public double ReachDestinationRange { get; set; }

        public SimulationParameters()
        {
            //default value
            DrawInterval = 1.0 / 30;
            Delta = 0.5; //Acceleration Time, reaction time, 0.5s 
            dt = 0.01; //Time Step 0.0001

            Mass = 58;
            AgentBodyRadius = 0.25;
            A = 998.97;                              //original value = 2000;
            B = 0.08;                              //original value = 0.08;
            kappa = 510.49;                        //original value = 240000;
            k = 819.62;                            //original value = 120000;
            AgentSpeed = 1.5;

            ReachDestinationRange = 2;
        }

        public SimulationParameters Copy()
        {
            SimulationParameters copy = new SimulationParameters();

            copy.DrawInterval = DrawInterval;
            copy.Delta = Delta;
            copy.dt = dt;
            copy.Mass = Mass;
            copy.AgentBodyRadius = AgentBodyRadius;
            copy.A = A;
            copy.B = B;
            copy.kappa = kappa;
            copy.k = k;
            copy.AgentSpeed = AgentSpeed;
            return copy;
        }

        public double[] ToArrayData(double ReachDestinationRangeValue)
        {

            //sync with optimized simulation
            const int index_DrawInterval = 0;
            const int index_Delta = 1;
            const int index_dt = 2;
            const int index_Mass = 3;
            const int index_AgentBodyRadius = 4;
            const int index_A = 5;
            const int index_B = 6;
            const int index_kappa = 7;
            const int index_k = 8;
            const int index_AgentSpeed = 9;
            const int index_ReachDestinationRange = 10;

            double[] val = new double[11];
            val[index_DrawInterval] = DrawInterval;


            val[index_Delta] = Delta;
            val[index_dt] = dt;
            val[index_Mass] = Mass;
            val[index_AgentBodyRadius] = AgentBodyRadius;
            val[index_A] = A;
            val[index_B] = B;
            val[index_kappa] = kappa;
            val[index_k] = k;
            val[index_AgentSpeed] = AgentSpeed;
            val[index_ReachDestinationRange] = ReachDestinationRangeValue;
            return val;

        }

    }
}

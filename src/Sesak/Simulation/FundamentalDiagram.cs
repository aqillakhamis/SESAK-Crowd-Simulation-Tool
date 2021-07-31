using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class FundamentalDiagram
    {
        public List<PointF> plotAvgSpeedvsCrowdDensity = new List<PointF>();
        public List<PointF> plotFlowvsCrowdDensity = new List<PointF>();
        private Dictionary<int, Tuple<int, float>> tempAvgSpeedvsCrowdDensity = new Dictionary<int, Tuple<int, float>>();


        public List<PointF> plotEvacuatedvsTime = new List<PointF>();
        private int lastEvacuatedCount = 0;

        /*
        public List<PointF> plotData = new List<PointF>();
        public List<PointF> adjPlotData = new List<PointF>();
        public Dictionary<int, Tuple<int, double>> adjPoint = new Dictionary<int, Tuple<int, double>>();
        */

        public event EventHandler OnDataUpdated;



        public void ClearAllData()
        {
            plotAvgSpeedvsCrowdDensity.Clear();
            tempAvgSpeedvsCrowdDensity.Clear();

            plotFlowvsCrowdDensity.Clear();

            plotEvacuatedvsTime.Clear();
            lastEvacuatedCount = 0;

        }
        /*
        public void _ProcessTick(TickData data,double timeDelta,RectangleF region)
        {
            double area = region.Width * region.Height;
            if (area <= 0)
                return;

            //Get all agent inside region
            List<AgentTickData> a = new List<AgentTickData>();
            
            foreach (AgentTickData agent in data.Agents)
            {
                if (region.Contains((float)agent.Position.X, (float)agent.Position.Y))
                    a.Add(agent);
            }
            if (a.Count <= 0)
                return; //No agent in region

            int agentCount = a.Count();

            
            double avgSpeed = 0;
            List<double> speed = new List<double>();
            foreach(AgentTickData agent in a)
            {
                speed.Add(agent.Velocity.GetMagnitude());
                
            }
            avgSpeed = speed.Average();

            if (avgSpeed <= 0)
                return;

            if(!adjPoint.ContainsKey(agentCount))
            {
                Tuple<int, double> t = new Tuple<int, double>(1,avgSpeed);
                adjPoint.Add(agentCount,t);
            }
            else
            {
                Tuple<int, double> t = adjPoint[agentCount];
                Tuple<int, double> newT = new Tuple<int, double>(t.Item1 + 1, t.Item2 + avgSpeed);
                adjPoint[agentCount] = newT;
            }


            adjPlotData.Clear();
            //reconstruct
            foreach (KeyValuePair<int, Tuple<int, double>> kp in adjPoint)
            {
                float x = (float)(kp.Key / area);
                float y = (float)(kp.Value.Item2 / kp.Value.Item1);
                adjPlotData.Add(new PointF(x, y));
            }


            if (OnDataUpdated != null)
                OnDataUpdated.Invoke(this, EventArgs.Empty);    
        }
        */
        public void ProcessTick(TickData data,SimulationInstance simInstance)
        {
            ProcessTickAvgSpeedvsCrowdDensity(data, simInstance);

            if (OnDataUpdated != null)
                OnDataUpdated.Invoke(this, EventArgs.Empty);
        }
        public void ProcessTickAvgSpeedvsCrowdDensity(TickData data, SimulationInstance simInstance)
        {
            
            RectangleF zoneTest = simInstance.simEnvironment.ComfortTestZone;
            float area = zoneTest.Width * zoneTest.Height;
            if (area <= 0)
                return; //no region defined, unable to compute crowd density

            //tempAvgSpeedvsCrowdDensity , agent in region as key
            
            //Get all agent inside region
            List<AgentTickData> a = new List<AgentTickData>();

            List<double> speed = new List<double>();
            int currentEvacuated = 0;
            foreach (AgentTickData agent in data.Agents)
            {
                if (zoneTest.Contains((float)agent.Position.X, (float)agent.Position.Y))
                {
                    a.Add(agent);
                    speed.Add(agent.Velocity.GetMagnitude());
                }

                if(agent.Evacuated)
                    currentEvacuated++;

            }

            if (a.Count <= 0)
                return; //No agent in region

            {
                float avgSpeed = (float)speed.Average();
                int agentCount = a.Count;

                //add datapoint into temp
                if (!tempAvgSpeedvsCrowdDensity.ContainsKey(agentCount))
                {
                    Tuple<int, float> t = new Tuple<int, float>(1, avgSpeed);
                    tempAvgSpeedvsCrowdDensity.Add(agentCount, t);
                }
                else
                {
                    Tuple<int, float> t = tempAvgSpeedvsCrowdDensity[agentCount];
                    Tuple<int, float> newT = new Tuple<int, float>(t.Item1 + 1, t.Item2 + avgSpeed);
                    tempAvgSpeedvsCrowdDensity[agentCount] = newT;
                }
            }

            //averaging datapoints, plotAvgSpeedvsCrowdDensity
            plotAvgSpeedvsCrowdDensity.Clear();
            plotFlowvsCrowdDensity.Clear();

            foreach (KeyValuePair<int, Tuple<int, float>> p in tempAvgSpeedvsCrowdDensity)
            {
                float density = p.Key / area;
                float avgSpeed = p.Value.Item2 / p.Value.Item1;

                plotAvgSpeedvsCrowdDensity.Add(new PointF(density,avgSpeed));

                if (avgSpeed > 0)
                {
                    float flow = p.Key / avgSpeed;
                    plotFlowvsCrowdDensity.Add(new PointF(density, flow));
                }
            }

            //evacuated
            if(currentEvacuated != lastEvacuatedCount)
            {
                lastEvacuatedCount = currentEvacuated;
                plotEvacuatedvsTime.Add(new PointF((float)data.Time, currentEvacuated));
            }

        }

        public void ProcessTickFlowvsCrowdDensity(TickData data,SimulationInstance simInstance)
        {

        }
    }
}

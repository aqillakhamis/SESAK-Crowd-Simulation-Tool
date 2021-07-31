using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class TickData
    {
        public double Time;
        public AgentTickData[] Agents;
        
        public string ToLine(int[] idx)
        {
            string s = Time.ToString();
            for (int i = 0; i < idx.Length; i++)
            {
                AgentTickData agent = new AgentTickData();
                agent.Index = -1;
                
                for (int j = 0; j < Agents.Length; j++)
                {
                    if(Agents[j].Index == idx[i])
                    {
                        agent = Agents[j];
                        break;
                    }
                }

                if(agent.Index >= 0)
                {
                    double speed = agent.Velocity.GetMagnitude();
                    s += ","
                        + agent.Position.X.ToString() + "," + agent.Position.Y.ToString() + ","
                        + agent.Velocity.X.ToString() + "," + agent.Velocity.Y.ToString() + ","
                        + speed.ToString();
                }
                else
                {
                    //s += ",P1,P2,V1,V2,SPEED"
                    s += ",,,,,";
                }
                
            }
            return s;
        }
    }
    public struct AgentTickData
    {
        public int Index;
        public bool Arrived;
        public bool Evacuated;
        public Vector2D Position;
        public Vector2D Velocity;
    }

    
}

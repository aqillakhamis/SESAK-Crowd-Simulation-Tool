using Sesak.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class FrameData
    {
        public double Runtime;
        public Vector2D[] AgentPositions;
        public float[] AgentHeadings;
        public float[] AgentBodyRadius;
        public Line[] Obstacles;


        public static FrameData Generate(double runtime, OldAgent[] agents, OldObstacle[] obstacles)
        {
            FrameData data = new FrameData();
            data.Runtime = runtime;

            if (agents != null)
            {
                data.AgentPositions = new Vector2D[agents.Length];
                data.AgentHeadings = new float[agents.Length];
                data.AgentBodyRadius = new float[agents.Length];
                for (int i = 0; i < agents.Length; i++)
                {
                    data.AgentPositions[i] = agents[i].Po;
                    if (agents[i].Vo.X == 0 && agents[i].Vo.Y == 0)
                        data.AgentHeadings[i] = 0;
                    else
                        data.AgentHeadings[i] = (float)Math.Atan2(agents[i].Vo.Y, agents[i].Vo.X);

                    data.AgentBodyRadius[i] = (float)agents[i].BodyRad;
                }
            }
            else
            {
                data.AgentPositions = new Vector2D[0];
                data.AgentHeadings = new float[0];
                data.AgentBodyRadius = new float[0];
            }

            if(obstacles != null)
            {
                data.Obstacles = new Line[obstacles.Length];

                for(int i = 0;i<obstacles.Length;i++)
                {
                    data.Obstacles[i] = new Line((float)obstacles[i].StartPos.X, (float)obstacles[i].StartPos.Y, (float)obstacles[i].EndPos.X, (float)obstacles[i].EndPos.Y);
                }
            }
            else
            {
                data.Obstacles = new Line[0];
            }

            return data;
        }
    }
}

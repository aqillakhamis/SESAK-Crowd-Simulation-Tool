using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class OldAgent
    {
        public Vector2D Po = new Vector2D(0, 0);
        public Vector2D Vo = new Vector2D(0, 0);
        public Vector2D StartPos = new Vector2D(0, 0);
        public Vector2D EndPos = new Vector2D(0, 0);

        public double BodyRad = 0.25;
        public double Vi = 1.00; //1.00 
        public double Mass = 80;
        public double MinRange = 1.00;
        public double Kad = 2;
        public double Epsilon = 1.0e-5;
        public Vector2D[] Waypoint;

        //var for simulation loop
        public bool ReachDestination = false;
        public bool Next_ReachDestination = false;
        public Vector2D TargetPos = new Vector2D(0, 0);
        public int CurrentZoneIndex = 0;

        public Vector2D Next_Vo = new Vector2D();
        public Vector2D Next_Po = new Vector2D();
        public int IndexTag = 0;

        //for average speed at corridor
        public double distanceTraveledInCorridor = 0;
        public List<double> velocityTraveledInCorridor = new List<double>();
        public double totalTimeTraveledInCorridor = 0;
        public bool startMeasureSpeed = false;

        public double tmpmul = 0;

        public int CurrentWaypointIndex = 0;

        public bool Evacuated = false;
        public double EvacuateTime = 0;

        public OldAgent Copy()
        {
            //create copy of agent (for simulation instance not overwrite loaded agent)
            OldAgent copy = new OldAgent();
            copy.Po = Po;
            copy.Vo = Vo;
            copy.StartPos = StartPos;
            copy.EndPos = EndPos;
            copy.BodyRad = BodyRad;
            copy.Vi = Vi;
            copy.Mass = Mass;
            copy.MinRange = MinRange;
            copy.Kad = Kad;
            copy.IndexTag = IndexTag;

            copy.totalTimeTraveledInCorridor = totalTimeTraveledInCorridor;
            copy.distanceTraveledInCorridor = distanceTraveledInCorridor;
            copy.startMeasureSpeed = startMeasureSpeed;
            copy.Evacuated = Evacuated;


            if (Waypoint == null)
                Waypoint = null;
            else
            {
                copy.Waypoint = new Vector2D[Waypoint.Length];
                for (int i = 0; i < Waypoint.Length; i++)
                {
                    copy.Waypoint[i] = Waypoint[i];
                }
            }
            return copy;
        }

        public void UpdateFromStruct(AgentStruct value, bool fullUpdate)
        {
            ReachDestination = value.ReachDestination != 0;
            Next_ReachDestination = value.Next_ReachDestination != 0;
            TargetPos = value.TargetPos;

            Po = value.Po;
            Vo = value.Vo;

            if (!fullUpdate)
                return;

            BodyRad = value.BodyRad;
            Vi = value.Vi;
            Mass = value.Mass;
            MinRange = value.MinRange;
            Kad = value.Kad;
            IndexTag = value.IndexTag;

            totalTimeTraveledInCorridor = value.totalTimeTraveledInCorridor;
            distanceTraveledInCorridor = value.distanceTraveledInCorridor;
            startMeasureSpeed = value.startMeasureSpeed;

        }
        public AgentStruct ToStruct()
        {
            //create copy of agent (for simulation instance not overwrite loaded agent)
            AgentStruct copy = new AgentStruct();
            copy.Po = Po;
            copy.Vo = Vo;


            copy.BodyRad = BodyRad;
            copy.Vi = Vi;
            copy.Mass = Mass;
            copy.MinRange = MinRange;
            copy.Kad = Kad;
            copy.IndexTag = IndexTag;


            copy.Next_Po = Next_Po;

            copy.Next_Vo = Next_Vo;

            copy.totalTimeTraveledInCorridor = totalTimeTraveledInCorridor;
            copy.distanceTraveledInCorridor = distanceTraveledInCorridor;
            copy.startMeasureSpeed = startMeasureSpeed;

            copy.ReachDestination = ReachDestination ? (byte)1 : (byte)0;
            copy.Next_ReachDestination = Next_ReachDestination ? (byte)1 : (byte)0;
            return copy;
        }

        public AgentTickData ToAgentTickData()
        {
            AgentTickData data = new AgentTickData();
            data.Index = IndexTag;
            data.Arrived = ReachDestination;
            data.Evacuated = Evacuated;
            data.Position = Po;
            data.Velocity = Vo;

            return data;
        }

        public override string ToString()
        {
            return "[" + StartPos.X.ToString() + "," + StartPos.Y.ToString() + "]";
        }
    }
}

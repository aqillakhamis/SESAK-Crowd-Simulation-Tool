using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class ZoneData
    {
        public string ZoneLabel;
        public double DoorOffset;
        public int DoorLocation; //0: WEST, 1:NORTH, 2: EAST, 3: SOUTH
        public Vector2D TopLeft;
        public Vector2D BottomRight;
        public int ExitZoneIndex;
        public int WallGeneration;
        public double DoorWidth = 1;

        public bool IsCorridor = false;

        //TEMP VALUE USE

        public Vector2D FarExitTargetPosition; //Target position when agent is not at the door
        public Vector2D NearExitTargetPosition; //target position when agent is in door region
        public Vector2D DoorCenterPosition;

        public override string ToString()
        {
            return ZoneLabel + "," + DoorLocation.ToString() + "," + ExitZoneIndex.ToString();
        }

        public bool IsInZone(Vector2D pt)
        {
            return (pt.X >= TopLeft.X && pt.X <= BottomRight.X && pt.Y >= BottomRight.Y && pt.Y <= TopLeft.Y);
        }
        public Vector2D GetExitTargetPosition(Vector2D position)
        {


            if (ExitZoneIndex == -1)
            {
                //last point, goto center
                return new Vector2D((TopLeft.X + BottomRight.X) / 2, (TopLeft.Y + BottomRight.Y) / 2);
            }
            else
            {

                /* 2 Point Exit
                double distance = SimHelper.GetMagnitude(position, FarExitTargetPosition);
                if (distance < DoorWidth/2)
                    return NearExitTargetPosition;
                else
                    return FarExitTargetPosition;
                */


                // 1 Point Exit
                return DoorCenterPosition;
            }
        }
        public ZoneData Copy()
        {

            //create copy of agent (for simulation instance not overwrite loaded agent)
            ZoneData copy = new ZoneData();
            copy.ZoneLabel = ZoneLabel;
            copy.DoorOffset = DoorOffset;
            copy.DoorLocation = DoorLocation; //0: WEST, 1:NORTH, 2: EAST, 3: SOUTH
            copy.TopLeft = new Vector2D(TopLeft.X, TopLeft.Y);
            copy.BottomRight = new Vector2D(BottomRight.X, BottomRight.Y);
            copy.ExitZoneIndex = ExitZoneIndex;
            copy.WallGeneration = WallGeneration;
            copy.DoorWidth = DoorWidth;
            copy.IsCorridor = IsCorridor;
            //TEMP VALUE USE

            copy.FarExitTargetPosition = new Vector2D(FarExitTargetPosition.X, FarExitTargetPosition.Y); //Target position when agent is not at the door
            copy.NearExitTargetPosition = new Vector2D(NearExitTargetPosition.X, NearExitTargetPosition.Y); //target position when agent is in door region
            copy.DoorCenterPosition = new Vector2D(DoorCenterPosition.X, DoorCenterPosition.Y);

            return copy;

        }
        public ZoneStruct ToStruct()
        {
            //create copy of agent (for simulation instance not overwrite loaded agent)
            ZoneStruct copy = new ZoneStruct();

            copy.DoorOffset = DoorOffset;
            copy.DoorLocation = (byte)DoorLocation; //0: WEST, 1:NORTH, 2: EAST, 3: SOUTH

            copy.TopLeft = TopLeft;
            copy.BottomRight = BottomRight;

            copy.ExitZoneIndex = ExitZoneIndex;
            copy.WallGeneration = WallGeneration;
            copy.DoorWidth = DoorWidth;
            copy.IsCorridor = IsCorridor;
            //TEMP VALUE USE

            copy.FarExitTargetPosition = FarExitTargetPosition;
            copy.NearExitTargetPosition = NearExitTargetPosition;
            copy.DoorCenterPosition = DoorCenterPosition;

            return copy;
        }
    }
}

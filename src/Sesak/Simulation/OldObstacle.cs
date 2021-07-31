using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class OldObstacle
    {
        public Vector2D StartPos = new Vector2D(0, 0);
        public Vector2D EndPos = new Vector2D(0, 0);

        public void FixStartEnd()
        {
            if (StartPos.X > EndPos.X)
            {
                double tmp = StartPos.X;
                StartPos.X = EndPos.X;
                EndPos.X = tmp;
            }
            else if (StartPos.Y > EndPos.Y)
            {
                double tmp = StartPos.Y;
                StartPos.Y = EndPos.Y;
                EndPos.Y = tmp;
            }
        }

        public ObstacleTypes ObstacleType
        {
            get
            {
                if (StartPos.X == EndPos.X && StartPos.Y == EndPos.Y)
                    return ObstacleTypes.Point;
                else if (StartPos.X == EndPos.X)
                    return ObstacleTypes.Vertical;
                else if (StartPos.Y == EndPos.Y)
                    return ObstacleTypes.Horizontal;
                else
                    return ObstacleTypes.Others;


            }
        }

        public OldObstacle Copy()
        {

            //create copy of agent (for simulation instance not overwrite loaded agent)
            OldObstacle copy = new OldObstacle();
            copy.StartPos = StartPos;
            copy.EndPos = EndPos;

            return copy;

        }
        public ObstacleStruct ToStruct()
        {
            ObstacleStruct copy = new ObstacleStruct();
            copy.StartPos = StartPos;
            copy.EndPos = EndPos;
            copy.obType = (sbyte)ObstacleType;

            return copy;
        }
        public override string ToString()
        {
            return "[" + StartPos.X.ToString() + "," + StartPos.Y.ToString() + " - " + EndPos.X.ToString() + "," + EndPos.Y.ToString() + "]";
        }
    }
    public enum ObstacleTypes
    {
        Point,
        Vertical,
        Horizontal,
        Others
    }
}

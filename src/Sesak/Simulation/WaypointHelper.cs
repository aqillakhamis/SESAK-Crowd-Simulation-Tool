using Sesak.Commons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public static class WaypointHelper
    {
        public static int GetNextTarget(PointF[] waypoint,Line[] obstacles, PointF currentPosition,int currentWaypointIndex)
        {
            if (waypoint == null)
                return -1;
                 
            for(int i = waypoint.Length-1;i>= 0;i--)
            {
                if (!MathHelper.IsLineLineIntersect(currentPosition, waypoint[i], obstacles))
                    return i;
            }
            return currentWaypointIndex;

            /*
            int losTarget = -1;
            for(int i = currentWaypointIndex;i<waypoint.Length;i++)
            {
                if (!MathHelper.IsLineLineIntersect(waypoint[i], waypoint[i], obstacles))
                    losTarget = i;

            }
            */
        }

        public static int GetNextTarget(Vec2[] waypoint, Line[] obstacles, Vec2 currentPosition, int currentWaypointIndex)
        {
            if (waypoint == null)
                return -1;

            //return waypoint.Length - 1;
            for (int i = waypoint.Length - 1; i >= 0; i--)
            {
                if (!MathHelper.IsLineLineIntersect(currentPosition.ToPoint(), waypoint[i].ToPoint(), obstacles))
                    return i;
            }
            return currentWaypointIndex;

            /*
            int losTarget = -1;
            for(int i = currentWaypointIndex;i<waypoint.Length;i++)
            {
                if (!MathHelper.IsLineLineIntersect(waypoint[i], waypoint[i], obstacles))
                    losTarget = i;

            }
            */
        }
        public static int GetNextTarget(Vector2D[] waypoint, Line[] obstacles, Vector2D currentPosition, int currentWaypointIndex)
        {
            if (waypoint == null)
                return -1;
            const double maxWpDistance = 1.5;

            //return waypoint.Length - 1;
            PointF ptC = new PointF((float)currentPosition.X, (float)currentPosition.Y);

            for (int i = waypoint.Length - 1; i >= 0; i--)
            {
                PointF ptWp = new PointF((float)waypoint[i].X, (float)waypoint[i].Y);

                //range test
                double dist = MathHelper.GetDistance(ptWp, ptC);
                
                if (!MathHelper.IsLineLineIntersect(ptC, ptWp, obstacles) && dist < maxWpDistance)
                    return i;
            }
            return currentWaypointIndex;

            /*
            int losTarget = -1;
            for(int i = currentWaypointIndex;i<waypoint.Length;i++)
            {
                if (!MathHelper.IsLineLineIntersect(waypoint[i], waypoint[i], obstacles))
                    losTarget = i;

            }
            */
        }
    }
}

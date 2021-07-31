using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Commons
{
    public static class MathHelper
    {
        public static double GetDistance(PointF p1, PointF p2)
        {
            double dx = p1.X - p2.X;
            double dy = p1.Y - p2.Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static void GetMinMaxPoint(PointF[] array, out PointF min, out PointF max)
        {
            min = new PointF();
            max = new PointF();
            for (int i = 0; i < array.Length; i++)
            {
                if (i == 0)
                {
                    min = array[0];
                    max = array[0];
                    continue;
                }

                if (array[i].X > max.X)
                {
                    max.X = array[i].X;
                }
                else if (array[i].X < min.X)
                {
                    min.X = array[i].X;
                }

                if (array[i].Y > max.Y)
                {
                    max.Y = array[i].Y;
                }
                else if (array[i].Y < min.Y)
                {
                    min.Y = array[i].Y;
                }
            }
        }
        public static bool IsLineLineIntersect(PointF a1, PointF a2, PointF b1, PointF b2)
        {

            // calculate the direction of the lines
            float uA = ((b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X)) / ((b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y));
            float uB = ((a2.X - a1.X) * (a1.Y - b1.Y) - (a2.Y - a1.Y) * (a1.X - b1.X)) / ((b2.Y - b1.Y) * (a2.X - a1.X) - (b2.X - b1.X) * (a2.Y - a1.Y));

            // if uA and uB are between 0-1, lines are colliding
            if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
            {

                // optionally, draw a circle where the lines meet
                float intersectionX = a1.X + (uA * (a2.X - a1.X));
                float intersectionY = a1.Y + (uA * (a2.Y - a1.Y));


                return true;
            }
            return false;
        }

        public static bool IsLineLineIntersect(PointF a1, PointF a2, Line line)
        {
            return IsLineLineIntersect(a1, a2, line.P1, line.P2);
        }
        public static bool IsLineLineIntersect(PointF a1, PointF a2, Line[] lines)
        {
            foreach (Line line in lines)
            {
                if (IsLineLineIntersect(a1, a2, line.P1, line.P2))
                    return true;
            }
            return false;
        }
        public static bool IsLineRectIntersect(PointF a1,PointF a2,RectangleF rect,bool checkInside = true)
        {
            if (checkInside && (rect.Contains(a1) || rect.Contains(a2)))
                return true;
            

            PointF[] rpt1 = new PointF[4];
            PointF[] rpt2 = new PointF[4];

            rpt1[0] = new PointF(rect.Left, rect.Top);
            rpt2[0] = new PointF(rect.Right, rect.Top);

            rpt1[1] = new PointF(rect.Right, rect.Top); 
            rpt2[1] = new PointF(rect.Right, rect.Bottom);

            rpt1[2] = new PointF(rect.Right, rect.Bottom);
            rpt2[2] = new PointF(rect.Left, rect.Bottom);

            rpt1[3] = new PointF(rect.Left, rect.Bottom);
            rpt2[3] = new PointF(rect.Left, rect.Top);

            for(int i = 0;i<4;i++)
            {
                if (IsLineLineIntersect(a1, a2, rpt1[i], rpt2[i]))
                    return true;
            }
            
            return false;
        }
        public static PointF GetCenterPoint(RectangleF rect)
        {
            return new PointF((rect.Left + rect.Right) / 2, (rect.Top + rect.Bottom) / 2);
        }

        public static double DegreeToRadian(double degree)
        {
            return degree / 180 * Math.PI;
        }
        public static double RadianToDegree(double radian)
        {
            return radian / Math.PI * 180;
        }

        public static PointF RadianToVector(double radian)
        {
            return new PointF((float)Math.Cos(radian),(float)Math.Sin(radian));
        }
        public static PointF RadianToVector(double radian,PointF offset,float radius)
        {
            return new PointF(offset.X + (float)Math.Cos(radian) * radius, offset.Y + (float)Math.Sin(radian) * radius);
        }


        public static double VectorToRadian(PointF p1,PointF p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if (dx == 0 && dy == 0)
                return 0;

            return Math.Atan2(dy, dx);
        }
    }
}

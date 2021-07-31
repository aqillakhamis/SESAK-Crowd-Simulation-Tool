using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Commons
{
    public struct Vec2
    {
        public const int ROUNDINGDECIMAL = 12;

        public static implicit operator PointF(Vec2 v) { return new PointF((float)v.X, (float)v.Y); }
        public static implicit operator Vec2(PointF v) { return new Vec2(v.X, v.Y); }

        public Vec2(PointF pts)
        {
            X = pts.X;
            Y = pts.Y;
        }
        public Vec2(double x,double y)
        {
            X = x;
            Y = y;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public PointF ToPoint()
        {
            return new PointF((float)X, (float)Y);
        }


        public void Rounding()
        {
            X = Math.Round(X, ROUNDINGDECIMAL);
            Y = Math.Round(Y, ROUNDINGDECIMAL);
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        public double GetDistance(Vec2 p)
        {
            double dx = p.X - X;
            double dy = p.Y - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        public override string ToString()
        {
            return X.ToString() + "," + Y.ToString();
        }
    }
}

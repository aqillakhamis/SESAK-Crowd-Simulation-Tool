using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public struct Vector2D
    {
        public const int ROUNDINGDECIMAL = 12;
        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X;
        public double Y;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetMagnitude()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2D Normalize()
        {
            Vector2D normalized = new Vector2D();

            double mag = GetMagnitude();

            normalized.X = X / mag;
            normalized.Y = Y / mag;

            return normalized;
        }
        public override string ToString()
        {
            return "{" + X.ToString() + "," + Y.ToString() + "}";
        }

        public void Rounding()
        {
            X = Math.Round(X, ROUNDINGDECIMAL);
            Y = Math.Round(Y, ROUNDINGDECIMAL);
        }
        public double GetDistance(Vector2D targetVector)
        {
            double dx = X - targetVector.X;
            double dy = Y - targetVector.Y;
            return Math.Round(Math.Sqrt(dx * dx + dy * dy), ROUNDINGDECIMAL);
        }

    }

}

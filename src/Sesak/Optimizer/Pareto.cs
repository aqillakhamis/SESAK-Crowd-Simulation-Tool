using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Optimizer
{
    public class Pareto
    {
        public List<Tuple<double, double>> DataPlot = new List<Tuple<double, double>>();

        public void Initialize(int population)
        {
            for (int i = 0; i < population; i++)
            {
                DataPlot.Add(new Tuple<double, double>(double.NaN, double.NaN));
            }
        }
        public void SetValue(Tuple<double, double> val, int popIndex)
        {
            DataPlot[popIndex] = val;
        }
        public static bool Dominate(Tuple<double, double> a, Tuple<double, double> b)
        {



            if (double.IsNaN(b.Item1) || double.IsNaN(b.Item2))
                return true;

            if (double.IsNaN(a.Item1) ||
                double.IsNaN(a.Item2) ||
                b.Item1 < a.Item1 ||
                b.Item2 < a.Item2 ||
                (a.Item1 == b.Item1 && a.Item2 == b.Item2))
                return false;

            return true;

        }
        /*
        public static bool DominateAny(Tuple<double, double> a, Tuple<double, double> b)
        {

            if (double.IsNaN(b.Item1) || double.IsNaN(b.Item2))
                return true;

            if (double.IsNaN(a.Item1) ||
                double.IsNaN(a.Item2) ||
                b.Item1 > a.Item1 ||
                b.Item2 > a.Item2 ||
                (a.Item1 == b.Item1 && a.Item2 == b.Item2))
                return false;

            return true;
        }
    */
    }
}

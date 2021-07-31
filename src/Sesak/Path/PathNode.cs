using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Path
{
    public class PathNode
    {
        public int Index;

        public PathNode(PointF pt, bool important = false)
        {
            Location = pt;
            Important = important;
        }
        public PointF Location;
        public List<PathNode> Neighbours = new List<PathNode>();
        public bool Important;
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Path
{
    public class WaypointNode
    {
        public WaypointNode() { }
        public WaypointNode(PointF location,bool important = false)
        {
            Location = location;
            Important = important;
        }
        public PointF Location;
        public bool Important;
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Path
{
    public interface IPathFinder
    {
        WaypointNode[] FindPath(ZoneMap map, PointF start, PointF end);
    }
}

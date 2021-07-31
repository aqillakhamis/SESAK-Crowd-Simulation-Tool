using Sesak.Commons;
using Sesak.Simulation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.SimulationObjects
{
    public interface IObstacle
    {
        bool CheckColision(RectangleF rec);
        Line[] GetCollisionLines();

        OldObstacle[] CreateOldObstacles();
    }
}

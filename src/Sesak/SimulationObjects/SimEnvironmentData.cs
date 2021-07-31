using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.SimulationObjects
{
    public class SimEnvironmentData
    {
        public List<Agent> Agents = new List<Agent>();
        public List<Wall> Walls = new List<Wall>();
        public List<Door> Doors = new List<Door>();

    }
}

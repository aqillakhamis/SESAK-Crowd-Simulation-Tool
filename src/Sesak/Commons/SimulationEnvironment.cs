using Newtonsoft.Json;
using Sesak.SimulationObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Commons
{

    [JsonObject(MemberSerialization.OptIn)]
    public class SimulationEnvironment
    {
        public event EventHandler<DrawableObjectEventArgs> OnObjectPositionChanged;

        public Dictionary<int, IDrawableObject> DrawableObjects = new Dictionary<int, IDrawableObject>();



        [JsonProperty]
        public List<Wall> Walls = new List<Wall>();

        [JsonProperty]
        public List<Door> Doors = new List<Door>();

        [JsonProperty]
        public List<Agent> Agents = new List<Agent>();

        [JsonProperty]
        public List<EvacuationArea> EvacuationAreas = new List<EvacuationArea>();

        [JsonProperty]
        public RectangleF ComfortTestZone = new RectangleF();

        Line[] obstacleLines;
        public Line[] GetObstacleLines()
        {
            return obstacleLines;
        }

        public void RefreshObstacleLines()
        {
            List<Line> obs = new List<Line>();
            foreach(Door door in Doors)
            {
                obs.AddRange(door.GetCollisionLines());
            }
            foreach (Wall wall in Walls)
            {
                obs.AddRange(wall.GetCollisionLines());
            }
            obstacleLines = obs.ToArray();
        }

        public RectangleF GetMapBound(float marginTop,float marginLeft, float marginBottom,float marginRight)
        {
            List<PointF> pts = new List<PointF>();
            
            foreach(KeyValuePair<int,IDrawableObject> obj in DrawableObjects)
            {

                PointF[] objPts = obj.Value.GetPoints();
                if (objPts.Length > 0)
                    pts.AddRange(objPts);
            }

            MathHelper.GetMinMaxPoint(pts.ToArray(), out PointF min, out PointF max);

            RectangleF mapBound = new RectangleF(min.X - marginLeft, min.Y - marginBottom, max.X - min.X + marginLeft + marginRight, max.Y - min.Y + marginTop + marginBottom); //in canvas coord

            return mapBound;
        }

        public int GetUniqueObjectID()
        {
            int lastID = 1;
            while(DrawableObjects.ContainsKey(lastID))
            {
                lastID++;
            }
            return lastID;
        }
        
        private void RegisterObjectEvent(IDrawableObject obj)
        {
            if (obj == null)
                return;
            obj.OnPositionChanged += DrawableObject_OnPositionChanged;
        }

        private void UnregisterObjectEvent(IDrawableObject obj)
        {
            if (obj == null)
                return;
            obj.OnPositionChanged -= DrawableObject_OnPositionChanged;
        }

        private void DrawableObject_OnPositionChanged(object sender, EventArgs e)
        {
            IDrawableObject obj = (IDrawableObject)sender;

            if (obj is IObstacle)
            {
                RefreshObstacleLines();
            }

            if (obj != null && OnObjectPositionChanged != null)
                OnObjectPositionChanged(this, new DrawableObjectEventArgs(obj));

            
        }


        public EvacuationArea CreateEvacuationArea()
        {
            EvacuationArea evac = new EvacuationArea();
            evac.ObjectID = GetUniqueObjectID();
            DrawableObjects.Add(evac.ObjectID, evac);
            EvacuationAreas.Add(evac);
            RegisterObjectEvent(evac);
            return evac;
        }
        public Wall CreateWall()
        {
            Wall wall = new Wall();
            wall.ObjectID = GetUniqueObjectID();
            DrawableObjects.Add(wall.ObjectID, wall);
            Walls.Add(wall);
            RegisterObjectEvent(wall);
            return wall;
        }
        public Door CreateDoor()
        {
            Door door = new Door();
            door.ObjectID = GetUniqueObjectID();
            DrawableObjects.Add(door.ObjectID, door);
            Doors.Add(door);
            RegisterObjectEvent(door);
            return door;
        }
        public Agent CreateAgent()
        {
            Agent agent = new Agent();
            agent.ObjectID = GetUniqueObjectID();
            DrawableObjects.Add(agent.ObjectID, agent);
            Agents.Add(agent);

            RegisterObjectEvent(agent);
            return agent;
        }
        public string Export()
        {
            string s = JsonConvert.SerializeObject(this);
            Debug.Print(s);
            return s;
        }

        public void Import(string data)
        { 
            SimulationEnvironment s = JsonConvert.DeserializeObject<SimulationEnvironment>(data);
            ComfortTestZone = s.ComfortTestZone;

            //unpack
            foreach (KeyValuePair<int,IDrawableObject> entry in DrawableObjects)
            {
                UnregisterObjectEvent(entry.Value);
            }

            DrawableObjects.Clear();
            Walls.Clear();
            Doors.Clear();
            Agents.Clear();
            EvacuationAreas.Clear();

            foreach(Wall wall in s.Walls)
            {
                DrawableObjects.Add(wall.ObjectID, wall);
                Walls.Add(wall);
            }

            foreach (Door door in s.Doors)
            {
                DrawableObjects.Add(door.ObjectID, door);
                Doors.Add(door);
            }

            foreach (Agent agent in s.Agents)
            {
                DrawableObjects.Add(agent.ObjectID, agent);
                Agents.Add(agent);
            }

            foreach (EvacuationArea area in s.EvacuationAreas)
            {
                DrawableObjects.Add(area.ObjectID, area);
                EvacuationAreas.Add(area);
            }

            foreach (KeyValuePair<int, IDrawableObject> entry in DrawableObjects)
            {
                entry.Value.Recalculate();
                RegisterObjectEvent(entry.Value);
            }

            RefreshObstacleLines();
        }

        public IDrawableObject RemoveObject(int objId)
        {
            if(!DrawableObjects.ContainsKey(objId))
            {
                return null;
            }
            IDrawableObject obj = DrawableObjects[objId];
            if(obj is Wall)
            {
                for (int i = 0; i < Walls.Count; i++)
                {
                    if (Walls[i].ObjectID == objId)
                    {
                        Walls.RemoveAt(i);
                        break;
                    }
                }
            }
            else if(obj is Door)
            {
                for (int i = 0; i < Doors.Count; i++)
                {
                    if (Doors[i].ObjectID == objId)
                    {
                        Doors.RemoveAt(i);
                        break;
                    }
                }
            }
            else if(obj is Agent)
            {
                for (int i = 0; i < Agents.Count; i++)
                {
                    if (Agents[i].ObjectID == objId)
                    {
                        Agents.RemoveAt(i);
                        break;
                    }
                }
            }
            else if (obj is EvacuationArea)
            {
                for (int i = 0; i < EvacuationAreas.Count; i++)
                {
                    if (EvacuationAreas[i].ObjectID == objId)
                    {
                        EvacuationAreas.RemoveAt(i);
                        break;
                    }
                }
            }

            DrawableObjects.Remove(objId);

            UnregisterObjectEvent(obj);

            if (obj is IObstacle)
            {
                RefreshObstacleLines();
            }

            return obj;
        }

        public SimulationEnvironment Copy()
        {
            string s = Export();
            SimulationEnvironment copy = new SimulationEnvironment();
            copy.Import(s);
            return copy;
        }

        public void SetOptimizerParameters(double[] y)
        {
            foreach (Door door in Doors)
            {
                if (!door.Optimizer)
                    continue;

                if (door.OptimizerIndex >= 0 && door.OptimizerIndex < y.Length)
                    door.DoorPosition = (float)y[door.OptimizerIndex];
            }
        }
    }

    public class DrawableObjectEventArgs: EventArgs
    {
        public DrawableObjectEventArgs()
        {
            DrawableObject = null;
        }
        public DrawableObjectEventArgs(IDrawableObject obj)
        {
            DrawableObject = obj;
        }
        public IDrawableObject DrawableObject;
    }



}

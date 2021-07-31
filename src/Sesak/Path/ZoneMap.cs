using Sesak.Commons;
using Sesak.SimulationObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Path
{
    public class ZoneMap
    {
        public const float DefaultCellSize = 0.5f;
        public GridCell[,] Cells = new GridCell[0,0];
        public Dictionary<int,GridCell> CellCollection = new Dictionary<int, GridCell>();

        public List<GridCell> obstacleCell = new List<GridCell>();

        public List<PathNode> pathNodes = new List<PathNode>();

        public Line[] ObstacleLines = new Line[0];
        public bool GenerateAgentWaypoint(Agent agent)
        {
            AStarPathFinder pathFinder = new AStarPathFinder();
            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //PointF[] ptInput = CubicSpline.AutoSpline(pathFinder.FindPath(zoneMap, agent.StartPosition, agent.Destination));//zoneMap.SimplifyPath(pathFinder.FindPath(zoneMap, agent.StartPosition, agent.Destination));
            //PointF[] pt1;
            //PointF[] pt2;
            
            WaypointNode[] nodes = pathFinder.FindPath(this, agent.StartPosition, agent.Destination);
            if (nodes == null)
            {
                agent.Waypoints = null;
                return false;
            }
            List<PointF> pts = new List<PointF>();
            for(int i = 0;i<nodes.Length;i++)
            {
                pts.Add(nodes[i].Location);
            }
            PointF[] waypoint = pts.ToArray();// pathFinder.FindPath(this, agent.StartPosition, agent.Destination);//InterpolatePath(SimplifyPath(pathFinder.FindPath(this, agent.StartPosition, agent.Destination)),1);

            

            //PointF[] waypoint = InterpolatePath(SimplifyPath(pathFinder.FindPath(this, agent.StartPosition, agent.Destination)), 1f);
            agent.Waypoints = waypoint;// CubicSpline.AutoSpline();
            //agent.Waypoints = pathFinder.FindPath(zoneMap, agent.StartPosition, agent.Destination);
            //sw.Stop();
            //this.Text = sw.ElapsedMilliseconds.ToString();

            return true;
        }

        public RectangleF MapBound { get; private set; }



        public void GenerateZoneMap2(SimulationEnvironment env,float cellSize = DefaultCellSize)
        {
            Debug.Print("GenerateZoneMap");
            float margin = cellSize * 2;
            MapBound =  env.GetMapBound(margin, margin, margin, margin);
            
            int cols = (int)Math.Ceiling(MapBound.Width / cellSize);
            int rows = (int)Math.Ceiling(MapBound.Height / cellSize);

            Cells = new GridCell[rows,cols];
            CellCollection = new Dictionary<int, GridCell>();
            obstacleCell.Clear();

            SizeF cellRectSize = new SizeF(cellSize, cellSize);

            int idx = 0;
            for(int y = 0;y<rows;y++)
            {
                for(int x = 0;x<cols;x++)
                {
                    PointF cellPosition = MapBound.Location;
                    cellPosition.X += cellSize * x;
                    cellPosition.Y += cellSize * y;
                    GridCell cell = new GridCell(idx, new RectangleF(cellPosition, cellRectSize));
                    CellCollection.Add(idx, cell);
                    Cells[y, x] = cell;
                    idx++;
                }
            }

            //quick store Neighbour
            /*
            float maxNeighbourDistance = cellSize * 1.5f;
            foreach (GridCell cell in Cells)
            {
                foreach(GridCell neighbour in Cells)
                {
                    if (neighbour == cell)
                        continue;

                    float dist = (float)MathHelper.GetDistance(cell.TargetPoint, neighbour.TargetPoint);
                    if (dist > 0 && dist <= maxNeighbourDistance)
                        cell.NeighbourCell.Add(neighbour);
                }
            }
            */

            Point[] neighbourOffsetIdx = new Point[]
            {
                new Point(-1,-1),
                new Point(-1,0),
                new Point(-1,1),

                new Point(0,-1),
                new Point(0,1),

                new Point(1,-1),
                new Point(1,0),
                new Point(1,-1)
            };

            
            foreach (GridCell cell in Cells)
            {

                

                //cell.HasObstacle = false;
                cell.HasObstacle = false;

                foreach(KeyValuePair<int,IDrawableObject> obj in env.DrawableObjects)
                {
                    if(obj.Value is IObstacle)
                    {
                        IObstacle obs = (IObstacle)obj.Value;
                        cell.HasObstacle = obs.CheckColision(cell.CellRectangle);
                        
                        /*
                        if(obs is Door)
                        {
                            //reposition cell target if at doorway to avoid agent stuck
                            Door door = (Door)obs;
                            PointF doorCenter = door.GetDoorCenter();
                            if (cell.CellRectangle.Contains(doorCenter))
                                cell.TargetPoint = doorCenter;
                            
                        }
                        */
                        if (cell.HasObstacle)
                        {
                            obstacleCell.Add(cell);
                            break;
                        }
                    }
                    continue;
                }
               

            }
            for(int y = 0;y<rows;y++)
            {
                for(int x = 0;x<cols;x++)
                {
                    GridCell cell = Cells[y, x];
                    foreach (Point offset in neighbourOffsetIdx)
                    {
                        int px = x + offset.X;
                        int py = y + offset.Y;
                        if (px < 0 || px >= cols || py < 0 || py >= rows || Cells[py, px].HasObstacle) //discard obstacle neighbour
                            continue;

                        //store Neighbour
                        cell.NeighbourCells.Add(Cells[py, px]);
                    }
                }
            }
            /*
            //cache obstacle lines
            List<Line> obsLine = new List<Line>();

            foreach (GridCell cell in obstacleCell)
            {
                Line[] l = cell.GetBoundLine();
                for (int i = 0; i < l.Length; i++)
                {
                    if (!obsLine.Contains(l[i]))
                        obsLine.Add(l[i]);
                }
            }

            obstacleLines = obsLine.ToArray();
            */
        }


        public PointF[] GenerateZoneMap(SimulationEnvironment env, float cellSize = DefaultCellSize)
        {
            float WallKeepOutDistance = cellSize;
            float MaxNeighbourRange = cellSize * 1.75f;
            Debug.Print("Generate Zone Map");
            Stopwatch sw = new Stopwatch();

            sw.Start();


            float margin = cellSize * 2;
            MapBound = env.GetMapBound(margin, margin, margin, margin);


            int cols = (int)Math.Ceiling(MapBound.Width / cellSize);
            int rows = (int)Math.Ceiling(MapBound.Height / cellSize);
            pathNodes.Clear();
            //pathNodes = new List<PathNode>();

            int idx = 0;
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    PointF cellPosition = MapBound.Location;
                    cellPosition.X += cellSize * x + cellSize / 2;
                    cellPosition.Y += cellSize * y + cellSize / 2;
                    pathNodes.Add(new PathNode(cellPosition));
                    idx++;
                }
            }
            sw.Stop();
            Debug.Print("[" + sw.Elapsed.TotalMilliseconds.ToString() + "] Base node generated");
            sw.Start();

            List<Line> obsLines = new List<Line>();


            //Clear Wall Obstacle
            foreach (Wall wall in env.Walls)
            {
                obsLines.AddRange(wall.GetCollisionLines());
            }
            foreach (Door door in env.Doors)
            {
                obsLines.AddRange(door.GetCollisionLines());
                
                PointF p1 = door.P1;
                PointF p2 = door.P2;

                float x1, x2, y1, y2;
                if (p1.X > p2.X)
                {
                    x1 = p2.X;
                    x2 = p1.X;
                }
                else
                {
                    x1 = p1.X;
                    x2 = p2.X;
                }

                if (p1.Y > p2.Y)
                {
                    y1 = p2.Y;
                    y2 = p1.Y;
                }
                else
                {
                    y1 = p1.Y;
                    y2 = p2.Y;
                }

                for (int i = pathNodes.Count - 1; i >= 0; i--)
                {

                    //TODO: Diagonal wall not included
                    if (x1 == x2 && pathNodes[i].Location.Y > y1 && pathNodes[i].Location.Y < y2 && Math.Abs(pathNodes[i].Location.X - x1) <= WallKeepOutDistance)
                    {
                        pathNodes.RemoveAt(i);
                        continue;
                    }
                    else if (y1 == y2 && pathNodes[i].Location.X > x1 && pathNodes[i].Location.X < x2 && Math.Abs(pathNodes[i].Location.Y - y1) <= WallKeepOutDistance)
                    {
                        pathNodes.RemoveAt(i);
                        continue;
                    }
                    else if (MathHelper.GetDistance(pathNodes[i].Location, p1) <= WallKeepOutDistance)
                    {
                        pathNodes.RemoveAt(i);
                        continue;
                    }
                    else if (MathHelper.GetDistance(pathNodes[i].Location, p2) <= WallKeepOutDistance)
                    {
                        pathNodes.RemoveAt(i);
                        continue;
                    }

                }
                
            }



            foreach (Line line in obsLines)
            { 
                PointF p1 = line.P1;
                PointF p2 = line.P2;

                float x1, x2, y1, y2;
                if(p1.X > p2.X)
                {
                    x1 = p2.X;
                    x2 = p1.X;
                }
                else
                {
                    x1 = p1.X;
                    x2 = p2.X;
                }

                if (p1.Y > p2.Y)
                {
                    y1 = p2.Y;
                    y2 = p1.Y;
                }
                else
                {
                    y1 = p1.Y;
                    y2 = p2.Y;
                }

                for (int i = pathNodes.Count-1; i >= 0; i--)
                {

                    //TODO: Diagonal wall not included
                    if (x1 == x2 && pathNodes[i].Location.Y > y1 && pathNodes[i].Location.Y < y2 && Math.Abs(pathNodes[i].Location.X - x1) <= WallKeepOutDistance)
                    {
                        pathNodes.RemoveAt(i);
                        continue;
                    }
                    else if (y1 == y2 && pathNodes[i].Location.X > x1 && pathNodes[i].Location.X < x2 && Math.Abs(pathNodes[i].Location.Y - y1) <= WallKeepOutDistance)
                    {
                        pathNodes.RemoveAt(i);
                        continue;
                    }
                    else if (MathHelper.GetDistance(pathNodes[i].Location, p1) <= WallKeepOutDistance)
                    {
                        pathNodes.RemoveAt(i);
                        continue;
                    }
                    else if (MathHelper.GetDistance(pathNodes[i].Location, p2) <= WallKeepOutDistance)
                    {
                        pathNodes.RemoveAt(i);
                        continue;
                    }
                    
                }

            }
            List<Line> obsExtra = new List<Line>();
            for (int i = 0; i < obsLines.Count; i++)
            {
                if (obsLines[i].P1.X == obsLines[i].P2.X)
                {
                    Line a1 = obsLines[i];
                    Line a2 = obsLines[i];

                    a1.P1.X += cellSize / 2;
                    a1.P2.X += cellSize / 2;
                    a2.P1.X -= cellSize / 2;
                    a2.P2.X -= cellSize / 2;
                    obsExtra.Add(a1);
                    obsExtra.Add(a2);
                }
                else if (obsLines[i].P1.Y == obsLines[i].P2.Y)
                {
                    Line a1 = obsLines[i];
                    Line a2 = obsLines[i];

                    a1.P1.Y += cellSize / 2;
                    a1.P2.Y += cellSize / 2;
                    a2.P1.Y -= cellSize / 2;
                    a2.P2.Y -= cellSize / 2;
                    obsExtra.Add(a1);
                    obsExtra.Add(a2);
                }
            }

            ObstacleLines = obsExtra.ToArray();
            sw.Stop();
            Debug.Print("[" + sw.Elapsed.TotalMilliseconds.ToString() + "] Wall Keep Out Removed");
            sw.Start();


            foreach (Door door in env.Doors)
            {
                PointF ptDoor = door.GetDoorCenter();
                //pathNodes.Add(new PathNode(ptDoor));
                //TODO: Diagonal door
                
                if (door.P1.X == door.P2.X) //horizontal Door
                {
                    pathNodes.Add(new PathNode(new PointF(ptDoor.X + cellSize / 2, ptDoor.Y), true));
                    pathNodes.Add(new PathNode(new PointF(ptDoor.X - cellSize / 2, ptDoor.Y), true));   
                }
                else
                {
                    pathNodes.Add(new PathNode(new PointF(ptDoor.X, ptDoor.Y + cellSize / 2), true));
                    pathNodes.Add(new PathNode(new PointF(ptDoor.X, ptDoor.Y - cellSize / 2), true));
                }
                
                //pathNodes.Add(new PathNode(ptDoor, true));
            }

            sw.Stop();
            Debug.Print("[" + sw.Elapsed.TotalMilliseconds.ToString() + "] Add Door Centerpoint");
            sw.Start();
            //Stitch Neighbour





            for (int i = 0; i < pathNodes.Count - 1; i++)
            {
                for (int j = i; j < pathNodes.Count; j++)
                {
                    PathNode a = pathNodes[i];
                    PathNode b = pathNodes[j];
                    if (a.Location == b.Location)
                        continue;

                    if (MathHelper.GetDistance(a.Location, b.Location) <= MaxNeighbourRange)
                    {
                        a.Neighbours.Add(b);
                        b.Neighbours.Add(a);
                    }
                }
            }


            sw.Stop();
            Debug.Print("[" + sw.Elapsed.TotalMilliseconds.ToString() + "] Neighbour Linked");
            sw.Start();

            PointF[] pt = new PointF[pathNodes.Count];
            for (int i = 0; i < pathNodes.Count; i++)
            {
                pathNodes[i].Index = i;
                pt[i] = pathNodes[i].Location;
            }


            return pt.ToArray();
            //quick store Neighbour
            /*
            float maxNeighbourDistance = cellSize * 1.5f;
            foreach (GridCell cell in Cells)
            {
                foreach(GridCell neighbour in Cells)
                {
                    if (neighbour == cell)
                        continue;

                    float dist = (float)MathHelper.GetDistance(cell.TargetPoint, neighbour.TargetPoint);
                    if (dist > 0 && dist <= maxNeighbourDistance)
                        cell.NeighbourCell.Add(neighbour);
                }
            }
            */

            Point[] neighbourOffsetIdx = new Point[]
            {
                new Point(-1,-1),
                new Point(-1,0),
                new Point(-1,1),

                new Point(0,-1),
                new Point(0,1),

                new Point(1,-1),
                new Point(1,0),
                new Point(1,-1)
            };


            foreach (GridCell cell in Cells)
            {



                //cell.HasObstacle = false;
                cell.HasObstacle = false;

                foreach (KeyValuePair<int, IDrawableObject> obj in env.DrawableObjects)
                {
                    if (obj.Value is IObstacle)
                    {
                        IObstacle obs = (IObstacle)obj.Value;
                        cell.HasObstacle = obs.CheckColision(cell.CellRectangle);

                        /*
                        if(obs is Door)
                        {
                            //reposition cell target if at doorway to avoid agent stuck
                            Door door = (Door)obs;
                            PointF doorCenter = door.GetDoorCenter();
                            if (cell.CellRectangle.Contains(doorCenter))
                                cell.TargetPoint = doorCenter;
                            
                        }
                        */
                        if (cell.HasObstacle)
                        {
                            obstacleCell.Add(cell);
                            break;
                        }
                    }
                    continue;
                }


            }
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    GridCell cell = Cells[y, x];
                    foreach (Point offset in neighbourOffsetIdx)
                    {
                        int px = x + offset.X;
                        int py = y + offset.Y;
                        if (px < 0 || px >= cols || py < 0 || py >= rows || Cells[py, px].HasObstacle) //discard obstacle neighbour
                            continue;

                        //store Neighbour
                        cell.NeighbourCells.Add(Cells[py, px]);
                    }
                }
            }
            /*
            //cache obstacle lines
            List<Line> obsLine = new List<Line>();

            foreach (GridCell cell in obstacleCell)
            {
                Line[] l = cell.GetBoundLine();
                for (int i = 0; i < l.Length; i++)
                {
                    if (!obsLine.Contains(l[i]))
                        obsLine.Add(l[i]);
                }
            }

            obstacleLines = obsLine.ToArray();
            */
        }

        public Point CanvasToCellCoordinate(PointF pt)
        {
            int cols = Cells.GetLength(1);
            int rows = Cells.GetLength(0);
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (Cells[y, x].CellRectangle.Contains(pt))
                        return new Point(x, y);
                }
            }

            return new Point(-1, -1);
        }

        public PointF[] InterpolatePath(PointF[] path,float minDistance)
        {
            if (path == null)
                return null;
            if (path.Length < 2)
                return path;

            List<PointF> interpolated = new List<PointF>();

            interpolated.Add(path[0]);
            for (int i = 1; i < path.Length; i++)
            {
                PointF p1 = path[i - 1];
                PointF p2 = path[i];

                double dist = MathHelper.GetDistance(p1, p2);
                if (dist > minDistance * 2)
                {
                    int c = (int)Math.Floor(dist / minDistance);

                    float dx = p2.X - p1.X;
                    float dy = p2.Y - p1.Y;

                    for (int j = 1; j < c; j++)
                    {
                        float k = (float)j / c;
                        float x = k * dx + p1.X;
                        float y = k * dy + p1.Y;

                        interpolated.Add(new PointF(x, y));
                    }

                }

                interpolated.Add(path[i]);
            }
            return interpolated.ToArray();
        }


        static bool PointExists(PointF a,PointF b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public PointF[] SimplifyPath(WaypointNode[] path)
        {
            if (path == null)
                return null;
            if (path.Length <= 2)
            {
                List<PointF> pts = new List<PointF>();
                for(int i = 0;i<path.Length;i++)
                {
                    pts.Add(path[i].Location);
                }
                
                return pts.ToArray();
            }

            //return new PointF[] { path[0], path[path.Length - 1] };

            List<PointF> pt = new List<PointF>();
            pt.Add(path[0].Location);
            PointF ptTest = path[0].Location;

            int c = 0;
            for(int i = 1;i< path.Length;i++)
            {
                if(path[i].Important)
                {
                    pt.Add(path[i].Location);
                    ptTest = path[i].Location;
                    continue;
                }

                if (!isInLOS(ptTest, path[i].Location))
                {
                    if(c == 0)
                    {
                        pt.Add(path[i].Location);
                        ptTest = path[i].Location;
                        continue;
                    }

                    i--;
                    if (!pt.Exists(e => PointExists(e,path[i].Location)))
                    {
                        pt.Add(path[i].Location);
                        ptTest = path[i].Location;
                    }
                    c = 0;
                    /*
                    ptTest = path[i];
                    
                    if (isInLOS(path[i], path[i - 1]))
                    {
                        i--;
                    }
                    
                    pt.Add(path[i]);
                    */
                    continue;
                }
                c++;
            }

            //pt.Add(path[path.Length - 1].Location);

            List<PointF> pOut = new List<PointF>();
            pOut.Add(pt[0]);
            
            for(int i = 1;i < pt.Count;i++)
            {
                if (!pOut.Contains(pt[i]))
                    pOut.Add(pt[i]);
            }
            /*
            path = pt.ToArray();
            pt.Clear();
            int j = 0;
            ptTest = path[0];
            for (int i = path.Length - 1; i > j; i--)
            {
                
                if (isInLOS(ptTest, path[i]) || (i == j + 1))
                {
                    pt.Add(path[i]);
                    ptTest = path[i];
                    j = i;
                    i = path.Length;
                    continue;
                }
            }
            */
            return pOut.ToArray();
        }
        private bool isInLOS(PointF pt1, PointF pt2)
        {

            if (MathHelper.IsLineLineIntersect(pt1, pt2, ObstacleLines))
                return false;

            return true;

            /*
            foreach(GridCell cell in obstacleCell)
            {
                if (MathHelper.IsLineRectIntersect(pt1, pt2,cell.CellRectangle))
                    return false;
            }
            return true;
            */
            /*
            foreach(Line l in obstacleLines)
            {
                if (MathHelper.IsLineLineIntersect(pt1, pt2, l))
                    return false;
            }
            return true;
            */
        }

    }
}

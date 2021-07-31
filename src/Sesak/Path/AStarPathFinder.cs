using Sesak.Commons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Path
{
    class AStarPathNode
    {
        public int CellIndex;
        public float HCost; //Distance From End Node
        public float GCost; //Distance From Starting Node
        public float FCost;

        public PointF Location;
        public AStarPathNode PrevNode;
        public bool Important;
    }
    public class AStarPathFinder : IPathFinder
    {
        public PointF[] FindPath2(ZoneMap map, PointF start, PointF end)
        {
            Point startIdx = map.CanvasToCellCoordinate(start);
            Point endIdx = map.CanvasToCellCoordinate(end);

            if (startIdx.X < 0 || startIdx.Y < 0 || endIdx.X < 0 || endIdx.Y < 0)
                return null;

            GridCell startCell = map.Cells[startIdx.Y, startIdx.X];
            GridCell endCell = map.Cells[endIdx.Y, endIdx.X];

            

            Dictionary<int,AStarPathNode> nodes = new Dictionary<int,AStarPathNode>();
            Dictionary<int,AStarPathNode> pendingNeighbours = new Dictionary<int,AStarPathNode>();

            AStarPathNode endNode = null;
            AStarPathNode startNode = CreatePathNode(startCell);

            int startNodeIndex = startNode.CellIndex;

            //nodes.Add(startNode.CellIndex, startNode);


            AStarPathNode currentNode = startNode;
            do
            {
                //add all unevaluated neighbour
                GridCell cell = map.CellCollection[currentNode.CellIndex];
                
                for (int i = 0; i < cell.NeighbourCells.Count; i++)
                {
                    GridCell nCell = cell.NeighbourCells[i];
                    if (nodes.ContainsKey(nCell.Index))
                        continue;

                    AStarPathNode nNode;
                    if (pendingNeighbours.ContainsKey(nCell.Index))
                    {
                         nNode = pendingNeighbours [nCell.Index];
                    }
                    else
                    {
                        nNode = CreatePathNode(nCell);
                        if(nNode.CellIndex == endCell.Index)
                        {
                            nNode.PrevNode = currentNode;
                            endNode = nNode;
                            pendingNeighbours.Clear();
                            
                            break;
                        }
                        pendingNeighbours.Add(nNode.CellIndex, nNode);
                    }

                    //compute cost for neighbour
                    ComputeCost(ref nNode, currentNode, end);
                }

                if (pendingNeighbours.ContainsKey(currentNode.CellIndex))
                    pendingNeighbours.Remove(currentNode.CellIndex);


                nodes.Add(currentNode.CellIndex, currentNode);

                if (pendingNeighbours.Count <= 0)
                    break;

                //pick lowest cost neighbour
                currentNode = null;
                foreach(KeyValuePair<int,AStarPathNode> keypair in pendingNeighbours)
                {
                    AStarPathNode neighbour = keypair.Value;
                    if (currentNode == null || (neighbour.FCost < currentNode.FCost) || (neighbour.FCost == currentNode.FCost && neighbour.HCost < currentNode.HCost))
                        currentNode = neighbour;
                }
            }
            while (pendingNeighbours.Count > 0);

            if (endNode == null)
                return null;
            List<PointF> waypoint = new List<PointF>();
            AStarPathNode node = endNode.PrevNode;
            waypoint.Add(end);

            while (node != null)
            {
                waypoint.Insert(0, node.Location);
                node = node.PrevNode;

                if (node == null || node.CellIndex == startNodeIndex)
                    break;
            }
            waypoint.Insert(0,start);

            return waypoint.ToArray();
            
        }

        public WaypointNode[] FindPath(ZoneMap map, PointF start, PointF end)
        {
            Stopwatch sw = new Stopwatch();


            Debug.Print("A* Find Path");
            sw.Start();

            PathNode startPathNode = null;
            PathNode endPathNode = null;

            {
                double currentDistStart = 0, currentDistEnd = 0;
                foreach (PathNode pn in map.pathNodes)
                {
                    if (startPathNode == null)
                    {
                        currentDistStart = MathHelper.GetDistance(pn.Location, start);
                        currentDistEnd = MathHelper.GetDistance(pn.Location, end);
                        startPathNode = pn;
                        endPathNode = pn;
                        continue;
                    }
                    double distEnd = MathHelper.GetDistance(pn.Location, end);
                    double distStart = MathHelper.GetDistance(pn.Location, start);

                    if (currentDistStart > distStart)
                    {
                        currentDistStart = distStart;
                        startPathNode = pn;
                    }

                    if (currentDistEnd > distEnd)
                    {
                        currentDistEnd = distEnd;
                        endPathNode = pn;
                    }
                }
            }
            sw.Stop();
            Debug.Print("["+sw.Elapsed.TotalMilliseconds + "] Find Start End Path Node");
            sw.Start();


            Dictionary<int, AStarPathNode> nodes = new Dictionary<int, AStarPathNode>();
            Dictionary<int, AStarPathNode> pendingNeighbours = new Dictionary<int, AStarPathNode>();

            AStarPathNode endNode = null;
            AStarPathNode startNode = CreatePathNode(startPathNode);

            int startNodeIndex = startNode.CellIndex;

            //nodes.Add(startNode.CellIndex, startNode);


            AStarPathNode currentNode = startNode;
            do
            {
                //add all unevaluated neighbour
                PathNode n = map.pathNodes[currentNode.CellIndex];//map.CellCollection[currentNode.CellIndex];

                for (int i = 0; i < n.Neighbours.Count; i++)
                {
                    PathNode neighbour = n.Neighbours[i];
                    if (nodes.ContainsKey(neighbour.Index))
                        continue;

                    AStarPathNode nNode;
                    if (pendingNeighbours.ContainsKey(neighbour.Index))
                    {
                        nNode = pendingNeighbours[neighbour.Index];
                    }
                    else
                    {
                        nNode = CreatePathNode(neighbour);
                        if (nNode.CellIndex == endPathNode.Index)
                        {
                            nNode.PrevNode = currentNode;
                            endNode = nNode;
                            pendingNeighbours.Clear();

                            break;
                        }
                        pendingNeighbours.Add(nNode.CellIndex, nNode);
                    }

                    //compute cost for neighbour
                    ComputeCost(ref nNode, currentNode, end);
                }

                if (pendingNeighbours.ContainsKey(currentNode.CellIndex))
                    pendingNeighbours.Remove(currentNode.CellIndex);


                nodes.Add(currentNode.CellIndex, currentNode);

                if (pendingNeighbours.Count <= 0)
                    break;

                //pick lowest cost neighbour
                currentNode = null;
                foreach (KeyValuePair<int, AStarPathNode> keypair in pendingNeighbours)
                {
                    AStarPathNode neighbour = keypair.Value;
                    if (currentNode == null || (neighbour.FCost < currentNode.FCost) || (neighbour.FCost == currentNode.FCost && neighbour.HCost < currentNode.HCost))
                        currentNode = neighbour;
                }
            }
            while (pendingNeighbours.Count > 0);

            if (endNode == null)
                return null;
            List<WaypointNode> waypoint = new List<WaypointNode>();

            AStarPathNode node = endNode.PrevNode;
            waypoint.Add(new WaypointNode(end,true));

            while (node != null)
            {
                waypoint.Insert(0,new WaypointNode(node.Location,node.Important));
                node = node.PrevNode;

                if (node == null || node.CellIndex == startNodeIndex)
                    break;
            }
            waypoint.Insert(0, new WaypointNode(start, true));

            return waypoint.ToArray();

        }
        private AStarPathNode  CreatePathNode(GridCell cell)
        {
            AStarPathNode node = new AStarPathNode();
            node.CellIndex = cell.Index;
            node.Location = cell.TargetPoint;
            return node;

        }
        private AStarPathNode CreatePathNode(PathNode pathNode)
        {
            AStarPathNode node = new AStarPathNode();
            node.CellIndex = pathNode.Index;
            node.Location = pathNode.Location;
            node.Important = pathNode.Important;
            return node;

        }
        private void ComputeCost(ref AStarPathNode target,AStarPathNode current,PointF destination)
        {
            float hCost = (float)MathHelper.GetDistance(target.Location, destination);
            float gCost = (float)MathHelper.GetDistance(target.Location, current.Location) + current.GCost;
            float fCost = hCost + gCost;

            if(target.PrevNode == null || target.FCost > fCost || (target.FCost == fCost && target.HCost == hCost))
            {
                target.FCost = fCost;
                target.GCost = gCost;
                target.HCost = hCost;
                target.PrevNode = current;
            }
            
            
        }
    }
}

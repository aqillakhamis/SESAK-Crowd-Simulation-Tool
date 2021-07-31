using Sesak.Commons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Path
{
    public class GridCell
    {
        public RectangleF CellRectangle;
        public PointF TargetPoint;
        public bool HasObstacle;
        public int Index;

        public List<GridCell> NeighbourCells;
        public GridCell(int idx,RectangleF cellRect)
        {
            Index = idx;
            CellRectangle = cellRect;
            HasObstacle = false;
            TargetPoint = MathHelper.GetCenterPoint(cellRect);
            NeighbourCells = new List<GridCell>();
        }
        public Line[] GetBoundLine()
        {
            Line[] r = new Line[4];
            r[0] = new Line() { 
                P1 = new PointF(CellRectangle.Left, CellRectangle.Top), 
                P2 = new PointF(CellRectangle.Right, CellRectangle.Top) };

            r[1] = new Line()
            {
                P1 = new PointF(CellRectangle.Right, CellRectangle.Top),
                P2 = new PointF(CellRectangle.Right, CellRectangle.Bottom)
            };

            r[2] = new Line()
            {
                P1 = new PointF(CellRectangle.Right, CellRectangle.Bottom),
                P2 = new PointF(CellRectangle.Left, CellRectangle.Bottom)
            };

            r[3] = new Line()
            {
                P1 = new PointF(CellRectangle.Left, CellRectangle.Bottom),
                P2 = new PointF(CellRectangle.Left, CellRectangle.Top)
            };

            return r;
        }
        public void SetObstacleFlag(bool val)
        {
            HasObstacle = val;
        }
    }
}

using Newtonsoft.Json;
using Sesak.Commons;
using Sesak.Path;
using Sesak.Simulation;
using Sesak.SimulationObjects.PropertiesControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak.SimulationObjects
{

    [JsonObject(MemberSerialization.OptIn)]
    public class Door : IDrawableObject,IObstacle
    {
        [JsonProperty]
        public int ObjectID { get; set; }

        [JsonProperty]
        public string Name { get; set; }


        [JsonProperty]
        public int OptimizerIndex { get; set; }

        [JsonProperty]
        private Vec2[] Point
        {
            get
            {
                return new Vec2[] { new Vec2(pts[0]), new Vec2(pts[3]) };
            }
            set
            {
                pts[0] = value[0].ToPoint();
                pts[3] = value[1].ToPoint();
            }
        }

        private float doorPosition = 0f;
        [JsonProperty]
        public float DoorPosition
        {
            get { return doorPosition; }
            set
            {
                if (value == doorPosition)
                    return;

                float old = doorPosition;

                if (value < -1)
                    doorPosition = -1;
                else if (value > 1)
                    doorPosition = 1;
                else
                    doorPosition = value;

                RecalculateDoorPosition();
                if (old != doorPosition && OnPositionChanged != null)
                    OnPositionChanged(this, new EventArgs());
            }
        }

        private float doorWidth = 2;
        [JsonProperty]
        public float DoorWidth
        {
            get { return doorWidth; }
            set
            {
                if (doorWidth == value)
                    return;

                doorWidth = value;
                RecalculateDoorPosition();

                if (OnPositionChanged != null)
                    OnPositionChanged(this, new EventArgs());
            }
        }

        [JsonProperty]
        public bool Optimizer { get; set; }

        //Color Settings
        const string WallColor = "#2196f3";
        const string WallColorSelected = "#ff5722";
        const float WallVisualThickness = 5;


        static bool inited = false;
        //static Pen WallPen;
        //static Pen WallPenSelected;
        static Brush DoorBrush;
        static Brush DoorSelectedBrush;
        static void init()
        {
            DoorBrush = new SolidBrush(ColorTranslator.FromHtml(WallColor));
            DoorSelectedBrush = new SolidBrush(ColorTranslator.FromHtml(WallColorSelected));

        }

        public Door()
        {
            if (!inited)
            {
                init();
            }

            Name = "Door";
        }

        public override string ToString()
        {
            return "[" + ObjectID.ToString() + "] " + Name;
            /*
            return "Door " + ObjectID.ToString() + " ["
                + P1.X.ToString() + "," + P1.Y.ToString() + "],["
                + P2.X.ToString() + "," + P2.Y.ToString() + "],["
                + DoorPosition.ToString() + "," + DoorWidth.ToString() + "]";
            */
        }


        public Control CreatePropertiesControl()
        {

            ucDoorProp prop = new ucDoorProp();
            prop.SetObject(this);
            return prop;
        }

        private PointF[] pts = new PointF[4] { new PointF(), new PointF(), new PointF(), new PointF() };

        public event EventHandler OnPositionChanged;

        public PointF P1 { get { return pts[0]; } }
        public PointF P2 { get { return pts[3]; } }

        private Line[] collisionLine = null;
        public float Length {
            get
            {
                return (float)MathHelper.GetDistance(P1, P2);
            }
        }
        public float DoorPositionMeter
        {
            get
            {
                float maxOffset = Length - doorWidth;
                float v = ((doorPosition + 1) /2) * (maxOffset);
                if (v < 0)
                    v = 0;
                else if (v > maxOffset)
                    v = maxOffset;
                return v;
            }
            set
            {
                float old = doorPosition;
                float dm = value;
                float maxOffset = Length - doorWidth;
                if (dm < 0)
                    dm = 0;
                else if (dm > maxOffset)
                    dm = maxOffset;

                float v = dm / maxOffset;
                v = (v * 2) - 1;
                doorPosition = v;

                RecalculateDoorPosition();

                if (old != doorPosition && OnPositionChanged != null)
                    OnPositionChanged(this, new EventArgs());
            }
        }
        public PointF CenterPoint { get { return new PointF((P1.X + P2.X) / 2, (P1.Y + P2.Y) / 2); } }
        

        public float DoorWidthPercentage
        {
            get 
            { 
                return doorWidth / Length; 
            }
            set 
            {
                float old = doorWidth;

                doorWidth = value * Length;
                if (doorWidth < 0)
                    doorWidth = 0;
                else if (doorWidth > Length)
                    doorWidth = Length;

                RecalculateDoorPosition();
                if (old != doorWidth && OnPositionChanged != null)
                    OnPositionChanged(this, new EventArgs());
            }
        }

        public bool Selected { get; set; }

        private void RecalculateDoorPosition()
        {
            PointF[] pt = GetDoorPoints(P1, P2, DoorWidth, DoorPosition);
            pts[1] = pt[1];
            pts[2] = pt[2];

            UpdateCollisionLine();
        }

        private void UpdateCollisionLine()
        {
            List<Line> c = new List<Line>();
            if (pts[0] != pts[1])
                c.Add(new Line() { P1 = pts[0], P2 = pts[1]});

            if (pts[2] != pts[3])
                c.Add(new Line() { P1 = pts[2], P2 = pts[3] });

            collisionLine = c.ToArray();
        }
        public bool SetPoints(PointF pt1, PointF pt2,int anchor = 0)
        {
            PointF p1Old = P1;
            PointF p2Old = P2;

            float dx = Math.Abs(pt1.X - pt2.X);
            float dy = Math.Abs(pt1.Y - pt2.Y);
            if (dx >= dy)
            {
                //horizontal wall
                if (anchor == 0)
                {
                    pts[0].X = pt1.X;
                    pts[0].Y = pt1.Y;

                    pts[3].X = pt2.X;
                    pts[3].Y = pt1.Y;
                }
                else
                {
                    pts[0].X = pt1.X;
                    pts[0].Y = pt2.Y;

                    pts[3].X = pt2.X;
                    pts[3].Y = pt2.Y;

                }
            }
            else
            {
                //vertical wall
                if (anchor == 0)
                {
                    pts[0].X = pt1.X;
                    pts[0].Y = pt1.Y;

                    pts[3].X = pt1.X;
                    pts[3].Y = pt2.Y;
                }
                else
                {
                    pts[0].X = pt2.X;
                    pts[0].Y = pt1.Y;

                    pts[1].X = pt2.X;
                    pts[1].Y = pt2.Y;
                }
            }

            RecalculateDoorPosition();
            bool modified = (p1Old != P1 || p2Old != P2);

            if (modified && OnPositionChanged != null)
                OnPositionChanged(this, new EventArgs());

            return modified;
        }
        

        public void Draw(Graphics g, CanvasHelper canvasHelper)
        {
            //PointF[] ptDraw = GetDoorPoints(pts[0], pts[1], DoorWidth, DoorPosition);
            PointF[] ptc = canvasHelper.CanvasToScreen(pts);

            Brush brush;
            if (Selected)
                brush = DoorSelectedBrush;
            else
                brush = DoorBrush;

            Pen wallPen = new Pen(brush, WallVisualThickness);
            Pen doorPen = new Pen(Brushes.DarkGray, WallVisualThickness / 2);

            //doorPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            //doorPen.DashPattern = new float[] { 2, 6 };
            g.DrawLine(wallPen, ptc[0], ptc[1]);
            g.DrawLine(wallPen, ptc[2], ptc[3]);

            g.DrawLine(doorPen, ptc[1], ptc[2]);
        }


        private static PointF[] GetDoorPoints(PointF ptWallStart,PointF ptWallEnd,float doorWidth,float doorOffset)
        {
            float dx = (ptWallEnd.X - ptWallStart.X);
            float dy = (ptWallEnd.Y - ptWallStart.Y);
            float dist = (float)Math.Sqrt(dx * dx + dy * dy);

            float wall = dist - doorWidth;
            float d = (doorOffset + 1) / 2;
            float dps = (wall * d) / dist;
            float dpe = (dist - (wall * (1-d))) / dist;

            float dx1 = (ptWallEnd.X - ptWallStart.X) * dps + ptWallStart.X;
            float dx2 = (ptWallEnd.X - ptWallStart.X) * dpe + ptWallStart.X;

            float dy1 = (ptWallEnd.Y - ptWallStart.Y) * dps + ptWallStart.Y;
            float dy2 = (ptWallEnd.Y - ptWallStart.Y) * dpe + ptWallStart.Y;

            PointF[] res = new PointF[4];

            res[0] = ptWallStart;
            res[1] = new PointF(dx1, dy1);
            res[2] = new PointF(dx2, dy2);
            res[3] = ptWallEnd;

            return res;
        }



        public PointOfInterest[] GetPointOfInterests(CanvasHelper canvasHelper, RectangleF canvasBound)
        {
            List<PointOfInterest> pois = new List<PointOfInterest>();

            if (canvasBound.Contains(pts[0]))
            {
                PointOfInterest poi = new PointOfInterest()
                {
                    ObjectID = ObjectID,
                    POIIndex = 0,
                    Pointer = Cursors.Hand,
                    Position = canvasHelper.CanvasToScreen(pts[0])
                };
                pois.Add(poi);
            }
            if (canvasBound.Contains(pts[3]))
            {
                PointOfInterest poi = new PointOfInterest()
                {
                    ObjectID = ObjectID,
                    POIIndex = 1,
                    Pointer = Cursors.Hand,
                    Position = canvasHelper.CanvasToScreen(pts[3])
                };
                pois.Add(poi);
            }

            PointF centerPoint = CenterPoint;
            if (canvasBound.Contains(centerPoint))
            {
                PointOfInterest poi = new PointOfInterest()
                {
                    ObjectID = ObjectID,
                    POIIndex = 2,
                    Pointer = Cursors.SizeAll,
                    Position = canvasHelper.CanvasToScreen(centerPoint)
                };
                pois.Add(poi);
            }

            return pois.ToArray();
        }

        public bool Manipulate(ref PointOfInterest poi, CanvasHelper canvasHelper, PointF ptStart, PointF ptEnd)
        {
            PointF poiCanvasLoc = new PointF();

            bool modified = false;

            if (poi.POIIndex == 0)
            {
                //move start point
                modified = SetPoints(ptEnd, pts[3], 1);
                poiCanvasLoc = pts[0];

            }
            else if (poi.POIIndex == 1)
            {
                //move end point
                modified = SetPoints(pts[0], ptEnd, 0);
                poiCanvasLoc = pts[3];

            }
            else if (poi.POIIndex == 2)
            {
                PointF center = CenterPoint;
                PointF p1Old = pts[0];
                PointF p2Old = pts[1];

                pts[0].X = pts[0].X - center.X + ptEnd.X;
                pts[0].Y = pts[0].Y - center.Y + ptEnd.Y;

                pts[3].X = pts[3].X - center.X + ptEnd.X;
                pts[3].Y = pts[3].Y - center.Y + ptEnd.Y;

                poiCanvasLoc = CenterPoint;
                
                RecalculateDoorPosition();
                modified = (p1Old != pts[0] || p2Old != pts[1]);

            }

            poi.Position = canvasHelper.CanvasToScreen(poiCanvasLoc);

            if (modified && OnPositionChanged != null)
                OnPositionChanged(this, new EventArgs());

            return modified;
        }

        public RectangleF GetBound()
        {
            PointF min, max;
            MathHelper.GetMinMaxPoint(pts, out min, out max);

            return new RectangleF(min.X, min.Y, max.X - min.X, max.Y - min.Y);
        }

        public PointF[] GetPoints()
        {
            return pts;
        }

        public bool CheckColision(RectangleF rec)
        {
            if (MathHelper.IsLineRectIntersect(pts[0], pts[1], rec))
                return true;

            if (MathHelper.IsLineRectIntersect(pts[2], pts[3], rec))
                return true;

            return false;
        }
        public PointF GetDoorCenter()
        {
            return new PointF((pts[1].X + pts[2].X) / 2, (pts[1].Y + pts[2].Y) / 2);
        }

        public Line[] GetCollisionLines()
        {
            return collisionLine;
            //throw new NotImplementedException();
        }

        public OldObstacle[] CreateOldObstacles()
        {
            Line[] ls = GetCollisionLines();
            List<OldObstacle> obs = new List<OldObstacle>();
            foreach(Line l in ls)
            {
                OldObstacle o = new OldObstacle() { StartPos = new Vector2D(l.P1.X, l.P1.Y), EndPos = new Vector2D(l.P2.X, l.P2.Y) };

                obs.Add(o);
            }
            return obs.ToArray();
        }

        public void Recalculate()
        {
            RecalculateDoorPosition();
            //throw new NotImplementedException();
        }
    }
}

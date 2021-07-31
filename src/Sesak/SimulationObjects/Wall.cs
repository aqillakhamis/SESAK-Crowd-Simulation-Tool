using Newtonsoft.Json;
using Sesak.Commons;
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
    public class Wall : IDrawableObject, IObstacle
    {
        [JsonProperty]
        public int ObjectID { get; set; }

        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        private Vec2[] Point
        {
            get
            {
                return new Vec2[] { new Vec2(pts[0]), new Vec2(pts[1]) };
            }
            set
            {
                pts[0] = value[0].ToPoint();
                pts[1] = value[1].ToPoint();
            }
        }


        //Color Settings
        const string WallColor = "#2196f3";
        const string WallColorSelected = "#ff5722";
        const float WallVisualThickness = 5;


        static bool inited = false;
        static Pen WallPen;
        static Pen WallPenSelected;

        static void init()
        {

            WallPen = new Pen(new SolidBrush(ColorTranslator.FromHtml(WallColor)), WallVisualThickness);
            WallPenSelected = new Pen(new SolidBrush(ColorTranslator.FromHtml(WallColorSelected)), WallVisualThickness);
        }

        public Wall()
        {
            if(!inited)
            {
                init();
            }

            Name = "Wall";
        }

        public override string ToString()
        {
            return "[" + ObjectID.ToString() + "] " + Name;// + " [" + P1.X.ToString() + "," + P1.Y.ToString() + "],[" + P2.X.ToString() + "," + P2.Y.ToString() + "]";
        }

        public event EventHandler OnPositionChanged;

        public Control CreatePropertiesControl()
        {
            ucWallProp prop = new ucWallProp();
            prop.SetObject(this);
            return prop;
        }

        private PointF[] pts = new PointF[2] { new PointF(), new PointF() };

        Line[] collisionLine = null;

        public PointF P1 { get { return pts[0]; } }


        public PointF P2 { get { return pts[1]; } }


        public PointF CenterPoint { get { return new PointF((P1.X + P2.X) / 2, (P1.Y + P2.Y) / 2); } }


        public bool Selected { get; set; }
        

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

                    pts[1].X = pt2.X;
                    pts[1].Y = pt1.Y;
                }
                else
                {
                    pts[0].X = pt1.X;
                    pts[0].Y = pt2.Y;

                    pts[1].X = pt2.X;
                    pts[1].Y = pt2.Y;

                }
            }
            else
            {
                //vertical wall
                if (anchor == 0)
                {
                    pts[0].X = pt1.X;
                    pts[0].Y = pt1.Y;

                    pts[1].X = pt1.X;
                    pts[1].Y = pt2.Y;
                }
                else
                {
                    pts[0].X = pt2.X;
                    pts[0].Y = pt1.Y;

                    pts[1].X = pt2.X;
                    pts[1].Y = pt2.Y;
                }
            }
            bool modified = (p1Old != P1 || p2Old != P2);

            UpdateCollisionLine();


            if (modified && OnPositionChanged != null)
                OnPositionChanged(this, new EventArgs());

            return modified;
            
        }
        
        private void UpdateCollisionLine()
        {
            if (P1 != P2)
                collisionLine = new Line[] { new Line() { P1 = P1, P2 = P2 } };
            else
                collisionLine = new Line[0];
        }

        public void Draw(Graphics g, CanvasHelper canvasHelper)
        {
            PointF ptDraw0 = canvasHelper.CanvasToScreen(pts[0]);
            PointF ptDraw1 = canvasHelper.CanvasToScreen(pts[1]);
            WallPen.Width = WallVisualThickness;
            WallPenSelected.Width = WallVisualThickness;

            if (Selected)
                g.DrawLine(WallPenSelected, ptDraw0, ptDraw1);
            else
                g.DrawLine(WallPen, ptDraw0, ptDraw1);
        }




        public PointOfInterest[] GetPointOfInterests(CanvasHelper canvasHelper, RectangleF canvasBound)
        {
            List<PointOfInterest> pois = new List<PointOfInterest>();

            if(canvasBound.Contains(pts[0]))
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
            if (canvasBound.Contains(pts[1]))
            {
                PointOfInterest poi = new PointOfInterest()
                {
                    ObjectID = ObjectID,
                    POIIndex = 1,
                    Pointer = Cursors.Hand,
                    Position = canvasHelper.CanvasToScreen(pts[1])
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
                modified = SetPoints(ptEnd, pts[1], 1);
                poiCanvasLoc = pts[0];
                
            }
            else if (poi.POIIndex == 1)
            {
                //move end point
                modified = SetPoints(pts[0], ptEnd, 0);
                poiCanvasLoc = pts[1];
            }
            else if (poi.POIIndex == 2)
            {
                PointF center = CenterPoint;
                PointF p1Old = pts[0];
                PointF p2Old = pts[1];

                pts[0].X = pts[0].X - center.X + ptEnd.X;
                pts[0].Y = pts[0].Y - center.Y + ptEnd.Y;

                pts[1].X = pts[1].X - center.X + ptEnd.X;
                pts[1].Y = pts[1].Y - center.Y + ptEnd.Y;


                modified = (p1Old != pts[0] || p2Old != pts[1]);

                poiCanvasLoc = CenterPoint;

                UpdateCollisionLine();
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
            throw new NotImplementedException();
        }

        public bool CheckColision(RectangleF rec)
        {
            return MathHelper.IsLineRectIntersect(P1, P2, rec);
        }

        public Line[] GetCollisionLines()
        {
            return collisionLine;
        }

        public OldObstacle[] CreateOldObstacles()
        {
            Line[] ls = GetCollisionLines();
            List<OldObstacle> obs = new List<OldObstacle>();
            foreach (Line l in ls)
            {
                OldObstacle o = new OldObstacle() { StartPos = new Vector2D(l.P1.X, l.P1.Y), EndPos = new Vector2D(l.P2.X, l.P2.Y) };

                obs.Add(o);
            }
            return obs.ToArray();
        }

        public void Recalculate()
        {
            UpdateCollisionLine();
            //throw new NotImplementedException();
        }
    }
}

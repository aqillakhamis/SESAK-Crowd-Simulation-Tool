using Newtonsoft.Json;
using Sesak.Commons;
using Sesak.SimulationObjects.PropertiesControl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak.SimulationObjects
{
    [JsonObject(MemberSerialization.OptIn)]
    public class EvacuationArea : IDrawableObject
    {
        static bool inited = false;
        static Brush unselectedBrush;
        static Brush selectedBrush;
        static Pen selectedPen;
        static Cursor[] cts = null;

        [JsonProperty]
        public int ObjectID { get; set; }

        [JsonProperty]
        public string Name { get; set; }


        private RectangleF area;

        [JsonProperty]
        public RectangleF Area { get { return area; } set { area = value; recomputePoint(); } }
        public bool Selected { get; set; }

        public event EventHandler OnPositionChanged;

        PointF[] pts = new PointF[4];
        PointF center = new PointF();
        static void init()
        {
            unselectedBrush = new SolidBrush(Color.FromArgb(180, 200, 255, 200));
            selectedBrush = new HatchBrush(HatchStyle.LightUpwardDiagonal, Color.FromArgb(180,150, 205, 150), Color.FromArgb(200, 255, 200));
            selectedPen = new Pen(Color.FromArgb(180, 150, 205, 150), 2);
            cts = new Cursor[]
                { Cursors.SizeNESW,
                  Cursors.SizeNWSE,
                  Cursors.SizeNWSE,
                  Cursors.SizeNESW
                };
        }
        public bool SetTopLeft(float px,float py)//,bool limit = true)
        {
            //if(limit)
            //{
                if (px > Area.Right)
                    px = Area.Right;
                if (py < Area.Top)
                    py = Area.Top;
            //}
            float x = px;
            float w = Area.Width - (px - Area.Left);
            if (w < 0)
                w = 0;

            float h = py - Area.Top;
            float y = Area.Top;
            if (h < 0)
            {
                y += h;
                h = 0;
            }

            RectangleF newArea = new RectangleF(x, y, w, h);
            bool modified = newArea != area;
            area = newArea;
            return modified;
            // recomputePoint();

        }

        public bool SetTopRight(float px, float py)//, bool limit = true)
        {
            //if (limit)
            //{
                if (px < Area.Left)
                    px = Area.Left;
                if (py < Area.Top)
                    py = Area.Top;
            //}

            float w = px - Area.Left;
            float x = Area.Left;
            if (w < 0)
            {
                x += w;
                w = 0;
            }

            float h = py - Area.Top;
            float y = Area.Top;
            if (h < 0)
            {
                y += h;
                h = 0;
            }

            RectangleF newArea = new RectangleF(x, y, w, h);
            bool modified = newArea != area;
            area = newArea;
            return modified;
            // recomputePoint();

        }
        public bool SetBottomLeft(float px, float py)//, bool limit = true)
        {
            //if (limit)
            //{
                if (px > Area.Right)
                    px = Area.Right;
                if (py > Area.Bottom)
                    py = Area.Bottom;
            //}
            float x = px;
            float w = Area.Width - (px - Area.Left);
            if (w < 0)
                w = 0;

            float y = py;
            float h = Area.Height - (py - Area.Top);

            if (h < 0)
            {
                h = 0;
            }

            RectangleF newArea = new RectangleF(x, y, w, h);
            bool modified = newArea != area;
            area = newArea;
            return modified;
            // recomputePoint();

        }

        public bool SetBottomRight(float px, float py)//, bool limit = true)
        {
            //if (limit)
            //{
                if (px < Area.Left)
                    px = Area.Left;
                if (py > Area.Bottom)
                    py = Area.Bottom;
            //}

            float w = px - Area.Left;
            float x = Area.Left;
            if (w < 0)
            {
                x += w;
                w = 0;
            }

            float y = py;
            float h = Area.Height - (py - Area.Top);

            if (h < 0)
            {
                h = 0;
            }

            RectangleF newArea = new RectangleF(x, y, w, h);
            bool modified = newArea != area;
            area = newArea;
            return modified;
            // recomputePoint();

        }

        public bool SetTop(float v)
        {
            //set bottom for rect
            float x = Area.X;
            float w = Area.Width;

            float h = v - Area.Top;
            float y = Area.Top;
            if (h < 0)
            {
                y += h;
                h = 0;
            }

            RectangleF newArea = new RectangleF(x, y, w, h);
            bool modified = newArea != area;
            area = newArea;
            return modified;
            // recomputePoint();

        }
        public bool SetBottom(float v)
        {
            //set top for rect
            float x = Area.X;
            float w = Area.Width;

            float y = v;
            float h = Area.Height - (v - Area.Top);
            
            if (h < 0)
            {
                h = 0;
            }

            RectangleF newArea = new RectangleF(x, y, w, h);
            bool modified = newArea != area;
            area = newArea;
            return modified;
            // recomputePoint();

        }
        public bool SetRight(float v)
        {
            float y = Area.Top;
            float h = Area.Height;

            float w = v - Area.Left;
            float x = Area.Left;
            if(w < 0)
            {
                x += w;
                w = 0;
            }

            RectangleF newArea = new RectangleF(x, y, w, h);
            bool modified = newArea != area;
            area = newArea;
            return modified;
            // recomputePoint();

        }

        public bool SetLeft(float v)
        {
            float x = v;
            float w = Area.Width - (v - Area.Left);
            if (w < 0)
                w = 0;


            float h = Area.Height;
            float y = Area.Top;

            RectangleF newArea = new RectangleF(x, y, w, h);
            bool modified = newArea != area;
            area = newArea;
            return modified;
            // recomputePoint();

        }
        private void recomputePoint()
        {
            pts[0].X = area.Left;
            pts[0].Y = area.Top;

            pts[1].X = area.Right;
            pts[1].Y = area.Top;

            pts[2].X = area.Left;
            pts[2].Y = area.Bottom;

            pts[3].X = area.Right;
            pts[3].Y = area.Bottom;

            center.X = (area.Right + area.Left) / 2;
            center.Y = (area.Bottom + area.Top) / 2;
        }
        public EvacuationArea()
        {
            if (!inited)
                init();
            Name = "Evacuation Area";
            
        }

        public Control CreatePropertiesControl()
        {
            ucEvacuationAreaProp prop = new ucEvacuationAreaProp();
            prop.SetObject(this);
            return prop;
        }

        public void Draw(Graphics g, CanvasHelper canvasHelper)
        {
            RectangleF area = canvasHelper.CanvasToScreen(Area);


            if (Selected)
            {
                g.FillRectangle(selectedBrush, area);
                //g.DrawRectangle(selectedPen, area.X, area.Y, area.Width, area.Height);
                g.DrawLine(selectedPen, area.Left, area.Top, area.Right, area.Top);
                g.DrawLine(selectedPen, area.Right, area.Top, area.Right, area.Bottom);
                g.DrawLine(selectedPen, area.Right, area.Bottom, area.Left, area.Bottom);
                g.DrawLine(selectedPen, area.Left, area.Bottom, area.Left, area.Top);

            }
            else
                g.FillRectangle(unselectedBrush, area);

        }

        public RectangleF GetBound()
        {
            return Area;
        }

        public PointOfInterest[] GetPointOfInterests(CanvasHelper canvasHelper, RectangleF canvasBound)
        {
            List<PointOfInterest> pois = new List<PointOfInterest>();

            for (int i = 0; i < pts.Length; i++)
            {
                if (canvasBound.Contains(pts[i]))
                {
                    pois.Add(new PointOfInterest() 
                    {
                        ObjectID = ObjectID,
                        POIIndex = i,
                        Pointer = cts[i],
                        Position = canvasHelper.CanvasToScreen(pts[i])
                    });
                }
            }

            if (canvasBound.Contains(center))
            {
                pois.Add(new PointOfInterest() 
                {
                    ObjectID = ObjectID,
                    POIIndex = 4,
                    Pointer = Cursors.SizeAll,
                    Position = canvasHelper.CanvasToScreen(center)
                });
            }

            return pois.ToArray();
        }

        public PointF[] GetPoints()
        {
            return new PointF[0];
            
        }

        public bool Manipulate(ref PointOfInterest poi, CanvasHelper canvasHelper, PointF ptStart, PointF ptEnd)
        {
            //PointF poiCanvasLoc = new PointF();
            bool modified = false;
            if(poi.POIIndex == 0)//Bottom Left
            {
                modified = SetBottomLeft(ptEnd.X, ptEnd.Y);
            }
            else if(poi.POIIndex == 1) //Bottom Right
            {
                modified = SetBottomRight(ptEnd.X, ptEnd.Y);
            }
            else if(poi.POIIndex == 2) //Top Left
            {
                modified = SetTopLeft(ptEnd.X, ptEnd.Y);
            }
            else if(poi.POIIndex == 3) //Top Right
            {
                modified = SetTopRight(ptEnd.X, ptEnd.Y);
            }
            else if(poi.POIIndex == 4) //center
            {
                //move 
                area.X = ptEnd.X - (area.Width/2);
                area.Y = ptEnd.Y - (area.Height/2);
                modified = true;
            }
            if(modified)
                recomputePoint();

            if (modified && OnPositionChanged != null)
                OnPositionChanged(this, new EventArgs());


            return modified;
        }

        public void Recalculate()
        {
            recomputePoint();
        }
        public override string ToString()
        {
            return "[" + ObjectID.ToString() + "] " + Name;// + " [" + P1.X.ToString() + "," + P1.Y.ToString() + "],[" + P2.X.ToString() + "," + P2.Y.ToString() + "]";
        }
    }
}

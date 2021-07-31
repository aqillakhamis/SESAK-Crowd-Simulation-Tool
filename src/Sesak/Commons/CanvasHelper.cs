using Sesak.Path;
using Sesak.Simulation;
using Sesak.SimulationObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak.Commons
{
    public struct CanvasInfo { 
    }

    /// <summary>
    /// Helper class for 2D environment plotting
    /// </summary>
    public class CanvasHelper
    {
        public event EventHandler OnSnapChanged;
        public event EventHandler OnOriginChanged;
        public event EventHandler OnGridChanged;
        public event EventHandler OnSnapSizeChanged;

        public PointOfInterest ActivePOI;

        public SimulationEnvironment SimEnvironment;

        //public PointF ptNextWaypointTest = new PointF();
        //int currentWp = 0;


        /// <summary>
        /// Pan value changed event
        /// </summary>
        public event Action<PointF> OnPanChanged;

        /// <summary>
        /// Scale value changed event
        /// </summary>
        public event Action<float> OnScaleChanged;

        public event Action ShouldRedraw;


        float snapGridSize = 0.5f;
        public float SnapGridSize
        {
            get { return snapGridSize; }
            set
            {
                if (snapGridSize == value || value <= 0)
                    return;


                snapGridSize = value;
                if (OnSnapSizeChanged != null)
                    OnSnapSizeChanged(this, EventArgs.Empty);

                if (ShouldRedraw != null)
                    ShouldRedraw();
            }
        }

        #region Drawing Options

        Font axisFont = new Font(FontFamily.GenericMonospace, 10);
        SolidBrush axisTextBrush = new SolidBrush(Color.DarkGray);

        float[] AxisStep = new float[] { 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000,2000,5000 };
        const int MinAxisPixelGap = 50;

        const int MinSnapPixelGap = 10;

        /// <summary>
        /// Minimum plot scale
        /// </summary>
        const float MinScale = 1;//0.125f;//0.0078125f;

        /// <summary>
        /// Maximum plot scale
        /// </summary>
        const float MaxScale = 500;//1024;

        const float DefaultScale = 30;

        float[] scaleStep = new float[] { 1, 2, 5, 10, 20, 50, 100, 200, 500 };
        //int currentScaleStep = 3;



        /// <summary>
        /// Origin (0,0) pen
        /// </summary>
        public Pen OriginLinePen;
        public Pen GridLinePen;

        public Brush SnapPointBrush;
        private void InitializeDrawConstant()
        {
            OriginLinePen = new Pen(Color.Blue, 3);
            GridLinePen = new Pen(Color.LightGray, 1);
            GridLinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            SnapPointBrush = new SolidBrush(Color.FromArgb(32, 32, 32));



        }



        private bool drawAxisText = true;
        /// <summary>
        /// Enable / disable drawing origin line
        /// </summary>
        public bool DrawAxisText
        {
            get { return drawAxisText; }
            set
            {
                
                drawAxisText = value;
                //TargetControl.Invalidate();
                ShouldRedraw();
            }
        }


        private bool drawOriginLine = true;
        /// <summary>
        /// Enable / disable drawing origin line
        /// </summary>
        public bool DrawOriginLine
        {
            get { return drawOriginLine; }
            set
            {
                if (drawOriginLine == value)
                    return;


                drawOriginLine = value;
                //TargetControl.Invalidate();
                ShouldRedraw();

                if(OnOriginChanged != null)
                    OnOriginChanged(this, EventArgs.Empty);
            }
        }

        private bool drawAxisLine = true;

        /// <summary>
        /// Enable / disable drawing axis line
        /// </summary>
        public bool DrawAxisLine
        {
            get
            {
                return drawAxisLine;
            }
            set
            {
                if (DrawAxisLine == value)
                    return;


                drawAxisLine = value;
                //TargetControl.Invalidate();
                ShouldRedraw();

                if (OnGridChanged != null)
                    OnGridChanged(this, EventArgs.Empty);
            }
        }

        private bool drawSnapPoint = true;

        public bool DrawSnapPoint
        {
            get
            {
                return drawSnapPoint;
            }
            set
            {
                if (drawSnapPoint == value)
                    return;

                drawSnapPoint = value;
                ShouldRedraw();

                if (OnSnapChanged != null)
                    OnSnapChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Constructor
        public Control TargetControl = null;

        public CanvasHelper(Control target)
        {
            TargetControl = target;
            InitializeDrawConstant();
        }
        #endregion

        #region Canvas Control

        #region Pan Control
        private PointF panOffset = new PointF();


        public void ResetPOI()
        {
            ActivePOI.POIIndex = -1;
        }
        public PointF PanOffset
        {
            get { return panOffset; }
            set
            {
                panOffset = value;
                OnPanChanged(panOffset);
                //TargetControl.Invalidate();
                ShouldRedraw();
            }
        }
        #endregion

        #region Zoom Scale Control
        private float scale = DefaultScale;
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = validateScale(value);
                OnScaleChanged(scale);
                //TargetControl.Invalidate();
                ShouldRedraw();
            }
        }
        public void ZoomIn()
        {
            int nearestScale = 0;
            for(int i = 0;i<scaleStep.Length;i++)
            {
                if (scaleStep[i] > scale)
                    break;

                nearestScale = i;
            }

            nearestScale++;
            if (nearestScale >= scaleStep.Length)
                nearestScale = scaleStep.Length - 1;

            Scale = scaleStep[nearestScale];
        }

        public void ZoomOut()
        {
            int nearestScale = scaleStep.Length - 1;
            for (int i = scaleStep.Length-1; i >= 0; i--)
            {
                if (scaleStep[i] < scale)
                    break;

                nearestScale = i;
            }

            nearestScale--;
            if (nearestScale < 0)
                nearestScale = 0;

            Scale = scaleStep[nearestScale];
        }
        public void PanToOrigin()
        {
            PanOffset = new PointF();
        }
        public void ResetScale()
        {
            Scale = DefaultScale;
        }
        float validateScale(float val)
        {
            if (val < MinScale)
                val = MinScale;
            if (val > MaxScale)
                val = MaxScale;

            return val;
        }
        #endregion

        #endregion


        public void Paint(Graphics g, Size targetSize)
        {
            //g.Clear(BackgroundColor);

            if (DrawAxisLine)
                DrawAxis(g, targetSize);

            if (DrawOriginLine)
                DrawOrigin(g, targetSize);

            if(DrawSnapPoint)
                DrawSnap(g, targetSize);
            
            //draw evac area
            foreach (IDrawableObject obj in SimEnvironment.EvacuationAreas)
            {
                obj.Draw(g, this);
            }
            

            //draw walls
            foreach (IDrawableObject obj in SimEnvironment.Walls)
            {
                obj.Draw(g, this);
            }
            
            //draw doors
            foreach (IDrawableObject obj in SimEnvironment.Doors)
            {
                obj.Draw(g, this);
            }
            Agent activeAgent = null;
            //draw agents
            foreach (IDrawableObject obj in SimEnvironment.Agents)
            {
                if (obj.Selected)
                    activeAgent = (Agent)obj;
                obj.Draw(g, this);
            }
            //draw poi
            if (ActivePOI.POIIndex >= 0)
            {
                Rectangle poiPoint = new Rectangle((int)(ActivePOI.Position.X - 5), (int)(ActivePOI.Position.Y - 5), 11, 11);
                g.DrawRectangle(Pens.Red, poiPoint);
            }
            /*
            if(activeAgent != null && activeAgent.Waypoints != null)
            {
                
                currentWp = WaypointHelper.GetNextTarget(activeAgent.Waypoints, SimEnvironment.GetObstacleLines(), ptNextWaypointTest, currentWp);
                if(currentWp >= 0 && currentWp < activeAgent.Waypoints.Length)
                {
                    PointF t1 = CanvasToScreen(activeAgent.Waypoints[currentWp]);
                    PointF t2 = CanvasToScreen(ptNextWaypointTest);
                    g.DrawLine(Pens.Crimson, t1, t2);
                }
                
        }
        */

        }

        public void OverlayZoneMap(Graphics g,Size targetSize,ZoneMap zoneMap)
        {
            if (zoneMap.Cells == null)
                return;

            Brush hasObstacleBrush = new SolidBrush(Color.FromArgb(64, 255, 143, 177));
            Brush noObstacleBrush = new SolidBrush(Color.FromArgb(64, 144, 202, 255));
            
            foreach (GridCell cell in zoneMap.Cells)
            {
                if (cell == null)
                    continue;

                RectangleF r = CanvasToScreen(cell.CellRectangle);
                if (cell.HasObstacle)
                    g.FillRectangle(hasObstacleBrush, r);
                else
                    g.FillRectangle(noObstacleBrush, r);

                //debug target point
                /*
                PointF pt = CanvasToScreen(cell.TargetPoint);
                DrawHelper.DrawCrossMarker(g, pt, Pens.Silver, new SizeF(5, 5));
                */
            }
        }


        #region Draw Components
        private void DrawSnap(Graphics g,Size targetSize)
        {
            

            if (snapGridSize <= 0)
                return;
            SmoothingMode currentSmoothMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.None;

            int snapMultiplier = 1;

            float stepSize = snapGridSize;
            
            while(true)
            {
                if (stepSize * scale >= MinSnapPixelGap)
                    break;

                snapMultiplier++;
                stepSize = snapGridSize * snapMultiplier;
            }


            PointF ptScreen = ScreenToCanvas(new PointF());

            float xGrid = (float)(Math.Ceiling(ptScreen.X / stepSize) * stepSize);
            float yGridOrigin = (float)(Math.Floor(ptScreen.Y / stepSize) * stepSize);



            //Draw X-axis
            SizeF ptSize = new SizeF(1, 1);
            while (true)
            {
                PointF ptX = CanvasToScreen(new PointF(xGrid, ptScreen.Y));

                if (ptX.X < 0)
                {
                    xGrid += stepSize;
                    continue;
                }

                if (ptX.X > targetSize.Width)
                    break;

                float yGrid = yGridOrigin;

                while (true)
                {
                    PointF ptY = CanvasToScreen(new PointF(ptScreen.X, yGrid));

                    if (ptY.Y < 0)
                    {
                        yGrid -= stepSize;
                        continue;
                    }

                    if (ptY.Y > targetSize.Height)
                        break;

                    PointF pt = new PointF(ptX.X, ptY.Y);
                    RectangleF rt = new RectangleF(pt, ptSize);
                    g.FillRectangle(SnapPointBrush, rt);
                    //g.DrawLine(SnapPointPen, pt, pt);


                    yGrid -= stepSize;

                }


                xGrid += stepSize;

            }
            
            g.SmoothingMode = currentSmoothMode;

        }
        private void DrawOrigin(Graphics g,Size targetSize)
        {
            PointF originCenter = CanvasToScreen(new PointF());

            g.DrawLine(OriginLinePen, originCenter.X, 0, originCenter.X, targetSize.Height);
            g.DrawLine(OriginLinePen, 0, originCenter.Y, targetSize.Width, originCenter.Y);

        }

        private void DrawAxis(Graphics g,Size targetSize)
        {
            float stepSize = 1;
            for(int i = 0;i<AxisStep.Length;i++)
            {
                stepSize = AxisStep[i];
                float axisGap = stepSize * scale;
                if (axisGap >= MinAxisPixelGap)
                    break;
            }

            PointF ptScreen = ScreenToCanvas(new PointF());

            float xGrid = (float)(Math.Ceiling(ptScreen.X / stepSize) * stepSize);
            float yGrid = (float)(Math.Floor(ptScreen.Y / stepSize) * stepSize);

            SizeF szText = g.MeasureString("0", axisFont);
            PointF pt;
            PointF p0;
            PointF p1;
            PointF ptText = new PointF();

            //Draw X-axis
            while (true)
            {
                pt = CanvasToScreen(new PointF(xGrid, ptScreen.Y));

                if (pt.X < 0)
                {
                    xGrid += stepSize;
                    continue;
                }

                if (pt.X > targetSize.Width)
                    break;

                p0 = new PointF(pt.X, 0);
                p1 = new PointF(pt.X, targetSize.Height);
                
                g.DrawLine(GridLinePen, p0, p1);

                if(DrawAxisText)
                {
                    ptText.X = pt.X;
                    ptText.Y = targetSize.Height - szText.Height;
                    g.DrawString(xGrid.ToString(), axisFont, axisTextBrush, ptText);
                }
                xGrid += stepSize;
                
            }

            //Draw Y-axis
            while (true)
            {
                pt = CanvasToScreen(new PointF(ptScreen.X, yGrid));

                if (pt.Y < 0)
                {
                    yGrid -= stepSize;
                    continue;
                }

                if (pt.Y > targetSize.Height)
                    break;

                p0 = new PointF(0,pt.Y);
                p1 = new PointF(targetSize.Width,pt.Y);

                g.DrawLine(GridLinePen, p0, p1);

                if (DrawAxisText)
                {
                    ptText.X = 0;
                    ptText.Y = pt.Y; // - szText.Height);
                    g.DrawString(yGrid.ToString(), axisFont, axisTextBrush, ptText);
                }

                yGrid -= stepSize;

            }


        }
        #endregion

        #region Draw Utilities
        public PointF CanvasToScreen(PointF canvasPosition)
        {

            float x = (canvasPosition.X + PanOffset.X) * Scale + (TargetControl.Width / 2);
            float y = (TargetControl.Height / 2) - (canvasPosition.Y + PanOffset.Y) * Scale;

            

            return new PointF(x, y);
        }
        public PointF[] CanvasToScreen(PointF[] canvasPositions)
        {
            if (canvasPositions == null)
                return null;

            PointF[] res = new PointF[canvasPositions.Length];

            for(int i = 0;i<res.Length;i++)
            {
                res[i] = CanvasToScreen(canvasPositions[i]);
            }

            return res;
        }
        public RectangleF CanvasToScreen(RectangleF canvasPosition)
        {

            float x = (canvasPosition.X + PanOffset.X) * Scale + (TargetControl.Width / 2);
            float y = (TargetControl.Height / 2) - (canvasPosition.Bottom + PanOffset.Y) * Scale;

            float w = canvasPosition.Width * Scale;
            float h = canvasPosition.Height * Scale;

            return new RectangleF(x, y, w, h);
        }
        public PointF ScreenToCanvas(PointF screenPosition,bool snapToGrid = false)
        {
            float x = ((screenPosition.X - TargetControl.Width/2) / Scale) - PanOffset.X; 
            float y = (((TargetControl.Height - screenPosition.Y) - TargetControl.Height/2) / Scale) - PanOffset.Y;

            if(snapToGrid)
            {
                x = (float)(Math.Round(x / snapGridSize) * snapGridSize);
                y = (float)(Math.Round(y / snapGridSize) * snapGridSize);
            }

            return new PointF(x, y);
        }
        public RectangleF ScreenToCanvas(RectangleF screenBound, bool snapToGrid = false)
        {
            float x1 = ((screenBound.Left - TargetControl.Width / 2) / Scale) - PanOffset.X;
            float y1 = (((TargetControl.Height - screenBound.Top) - TargetControl.Height / 2) / Scale) - PanOffset.Y;

            float x2 = ((screenBound.Right - TargetControl.Width / 2) / Scale) - PanOffset.X;
            float y2 = (((TargetControl.Height - screenBound.Bottom) - TargetControl.Height / 2) / Scale) - PanOffset.Y;



            if (snapToGrid)
            {
                x1 = (float)(Math.Round(x1 / snapGridSize) * snapGridSize);
                y1 = (float)(Math.Round(y1 / snapGridSize) * snapGridSize);

                x2 = (float)(Math.Round(x2 / snapGridSize) * snapGridSize);
                y2 = (float)(Math.Round(y2 / snapGridSize) * snapGridSize);
            }

            return new RectangleF(x1, y2, x2 - x1, y1 - y2);
        }
        public PointF[] ScreenToCanvas(PointF[] screenPositions, bool snapToGrid = false)
        {
            if (screenPositions == null)
                return null;

            PointF[] res = new PointF[screenPositions.Length];

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = ScreenToCanvas(screenPositions[i], snapToGrid);
            }

            return res;
        }
        #endregion
    }
}

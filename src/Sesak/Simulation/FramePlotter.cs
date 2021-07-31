using Sesak.Commons;
using Sesak.SimulationObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak.Simulation
{
    public class FramePlotter
    {
        public event EventHandler PlotInvalidated;
        public event EventHandler OnScaleChanged;
        public event EventHandler OnPanChanged;
        public event EventHandler OnSnapSizeChanged;


        public SimulationInstance simInstance;

        public RectangleF ComfortTestZone = new RectangleF();

        public HeatMap heatMap = new HeatMap();

        public bool DrawHeatMap = false;

        public PlotTemplate DrawingPlotTemplate = new PlotTemplate();

        /// <summary>
        /// Minimum plot scale
        /// </summary>
        const float MinScale = 1;

        /// <summary>
        /// Maximum plot scale
        /// </summary>
        const float MaxScale = 500;


        const float DefaultScale = 30;


        float[] axisSteps = new float[] { 1, 2, 5, 10, 20, 50, 100, 200, 500, 1000, 2000, 5000 };
        float axisStep = 1;
        void recalculateAxisStep()
        {
            axisStep = 1;
            for (int i = 0; i < axisSteps.Length; i++)
            {
                axisStep = axisSteps[i];
                float axisGap = axisStep * scale;
                if (axisGap >= MinAxisPixelGap)
                    break;
            }
        }


        const int MinAxisPixelGap = 50;
        const int MinSnapPixelGap = 10;

        #region Zoom Scale Control
        private float scale = DefaultScale;
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = validateScale(value);
                if(OnScaleChanged != null)
                    OnScaleChanged(this,EventArgs.Empty);

                if (PlotInvalidated != null)
                    PlotInvalidated(this, EventArgs.Empty);
            }
            
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

        #region Pan Control
        private PointF panOffset = new PointF();

        public PointF PanOffset
        {
            get { return panOffset; }
            set
            {
                panOffset = value;
                if (OnPanChanged != null)
                    OnPanChanged(this, EventArgs.Empty);

                if (PlotInvalidated != null)
                    PlotInvalidated(this, EventArgs.Empty);

            }
        }
        #endregion

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

                if (PlotInvalidated != null)
                    PlotInvalidated(this, EventArgs.Empty);
            }
        }


        private bool drawSnapPoint = false;

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
                if (PlotInvalidated != null)
                    PlotInvalidated(this, EventArgs.Empty);
            }
        }
        #region Draw Utilities
        public RectangleF CanvasToScreen(RectangleF canvasPosition, Size targetSize)
        {

            float x = (canvasPosition.X + PanOffset.X) * Scale + (targetSize.Width / 2);
            float y = (targetSize.Height / 2) - (canvasPosition.Bottom + PanOffset.Y) * Scale;

            float w = canvasPosition.Width * Scale;
            float h = canvasPosition.Height * Scale;

            return new RectangleF(x, y, w, h);
        }

        public PointF CanvasToScreen(PointF canvasPosition,Size targetSize)
        {

            float x = (canvasPosition.X + PanOffset.X) * Scale + (targetSize.Width / 2f);
            float y = (targetSize.Height / 2f) - (canvasPosition.Y + PanOffset.Y) * Scale;



            return new PointF(x, y);
        }
        public PointF CanvasToScreen(float x,float y, Size targetSize)
        {

            float tx = (x + PanOffset.X) * Scale + (targetSize.Width / 2f);
            float ty = (targetSize.Height / 2f) - (y + PanOffset.Y) * Scale;

            return new PointF(tx, ty);
        }
        public PointF[] CanvasToScreen(PointF[] canvasPositions, Size targetSize)
        {
            if (canvasPositions == null)
                return null;

            PointF[] res = new PointF[canvasPositions.Length];

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = CanvasToScreen(canvasPositions[i], targetSize);
            }

            return res;
        }
        public RectangleF CanvasToScreen(RectangleF canvasPosition,Rectangle targetScreen)
        {

            float x = (canvasPosition.X + PanOffset.X) * Scale + (targetScreen.Width / 2);
            float y = (targetScreen.Height / 2) - (canvasPosition.Bottom + PanOffset.Y) * Scale;

            float w = canvasPosition.Width * Scale;
            float h = canvasPosition.Height * Scale;

            return new RectangleF(x, y, w, h);
        }
        public PointF ScreenToCanvas(float x,float y, Size targetSize, bool snapToGrid = false)
        {
            float tx = ((x - targetSize.Width / 2) / Scale) - PanOffset.X;
            float ty = (((targetSize.Height - y) - targetSize.Height / 2) / Scale) - PanOffset.Y;

            if (snapToGrid)
            {
                tx = (float)(Math.Round(x / snapGridSize) * snapGridSize);
                ty = (float)(Math.Round(y / snapGridSize) * snapGridSize);
            }

            return new PointF(tx, ty);
        }
        public PointF ScreenToCanvas(PointF screenPosition, Size targetSize, bool snapToGrid = false)
        {
            float x = ((screenPosition.X - targetSize.Width / 2) / Scale) - PanOffset.X;
            float y = (((targetSize.Height - screenPosition.Y) - targetSize.Height / 2) / Scale) - PanOffset.Y;

            if (snapToGrid)
            {
                x = (float)(Math.Round(x / snapGridSize) * snapGridSize);
                y = (float)(Math.Round(y / snapGridSize) * snapGridSize);
            }

            return new PointF(x, y);
        }
        public RectangleF ScreenToCanvas(RectangleF screenBound, Rectangle targetScreen, bool snapToGrid = false)
        {
            float x1 = ((screenBound.Left - targetScreen.Width / 2) / Scale) - PanOffset.X;
            float y1 = (((targetScreen.Height - screenBound.Top) - targetScreen.Height / 2) / Scale) - PanOffset.Y;

            float x2 = ((screenBound.Right - targetScreen.Width / 2) / Scale) - PanOffset.X;
            float y2 = (((targetScreen.Height - screenBound.Bottom) - targetScreen.Height / 2) / Scale) - PanOffset.Y;



            if (snapToGrid)
            {
                x1 = (float)(Math.Round(x1 / snapGridSize) * snapGridSize);
                y1 = (float)(Math.Round(y1 / snapGridSize) * snapGridSize);

                x2 = (float)(Math.Round(x2 / snapGridSize) * snapGridSize);
                y2 = (float)(Math.Round(y2 / snapGridSize) * snapGridSize);
            }

            return new RectangleF(x1, y2, x2 - x1, y1 - y2);
        }
        public PointF[] ScreenToCanvas(PointF[] screenPositions, Size targetSize, bool snapToGrid = false)
        {
            if (screenPositions == null)
                return null;

            PointF[] res = new PointF[screenPositions.Length];

            for (int i = 0; i < res.Length; i++)
            {
                res[i] = ScreenToCanvas(screenPositions[i], targetSize, snapToGrid);
            }

            return res;
        }
        #endregion

        private bool drawAxisText = true;
        /// <summary>
        /// Enable / disable drawing origin line
        /// </summary>
        public bool DrawAxisText
        {
            get { return drawAxisText; }
            set
            {
                if (DrawAxisText == value)
                    return;
                
                drawAxisText = value;
                //TargetControl.Invalidate();
                if (PlotInvalidated != null)
                    PlotInvalidated(this, EventArgs.Empty);
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


                if (PlotInvalidated != null)
                    PlotInvalidated(this, EventArgs.Empty);
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
                if (PlotInvalidated != null)
                    PlotInvalidated(this, EventArgs.Empty);
            }
        }


        private bool drawObstacleObject = true;
        public bool DrawObstacleObject { 
            get { return drawObstacleObject; }
            set
            {
                if (drawObstacleObject == value)
                    return;

                drawObstacleObject = value;
                if (PlotInvalidated != null)
                    PlotInvalidated(this, EventArgs.Empty);

            }
        }

        private bool drawAgentObject = true;
        public bool DrawAgentObject
        {
            get { return drawAgentObject; }
            set
            {
                if (drawAgentObject == value)
                    return;

                drawAgentObject = value;
                if (PlotInvalidated != null)
                    PlotInvalidated(this, EventArgs.Empty);

            }
        }
        private bool drawEvacuationAreaObject = true;
        public bool DrawEvacuationAreaObject
        {
            get { return drawEvacuationAreaObject; }
            set
            {
                if (drawEvacuationAreaObject == value)
                    return;

                drawEvacuationAreaObject = value;
                if (PlotInvalidated != null)
                    PlotInvalidated(this, EventArgs.Empty);

            }
        }

        public void Draw(Graphics g,FrameData frame, Rectangle targetRectangle,bool slowRender = false)
        {
            PlotTemplate t = DrawingPlotTemplate;
            g.Clear(t.BackgroundColor);

            if (!SimulationInstance.ComfortTestZone.IsEmpty)
            {
                RectangleF comfortZone = CanvasToScreen(SimulationInstance.ComfortTestZone, targetRectangle.Size);
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 200)), comfortZone);
            }

            if (DrawEvacuationAreaObject)
                DrawEvacuationArea(g, frame, targetRectangle.Size);

            if (DrawHeatMap && frame != null)
            {
                PointF[] heatPoints = GetHeatPoints(frame.AgentPositions, targetRectangle.Size);
                
                heatMap.UpdateHeatmap(targetRectangle.Width, targetRectangle.Height, heatPoints, 2 * Scale, 0.01f * Scale);
                if(slowRender)
                {
                    while (heatMap.HeatBusy)
                    {
                        Thread.Sleep(1);
                    }
                }

                if (heatMap.HeatBuffer != null)
                    g.DrawImage(heatMap.HeatBuffer, Point.Empty);
            }



            //axis line
            if (DrawAxisLine)
                DrawAxis(g, targetRectangle.Size);

            if (DrawOriginLine)
                DrawOrigin(g, targetRectangle.Size);

            if (DrawSnapPoint)
                DrawSnap(g, targetRectangle.Size);

            if (frame == null)
                return;


            if (DrawObstacleObject)
                DrawObstacle(g,frame, targetRectangle.Size);

            if (DrawAgentObject)
                DrawAgent(g, frame, targetRectangle.Size);



        }
        private PointF[] GetHeatPoints(Vector2D[] agentPositions, Size canvasSize)
        {
            List<PointF> heatpoints = new List<PointF>();
            for (int i = 0; i < agentPositions.Length; i++)
            {
                PointF pt = new PointF((float)agentPositions[i].X, (float)agentPositions[i].Y);
                pt = CanvasToScreen(pt, canvasSize);
                heatpoints.Add(pt);
            }
            return heatpoints.ToArray();
        }

        private void DrawObstacle(Graphics g,FrameData frame,Size targetSize)
        {
            if (frame == null || frame.Obstacles == null)
                return;

            PlotTemplate t = DrawingPlotTemplate;

            foreach(Line line in frame.Obstacles)
            {
                PointF p1 = CanvasToScreen(line.P1, targetSize);
                PointF p2 = CanvasToScreen(line.P2, targetSize);
                g.DrawLine(t.ObstacleWallPen, p1, p2);
            }
            
        }

        private void DrawEvacuationArea(Graphics g, FrameData frame, Size targetSize)
        {

            PlotTemplate t = DrawingPlotTemplate;

            foreach (EvacuationArea evac in simInstance.simEnvironment.EvacuationAreas)
            {
                RectangleF r = CanvasToScreen(evac.Area, targetSize);
                g.FillRectangle(t.EvacuationAreaFillBrush,r);
            }

        }

        private void DrawAgent(Graphics g,FrameData frame,Size targetSize)
        {
            if (frame == null || frame.AgentPositions == null || frame.AgentHeadings == null || frame.AgentBodyRadius  == null ||
                frame.AgentPositions.Length != frame.AgentHeadings.Length || frame.AgentPositions.Length != frame.AgentBodyRadius.Length)
                return;

            PlotTemplate t = DrawingPlotTemplate;
            
            for (int i = 0; i < frame.AgentPositions.Length; i++)
            {
                float r = frame.AgentBodyRadius[i] * scale;
                PointF pt = CanvasToScreen((float)frame.AgentPositions[i].X, (float)frame.AgentPositions[i].Y, targetSize);

                RectangleF bodyBound = new RectangleF(pt.X - r, pt.Y - r, r * 2, r * 2);
                
                g.DrawEllipse(t.AgentCirclePen, bodyBound);
                DrawHelper.DrawPlusMarker(g, pt, t.AgentMarkerPen, t.AgentMarkerSize);

                //draw heading
                PointF pHead = CanvasToScreen(MathHelper.RadianToVector(frame.AgentHeadings[i], new PointF((float)frame.AgentPositions[i].X, (float)frame.AgentPositions[i].Y), frame.AgentBodyRadius[i]), targetSize);
                g.DrawLine(t.AgentCirclePen,pt, pHead);
            }
        }
        private void DrawAxis(Graphics g, Size targetSize)
        {
            PlotTemplate t = DrawingPlotTemplate;
            PointF ptScreen = ScreenToCanvas(0, 0, targetSize);

            float xGrid = (float)(Math.Ceiling(ptScreen.X / axisStep) * axisStep);
            float yGrid = (float)(Math.Floor(ptScreen.Y / axisStep) * axisStep);

            SizeF szText = g.MeasureString("0", t.AxisTextFont);


            //Draw X-axis
            while (true)
            {
                PointF pt = CanvasToScreen(xGrid, ptScreen.Y, targetSize);

                if (pt.X < 0)
                {
                    xGrid += axisStep;
                    continue;
                }

                if (pt.X > targetSize.Width)
                    break;

                PointF p0 = new PointF(pt.X, 0);
                PointF p1 = new PointF(pt.X, targetSize.Height);

                g.DrawLine(t.GridLinePen, p0, p1);

                if (DrawAxisText)
                {
                    PointF ptText = new PointF(pt.X, targetSize.Height - szText.Height);
                    g.DrawString(xGrid.ToString(), t.AxisTextFont, t.AxisTextBrush, ptText);
                }
                xGrid += axisStep;

            }

            //Draw Y-axis
            while (true)
            {
                PointF pt = CanvasToScreen(ptScreen.X, yGrid, targetSize);

                if (pt.Y < 0)
                {
                    yGrid -= axisStep;
                    continue;
                }

                if (pt.Y > targetSize.Height)
                    break;

                PointF p0 = new PointF(0, pt.Y);
                PointF p1 = new PointF(targetSize.Width, pt.Y);

                g.DrawLine(t.GridLinePen, p0, p1);

                if (DrawAxisText)
                {
                    PointF ptText = new PointF(0, pt.Y);// - szText.Height);
                    g.DrawString(yGrid.ToString(), t.AxisTextFont, t.AxisTextBrush, ptText);
                }

                yGrid -= axisStep;

            }


        }

        private void DrawOrigin(Graphics g, Size targetSize)
        {
            PlotTemplate t = DrawingPlotTemplate;

            PointF originCenter = CanvasToScreen(0,0, targetSize);

            g.DrawLine(t.OriginLinePen, originCenter.X, 0, originCenter.X, targetSize.Height);
            g.DrawLine(t.OriginLinePen, 0, originCenter.Y, targetSize.Width, originCenter.Y);

        }

        private void DrawSnap(Graphics g, Size targetSize)
        {

            PlotTemplate t = DrawingPlotTemplate;
            if (snapGridSize <= 0)
                return;
            SmoothingMode currentSmoothMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.None;

            int snapMultiplier = 1;

            float stepSize = snapGridSize;

            while (true)
            {
                if (stepSize * scale >= MinSnapPixelGap)
                    break;

                snapMultiplier++;
                stepSize = snapGridSize * snapMultiplier;
            }


            PointF ptScreen = ScreenToCanvas(0, 0, targetSize);

            float xGrid = (float)(Math.Ceiling(ptScreen.X / stepSize) * stepSize);
            float yGridOrigin = (float)(Math.Floor(ptScreen.Y / stepSize) * stepSize);



            //Draw X-axis
            SizeF ptSize = new SizeF(1, 1);
            while (true)
            {
                PointF ptX = CanvasToScreen(xGrid, ptScreen.Y, targetSize);

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
                    PointF ptY = CanvasToScreen(ptScreen.X, yGrid, targetSize);

                    if (ptY.Y < 0)
                    {
                        yGrid -= stepSize;
                        continue;
                    }

                    if (ptY.Y > targetSize.Height)
                        break;

                    PointF pt = new PointF(ptX.X, ptY.Y);
                    RectangleF rt = new RectangleF(pt, ptSize);
                    g.FillRectangle(t.SnapPointBrush, rt);
                    


                    yGrid -= stepSize;

                }


                xGrid += stepSize;

            }

            g.SmoothingMode = currentSmoothMode;

        }
    }
}

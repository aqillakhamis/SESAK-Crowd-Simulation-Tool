using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class PlottingHelper
    {

        public static bool EnableHeatMap = false;

        static List<Vector2D> agentpos = new List<Vector2D>();

        public static bool ShowWPTarget = true;
        public static void PlotSimulation(Graphics g, SimulationInstance simInstance, List<OldAgent> agentSnapshot, Size canvasSize, float sizeMultiplier = 20)
        {

            Matrix tMat = g.Transform;
            g.TranslateTransform(100, -100);

            const float heatMapRadius = 2; //set 2 meter radius for each heat map

            g.Clear(Color.White);
            if (simInstance == null || agentSnapshot == null)
                return;

            Pen agentPen = new Pen(Color.Blue, 1);
            Pen agentPenMark = new Pen(Color.Blue, 3);
            Pen targetPen = new Pen(Color.FromArgb(128, 255, 174, 200), 1);

            List<PointF> activeHeatPoint = new List<PointF>();

            List<Vector2D> newagentpos = new List<Vector2D>();
            foreach (OldAgent a in agentSnapshot)
            {
                if (a.ReachDestination)
                    continue;




                PointF ptPos = VectPosToDrawPlot(a.Po, canvasSize, sizeMultiplier);
                activeHeatPoint.Add(ptPos);



                newagentpos.Add(new Vector2D(ptPos.X, ptPos.Y));


                float radius = (float)(a.BodyRad * sizeMultiplier);
                if (ShowWPTarget)
                {
                    PointF ptTarget = VectPosToDrawPlot(a.TargetPos, canvasSize, sizeMultiplier);
                    g.DrawLine(targetPen, ptPos, ptTarget);
                }
                g.DrawEllipse(agentPen, ptPos.X - radius, ptPos.Y - radius, radius * 2, radius * 2);



            }


            Pen obstaclePen = new Pen(Color.Red, 3);

            foreach (OldObstacle o in simInstance.Obstacles)
            {

                PointF pStart = VectPosToDrawPlot(o.StartPos, canvasSize, sizeMultiplier);
                PointF pEnd = VectPosToDrawPlot(o.EndPos, canvasSize, sizeMultiplier);


                g.DrawLine(obstaclePen, pStart, pEnd);
            }

            /*
            if (EnableHeatMap)
            {
                HeatMap.OverlayHeatmap(g, canvasSize.Width, canvasSize.Height, activeHeatPoint.ToArray(), heatMapRadius * sizeMultiplier / 1.2f, 0.2f);
            }
            */


            //g.DrawImage(hmap.HeatOverlay, 0, 0);

            g.Transform = tMat;
        }
        private static PointF VectPosToDrawPlot(Vector2D v, Size canvasSize, float SizeMultiplier)
        {

            PointF pt = new PointF();
            pt.X = (float)(v.X * SizeMultiplier);
            pt.Y = (float)((canvasSize.Height - (v.Y * SizeMultiplier)));

            return pt;
        }
    }
}

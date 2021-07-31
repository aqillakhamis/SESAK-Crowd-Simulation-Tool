using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Commons
{
    public class PlotTemplate
    {
        public Color BackgroundColor;
        public Font AxisTextFont;
        public Brush AxisTextBrush;
        public Brush EvacuationAreaFillBrush;

        public Pen GridLinePen;
        public Pen OriginLinePen;

        public Brush SnapPointBrush;

        public Pen ObstacleWallPen;

        public Pen AgentCirclePen;
        public Pen AgentMarkerPen;
        public Size AgentMarkerSize;
        public PlotTemplate()
        {
            //default plot template
            BackgroundColor = Color.White;
            AxisTextFont = new Font(FontFamily.GenericMonospace, 10);
            GridLinePen = new Pen(Color.LightGray, 1);
            AxisTextBrush = new SolidBrush(Color.DarkGray);

            OriginLinePen = new Pen(Color.Blue, 3);
            SnapPointBrush = new SolidBrush(Color.FromArgb(32, 32, 32));
            ObstacleWallPen = new Pen(Color.Red, 5);

            AgentCirclePen = new Pen(Color.Blue, 2);
            AgentMarkerPen = new Pen(Color.Blue, 1);

            AgentMarkerSize = new Size(10, 10);
            EvacuationAreaFillBrush = new SolidBrush(Color.FromArgb(180, 200, 255, 200));
        }
        
    }
}

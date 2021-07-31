using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak.Commons
{
    public static class DrawHelper
    {
        public static void DrawPlusMarker(Graphics g,PointF position,Pen pen,SizeF size)
        {
            float hw = size.Width / 2f;
            float hh = size.Height / 2f;
            g.DrawLine(pen, position.X - hw, position.Y, position.X + hw, position.Y);
            g.DrawLine(pen, position.X, position.Y - hh, position.X, position.Y + hh);
        }
        public static void DrawCrossMarker(Graphics g, PointF position, Pen pen, SizeF size)
        {
            float hw = size.Width / 2f;
            float hh = size.Height / 2f;
            g.DrawLine(pen, position.X - hw, position.Y - hh, position.X + hw, position.Y + hh);
            g.DrawLine(pen, position.X + hw, position.Y - hh, position.X - hw, position.Y + hh);
        }
    }
}

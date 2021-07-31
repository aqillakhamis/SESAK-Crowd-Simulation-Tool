using Sesak.Commons;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak.SimulationObjects
{
    public interface IDrawableObject
    {
        event EventHandler OnPositionChanged;
        string Name { get; set; }
        int ObjectID { get; set; }
        bool Selected { get; set; }
        
        void Draw(Graphics g, CanvasHelper canvasHelper);

        RectangleF GetBound();
        PointF[] GetPoints();
        PointOfInterest[] GetPointOfInterests(CanvasHelper canvasHelper, RectangleF canvasBound);

        bool Manipulate(ref PointOfInterest poi, CanvasHelper canvasHelper, PointF ptStart, PointF ptEnd);

        Control CreatePropertiesControl();

        void Recalculate();

    }
}

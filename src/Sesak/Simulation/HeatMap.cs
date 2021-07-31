using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sesak.Simulation
{
    public class HeatMap
    {
        //public Vector2D[] HeatPoints;
        //public Bitmap HeatMapData;
        //public Bitmap HeatOverlay;
        //public float HeatRadius = 40;

        public event EventHandler OnHeatMapProcessed;

        static Color[] DefaultRemapColors = null;

        private static Color[] DefaultColorPalette()
        {
            Color[] c = new Color[256];
            //Generate Color Palette

            List<Color> baseColor = new List<Color>();
            //Default color map
            baseColor.Add(Color.FromArgb(200, 255, 0, 0)); //RED
            baseColor.Add(Color.FromArgb(127, 255, 0, 0)); //RED
            baseColor.Add(Color.FromArgb(127, 255, 255, 0)); //YELLOW
            baseColor.Add(Color.FromArgb(127, 0, 255, 0)); //GREEN
            baseColor.Add(Color.FromArgb(127, 0, 255, 255)); //CYAN
            //baseColor.Add(Color.FromArgb(127, 0, 0, 255)); //BLUE
            baseColor.Add(Color.FromArgb(0, 0, 0, 255)); //BLUE, alpha zero


            for (int i = 0; i < c.Length; i++)
            {
                float baseColorPos = i / 255f * (baseColor.Count - 1);
                int baseColorA = (int)Math.Floor(baseColorPos);
                int baseColorB = baseColorA + 1;
                if (baseColorB >= baseColor.Count)
                    baseColorB = baseColor.Count - 1;

                float pos = baseColorPos - baseColorA;
                c[i] = InterpolateColor(baseColor[baseColorA], baseColor[baseColorB], pos);
            }
            return c;
        }
        private static Color InterpolateColor(Color A, Color B, float position)
        {
            byte a = (byte)Math.Round(A.A * (1 - position) + B.A * position);
            byte r = (byte)Math.Round(A.R * (1 - position) + B.R * position);
            byte g = (byte)Math.Round(A.G * (1 - position) + B.G * position);
            byte b = (byte)Math.Round(A.B * (1 - position) + B.B * position);
            return Color.FromArgb(a, r, g, b);
        }

        private bool heatBusy = false;
        public bool HeatBusy { get { return heatBusy; } }

        private bool heatReady = false;
        public bool HeatReady { get { return heatReady; } }

        public Bitmap HeatBuffer = null;
        private Bitmap processingBuffer = null;

        Size canvasSize;
        PointF[] hPoints;
        float hRadius;
        float hIntensity;

        public bool UpdateHeatmap(int canvasWidth, int canvasHeight, PointF[] heatPoints, float heatRadiusPixel, float heatIntensity, Color[] colorMap = null)
        {
            if (HeatBusy)
                return false;

            heatBusy = true;

            Thread th = new Thread(ProcessOverlay);
            th.IsBackground = true;

            canvasSize = new Size(canvasWidth, canvasHeight);
            hPoints = heatPoints;
            hRadius = heatRadiusPixel;
            hIntensity = heatIntensity;

            th.Start();

            if (HeatReady)
            {
                Bitmap tmp = HeatBuffer;
                HeatBuffer = processingBuffer;
                if (tmp != null)
                {
                    tmp.Dispose();
                    tmp = null;
                }
                heatReady = false;

                return true;
            }
            return false;
        }
        private void ProcessOverlay()
        {
            Bitmap bmp = OverlayHeatmap(canvasSize.Width, canvasSize.Height, hPoints, hRadius, hIntensity);
            processingBuffer = bmp;
            heatReady = true;
            heatBusy = false;
        }
        public static unsafe Bitmap OverlayHeatmap(int canvasWidth, int canvasHeight, PointF[] heatPoints, float heatRadiusPixel, float heatIntensity, Color[] colorMap = null)
        {
            float heatDiameterPixel = heatRadiusPixel * 2;
            if (heatIntensity > 1)
                heatIntensity = 1;
            else if (heatIntensity < 0)
                heatIntensity = 0;

            byte heatPointIntensity = (byte)(heatIntensity * 255);

            Color centerColor = Color.FromArgb(heatPointIntensity, 0, 0, 0);
            Color[] edgeColors = new Color[] { Color.FromArgb(0, 255, 255, 255) };

            GraphicsPath path = new GraphicsPath();
            RectangleF heatPatch = new RectangleF(0, 0, heatDiameterPixel, heatDiameterPixel);
            path.AddEllipse(heatPatch);
            PathGradientBrush brush = new PathGradientBrush(path);
            brush.CenterColor = centerColor;
            brush.SurroundColors = edgeColors;


            if (colorMap == null)
            {
                if (DefaultRemapColors == null)
                    DefaultRemapColors = DefaultColorPalette();

                colorMap = DefaultRemapColors;
            }


            using (Bitmap heatData = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format32bppArgb))
            {


                Graphics g = Graphics.FromImage(heatData);
                g.Clear(Color.FromArgb(255, 255, 255, 255));


                for (int i = 0; i < heatPoints.Length; i++)
                {
                    float dx = heatPoints[i].X - heatRadiusPixel;
                    float dy = heatPoints[i].Y - heatRadiusPixel;

                    heatPatch.X = dx;
                    heatPatch.Y = dy;
                    brush.TranslateTransform(dx, dy);

                    g.FillEllipse(brush, heatPatch);
                    brush.ResetTransform();

                }


                Bitmap overlay = new Bitmap(heatData.Width, heatData.Height, PixelFormat.Format8bppIndexed);


                ColorPalette palette = overlay.Palette;
                for (int i = 0; i < 256; i++)
                {
                    palette.Entries[i] = colorMap[i];
                }
                overlay.Palette = palette;

                BitmapData overlaydata = overlay.LockBits(new Rectangle(0, 0, heatData.Width, heatData.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                BitmapData bmpdata = heatData.LockBits(new Rectangle(0, 0, heatData.Width, heatData.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                byte* bmpdataptr = (byte*)bmpdata.Scan0.ToPointer();
                byte* overlaydataptr = (byte*)overlaydata.Scan0.ToPointer();

                for (int row = 0; row < bmpdata.Height; row++)
                {
                    for (int col = 0; col < bmpdata.Width; col++)
                    {
                        int overlayIndex = row * overlaydata.Stride + col;
                        int pixelIndex = row * bmpdata.Stride + col * 4;

                        byte data = bmpdataptr[pixelIndex + 1];
                        overlaydataptr[overlayIndex] = data;

                    }
                }
                heatData.UnlockBits(bmpdata);
                overlay.UnlockBits(overlaydata);

                //targetSurface.DrawImage(overlay, Point.Empty);
                return overlay;

            }
        }

        public static unsafe void OverlayHeatmap(Graphics targetSurface, int canvasWidth, int canvasHeight, PointF[] heatPoints, float heatRadiusPixel, float heatIntensity, Color[] colorMap = null)
        {
            float heatDiameterPixel = heatRadiusPixel * 2;
            if (heatIntensity > 1)
                heatIntensity = 1;
            else if (heatIntensity < 0)
                heatIntensity = 0;

            byte heatPointIntensity = (byte)(heatIntensity * 255);

            Color centerColor = Color.FromArgb(heatPointIntensity, 0, 0, 0);
            Color[] edgeColors = new Color[] { Color.FromArgb(0, 255, 255, 255) };

            GraphicsPath path = new GraphicsPath();
            RectangleF heatPatch = new RectangleF(0, 0, heatDiameterPixel, heatDiameterPixel);
            path.AddEllipse(heatPatch);
            PathGradientBrush brush = new PathGradientBrush(path);
            brush.CenterColor = centerColor;
            brush.SurroundColors = edgeColors;


            if (colorMap == null)
            {
                if (DefaultRemapColors == null)
                    DefaultRemapColors = DefaultColorPalette();

                colorMap = DefaultRemapColors;
            }


            using (Bitmap heatData = new Bitmap(canvasWidth, canvasHeight, PixelFormat.Format32bppArgb))
            {


                Graphics g = Graphics.FromImage(heatData);
                g.Clear(Color.FromArgb(255, 255, 255, 255));


                for (int i = 0; i < heatPoints.Length; i++)
                {
                    float dx = heatPoints[i].X - heatRadiusPixel;
                    float dy = heatPoints[i].Y - heatRadiusPixel;

                    heatPatch.X = dx;
                    heatPatch.Y = dy;
                    brush.TranslateTransform(dx, dy);

                    g.FillEllipse(brush, heatPatch);
                    brush.ResetTransform();

                }


                using (Bitmap overlay = new Bitmap(heatData.Width, heatData.Height, PixelFormat.Format8bppIndexed))
                {

                    ColorPalette palette = overlay.Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        palette.Entries[i] = colorMap[i];
                    }
                    overlay.Palette = palette;

                    BitmapData overlaydata = overlay.LockBits(new Rectangle(0, 0, heatData.Width, heatData.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                    BitmapData bmpdata = heatData.LockBits(new Rectangle(0, 0, heatData.Width, heatData.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                    byte* bmpdataptr = (byte*)bmpdata.Scan0.ToPointer();
                    byte* overlaydataptr = (byte*)overlaydata.Scan0.ToPointer();

                    for (int row = 0; row < bmpdata.Height; row++)
                    {
                        for (int col = 0; col < bmpdata.Width; col++)
                        {
                            int overlayIndex = row * overlaydata.Stride + col;
                            int pixelIndex = row * bmpdata.Stride + col * 4;

                            byte data = bmpdataptr[pixelIndex + 1];
                            overlaydataptr[overlayIndex] = data;

                        }
                    }
                    heatData.UnlockBits(bmpdata);
                    overlay.UnlockBits(overlaydata);

                    targetSurface.DrawImage(overlay, Point.Empty);
                }
            }
        }

    }
}

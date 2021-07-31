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
    public class Agent : IDrawableObject
    {
        const float MinBodyRadius = 0.1f;
        const float MaxBodyRadius = 3f;

        const float MinWalkSpeed = 0.1f;
        const float MaxWalkSpeed = 100;

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

        [JsonProperty]
        public float Heading
        {
            get { return heading; }
            set
            {
                heading = (float)(value % (Math.PI * 2));  
            }
        }


        private float bodyRadius = 0.25f;

        [JsonProperty]
        public float BodyRadius
        {
            get { return bodyRadius; }
            set
            {
                bodyRadius = value;
                if (bodyRadius < MinBodyRadius)
                    bodyRadius = MinBodyRadius;
                else if (bodyRadius > MaxBodyRadius)
                    bodyRadius = MaxBodyRadius;
            }
        }

        private bool defaultAgentParameters = true;

        [JsonProperty]
        public bool DefaultAgentParameters
        {
            get { return defaultAgentParameters; }
            set { defaultAgentParameters = value; }
        }


        private float walkSpeed = 1.5f;

        [JsonProperty]
        public float WalkSpeed
        {
            get { return walkSpeed; }
            set
            {
                walkSpeed = value;
                if (walkSpeed < MinWalkSpeed)
                    walkSpeed = MinWalkSpeed;
                else if (walkSpeed > MaxWalkSpeed)
                    walkSpeed = MaxWalkSpeed;
            }
        }

        public PointF[] Waypoints = null;

        //Color Settings
        const string AgentMarkerColor = "#00897b";
        const string AgentMarkerColorSelected = "#ff5722";

        const string DestinationMarkerColor = "#666666";

        const float AgentMarkerThickness = 2;
        const float AgentMarkerRadius = 5;
        const float AgentArrowDistance = 10;

        static bool inited = false;
        static Pen MarkerPen;
        static Pen MarkerPenSelected;
        static Pen DestinationPen;
        static Pen CurrentToDestinationPathPen;

        static void init()
        {

            MarkerPen = new Pen(new SolidBrush(ColorTranslator.FromHtml(AgentMarkerColor)), AgentMarkerThickness);
            MarkerPenSelected = new Pen(new SolidBrush(ColorTranslator.FromHtml(AgentMarkerColorSelected)), AgentMarkerThickness);

            DestinationPen = new Pen(new SolidBrush(ColorTranslator.FromHtml(DestinationMarkerColor)), AgentMarkerThickness);
            CurrentToDestinationPathPen = new Pen(new SolidBrush(ColorTranslator.FromHtml(DestinationMarkerColor)), 1);
            CurrentToDestinationPathPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            
        }

        public Agent()
        {
            if (!inited)
            {
                init();
            }

            Name = "Agent";
        }

        /*
        public Agent(SimAgent agent)
        {
            if (!inited)
            {
                init();
            }

            Name = "Agent";
            ObjectID = agent.ObjectID;
            pts[0] = agent.StartPosition;
            pts[1] = agent.Destination;
            CurrentPosition = agent.Position;

        }
        */
        public override string ToString()
        {
            return "[" + ObjectID.ToString() + "] " + Name;//"Agent " + ObjectID.ToString() + " [" + StartPosition.X.ToString() + "," + StartPosition.Y.ToString() + "],[" + Destination.X.ToString() + "," + Destination.Y.ToString() + "]";
        }


        public Control CreatePropertiesControl()
        {
            ucAgentProp prop = new ucAgentProp();
            prop.SetObject(this);
            return prop;
        }

        private PointF[] pts = new PointF[2] { new PointF(), new PointF() };
        private float heading;

        public event EventHandler OnPositionChanged;


        //private PointF centerPoint = new PointF();

        public PointF CurrentPosition { get; set; }

        public float CurrentHeading { get; set; }

        public PointF StartPosition
        {
            get { return pts[0]; }
            set
            {
                if (pts[0] == value)
                    return;

                pts[0] = value; 
                AutoHeading();
                ResetPosition();

                if (OnPositionChanged != null)
                    OnPositionChanged(this, new EventArgs());

            }
        }
        public PointF Destination
        {
            get { return pts[1]; }
            set
            {
                if (pts[1] == value)
                    return;

                pts[1] = value;
                AutoHeading();

                if (OnPositionChanged != null)
                    OnPositionChanged(this, new EventArgs());
            }
        }

        public void ResetPosition()
        {
            CurrentPosition = StartPosition;
        }

        public void AutoHeading()
        {
            Heading = (float)MathHelper.VectorToRadian(CurrentPosition, Destination);
        }

        public bool Selected { get; set; }

        public RectangleF GetBodyBound(PointF position)
        {

            RectangleF res = new RectangleF();
            res.X = position.X - BodyRadius;
            res.Y = position.Y - BodyRadius;
            res.Width = BodyRadius * 2;
            res.Height = res.Width;

            return res;
            
        }

        public void Draw(Graphics g, CanvasHelper canvasHelper)
        {
            PointF ptCurrent = canvasHelper.CanvasToScreen(CurrentPosition);

            //PointF[] ptDraw = canvasHelper.CanvasToScreen(pts);

            Pen ap;
            if (Selected)
                ap = MarkerPenSelected;
            else
                ap = MarkerPen;

            RectangleF rBody = canvasHelper.CanvasToScreen(GetBodyBound(CurrentPosition));
            g.DrawEllipse(ap, rBody);
            DrawHelper.DrawPlusMarker(g, ptCurrent, ap, new SizeF(10, 10));

            //draw heading
            PointF pHead = canvasHelper.CanvasToScreen(MathHelper.RadianToVector(Heading, CurrentPosition, BodyRadius));
            g.DrawLine(ap, ptCurrent, pHead);



            if (Selected)
            {
                PointF ptDest = canvasHelper.CanvasToScreen(pts[1]);
                //show destination
                DrawHelper.DrawCrossMarker(g,ptDest, DestinationPen, new SizeF(10, 10));
                g.DrawLine(CurrentToDestinationPathPen, ptCurrent, ptDest);
                Size wpMarkerSize = new Size(5, 5);
                if(Waypoints != null && Waypoints.Length >= 2)
                {
                    Pen wpPen = new Pen(Brushes.Orange, 2);
                    PointF[] ptWaypoints = canvasHelper.CanvasToScreen(Waypoints);
                    g.DrawLines(wpPen, ptWaypoints);

                    foreach(PointF p in ptWaypoints)
                    {
                        DrawHelper.DrawCrossMarker(g, p, Pens.Red, wpMarkerSize);
                    }
                }
            }

           
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
            if (canvasBound.Contains(pts[1]) && Selected)
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

           

            return pois.ToArray();
        }

        public bool Manipulate(ref PointOfInterest poi, CanvasHelper canvasHelper, PointF ptStart, PointF ptEnd)
        {
            PointF poiCanvasLoc = new PointF();
            bool modified = false;
            PointF pt;
            if (poi.POIIndex == 0)
            {
                //move start point
                pt = StartPosition;
                StartPosition = ptEnd;
                poiCanvasLoc = StartPosition;

                modified = (pt != StartPosition);
                ResetPosition();

            }
            else if (poi.POIIndex == 1)
            {
                //move destination point
                pt = Destination;
                Destination = ptEnd;
                poiCanvasLoc = Destination;

                modified = (pt != Destination);
                ResetPosition();
            }

            poi.Position = canvasHelper.CanvasToScreen(poiCanvasLoc);

            if(modified && OnPositionChanged != null)
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
            //throw new NotImplementedException();
        }

        /*
        public SimAgent CreateSimAgent(SimulationParameters simParam, ZoneMap zoneMap) //create sim agent for use in simulation instance
        {
            SimAgent agent = new SimAgent()
            {
                ObjectID = ObjectID,
                BodyRadius = BodyRadius,
                StartPosition = StartPosition,
                Destination = Destination,

            };
            zoneMap.GenerateAgentWaypoint(this);

            agent.Waypoints = new Vec2[Waypoints.Length];
            for(int i = 0;i<Waypoints.Length;i++)
            {
                agent.Waypoints[i] = Waypoints[i];
            }


            
            agent.ResetState(simParam);

            return agent;
        }
        */
        public OldAgent CreateOldAgent(SimulationParameters simParam, ZoneMap zoneMap) //create sim agent for use in simulation instance
        {
            OldAgent agent = new OldAgent()
            {
                //ObjectID = ObjectID,
                IndexTag = ObjectID,

                //BodyRadius = BodyRadius,
                BodyRad = BodyRadius,

                //StartPosition = StartPosition,
                StartPos = new Vector2D(StartPosition.X,StartPosition.Y),
                

                //Destination = Destination,
                TargetPos = new Vector2D(Destination.X,Destination.Y)

            };
            if(zoneMap.GenerateAgentWaypoint(this))
            {
                agent.Waypoint = new Vector2D[Waypoints.Length];
                for (int i = 0; i < Waypoints.Length; i++)
                {
                    agent.Waypoint[i] = new Vector2D(Waypoints[i].X, Waypoints[i].Y);
                }
            }
            else
            {
                agent.Waypoint = null;
            }


            



            //agent.ResetState(simParam);

            return agent;
        }

        public void Recalculate()
        {
            ResetPosition();
            //throw new NotImplementedException();
        }
    }
}

using Newtonsoft.Json;
using Sesak.Commons;
using Sesak.Path;
using Sesak.Simulation;
using Sesak.SimulationObjects;
using Sesak.SimulationObjects.PropertiesControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Sesak
{
    enum AddModes
    {
        None = 0,
        Wall = 1,
        Door = 2,
        Agent = 3,
        ComfortZone = 4,
        EvacuationArea = 5

    }
    public partial class FormEditor : DockContent
    {
        //SimulationParameters simParam = new SimulationParameters();


        ZoneMap zoneMap = new ZoneMap();
        //SimulationEnvironment simEnvironment = new SimulationEnvironment();


        bool unsavedChanges { get; set; }
        const int MaxGrabDistance = 20;

        bool snapToGrid = true;

        AddModes addMode = AddModes.None;

        Wall newWall = null;
        Door newDoor = null;
        Agent newAgent = null;

        PointF ptComfortDown;
        PointF ptComfortUp;

        PointF ptEvacDown;
        PointF ptEvacUp;

        RectangleF evacTemp;

        CanvasHelper canvasHelper;
        IDrawableObject selectedObject = null;

        #region Viewport Manipulation Variables
        PointF panScreenAnchor = new PointF();
        PointF panCanvasAnchor = new PointF();

        #endregion
        public FormEditor()
        {
            InitializeComponent();
        }



        private void FormEditor_Load(object sender, EventArgs e)
        {
            unsavedChanges = false;

            SimulationEnvironment simEnvironment =  SimulationWorkspace.Instance.SimEnvironment;
            canvasHelper = new CanvasHelper(pbCanvas);
            canvasHelper.SimEnvironment = simEnvironment;
            //canvasHelper.zoneMap = zoneMap;

            canvasHelper.OnPanChanged += CanvasHelper_OnPanChanged;
            canvasHelper.OnScaleChanged += CanvasHelper_OnScaleChanged;
            canvasHelper.ShouldRedraw += CanvasHelper_ShouldRedraw;

            canvasHelper.OnGridChanged += CanvasHelper_OnGridChanged;
            canvasHelper.OnOriginChanged += CanvasHelper_OnOriginChanged;
            canvasHelper.OnSnapChanged += CanvasHelper_OnSnapChanged;
            canvasHelper.OnSnapSizeChanged += CanvasHelper_OnSnapSizeChanged;


            simEnvironment.OnObjectPositionChanged += SimEnvironment_OnObjectPositionChanged;

            this.MouseWheel += FormEditor_MouseWheel;

            
            

            setTooltips();
            

        }

        private void SimEnvironment_OnObjectPositionChanged(object sender, DrawableObjectEventArgs e)
        {
            IDrawableObject obj = e.DrawableObject;
            if (obj == null)
                return;

            if(obj is Door || obj is Wall)
            {
                UpdateZoneMap();
            }
            else if(obj is Agent)
            {
                float margin = ZoneMap.DefaultCellSize * 2;
                RectangleF newBound = SimulationWorkspace.Instance.SimEnvironment.GetMapBound(margin, margin, margin, margin);

                if (zoneMap.MapBound != newBound)
                {
                    UpdateZoneMap();
                    //ObstacleUpdated();
                    
                }

                zoneMap.GenerateAgentWaypoint((Agent)obj);

            }
        }

        private void CanvasHelper_OnSnapSizeChanged(object sender, EventArgs e)
        {
            UpdateSnapSizeChecked();

            //throw new NotImplementedException();
        }

        private void CanvasHelper_OnSnapChanged(object sender, EventArgs e)
        {
            btnSnap.BorderStyle = canvasHelper.DrawSnapPoint ? Border3DStyle.SunkenOuter : Border3DStyle.RaisedOuter;
            snapToolStripMenuItem.Checked = canvasHelper.DrawSnapPoint;
        }

        private void CanvasHelper_OnOriginChanged(object sender, EventArgs e)
        {
            btnOrigin.BorderStyle = canvasHelper.DrawOriginLine ? Border3DStyle.SunkenOuter : Border3DStyle.RaisedOuter;
            originToolStripMenuItem.Checked = canvasHelper.DrawOriginLine;

        }

        private void CanvasHelper_OnGridChanged(object sender, EventArgs e)
        {
            btnGrid.BorderStyle = canvasHelper.DrawAxisLine ? Border3DStyle.SunkenOuter : Border3DStyle.RaisedOuter;
            gridToolStripMenuItem.Checked = canvasHelper.DrawAxisLine;
        }

        void setTooltips()
        {
            tooltips.SetToolTip(btnAddWall, "Wall");
            tooltips.SetToolTip(btnAddDoor, "Door");
            tooltips.SetToolTip(btnAddAgent, "Agent");
            tooltips.SetToolTip(btnSetComfortRegion, "Comfort Test Zone");
            tooltips.SetToolTip(btnSetEvacuationArea, "Add Evacuation Area");
        }



        void InvalidateAgentWaypoints()
        {
            foreach (Agent agent in SimulationWorkspace.Instance.SimEnvironment.Agents)
            {
                
                agent.Waypoints = null;
            }
        }


        IPropControl GetPropControl()
        {
            foreach(Control ctrl in panelProperties.Controls)
            {
                if (ctrl is IPropControl)
                    return (IPropControl)ctrl;
            }
            return null;
        }
        private void CanvasHelper_ShouldRedraw()
        {
            //throw new NotImplementedException();
            //redrawTimer.Enabled = true;
            pbCanvas.Invalidate();

        }

        private void FormEditor_MouseWheel(object sender, MouseEventArgs e)
        {


            if (e.Delta > 0)
                canvasHelper.Scale += (int)(Math.Ceiling(canvasHelper.Scale / 10));
            //canvasHelper.ZoomIn();
            else if (e.Delta < 0)
                canvasHelper.Scale -= (int)(Math.Ceiling(canvasHelper.Scale / 10));
            //canvasHelper.ZoomOut();

        }

        private void CanvasHelper_OnScaleChanged(float scale)
        {
            trkZoom.Value = (int)scale;
        }

        private void CanvasHelper_OnPanChanged(PointF panOffset)
        {
            //lblCanvasCoordinate.Text = panOffset.ToString();
            //lblCanvasCoordinate.Tag = panOffset.ToString();
            //delayedUpdate.Enabled = false;
            //delayedUpdate.Enabled = true;
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.White);

            RectangleF comfortZone = canvasHelper.CanvasToScreen(SimulationWorkspace.Instance.SimEnvironment.ComfortTestZone);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 200)), comfortZone);

            if (addMode == AddModes.EvacuationArea)
            {
                RectangleF evacAreaEdit = canvasHelper.CanvasToScreen(evacTemp);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 255, 200)), evacAreaEdit);
            }
            


            canvasHelper.OverlayZoneMap(g, pbCanvas.Size, zoneMap);


            canvasHelper.Paint(g, pbCanvas.Size);
            float margin = ZoneMap.DefaultCellSize * 2;
            RectangleF bound = SimulationWorkspace.Instance.SimEnvironment.GetMapBound(margin, margin, margin, margin);

            RectangleF screenBound = canvasHelper.CanvasToScreen(bound);


            e.Graphics.DrawRectangles(Pens.Aqua, new RectangleF[] { screenBound });

            

            Font font = new Font("Consolas", 12, FontStyle.Bold);
            string s;
            SizeF sz;
            switch (addMode)
            {
                case AddModes.Wall:
                    s = "Create New Wall";
                    sz = e.Graphics.MeasureString(s, font);
                    e.Graphics.DrawString(s, font, Brushes.Magenta, pbCanvas.Width - sz.Width, 0);
                    break;
                case AddModes.Door:
                    s = "Create New Door";
                    sz = e.Graphics.MeasureString(s, font);
                    e.Graphics.DrawString(s, font, Brushes.Magenta, pbCanvas.Width - sz.Width, 0);
                    break;
                case AddModes.Agent:
                    s = "Create New Agent";
                    sz = e.Graphics.MeasureString(s, font);
                    e.Graphics.DrawString(s, font, Brushes.Magenta, pbCanvas.Width - sz.Width, 0);
                    break;
                case AddModes.ComfortZone:
                    s = "Set Comfort Test Zone";
                    sz = e.Graphics.MeasureString(s, font);
                    e.Graphics.DrawString(s, font, Brushes.Magenta, pbCanvas.Width - sz.Width, 0);
                    break;

                case AddModes.EvacuationArea:
                    s = "Add Evacuation Area";
                    sz = e.Graphics.MeasureString(s, font);
                    e.Graphics.DrawString(s, font, Brushes.Magenta, pbCanvas.Width - sz.Width, 0);
                    break;
            }

            TestDraw(g);
        }

        private void SetAddMode(AddModes mode)
        {
            addMode = mode;
            pbCanvas.Invalidate();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void pbCanvas_Resize(object sender, EventArgs e)
        {
            pbCanvas.Invalidate();
        }


        private void unselectAllObject()
        {
            foreach (EvacuationArea evac in SimulationWorkspace.Instance.SimEnvironment.EvacuationAreas)
            {
                evac.Selected = false;
            }
            foreach (Wall wall in SimulationWorkspace.Instance.SimEnvironment.Walls)
            {
                wall.Selected = false;
            }
            foreach (Door door in SimulationWorkspace.Instance.SimEnvironment.Doors)
            {
                door.Selected = false;
            }
            foreach (Agent agent in SimulationWorkspace.Instance.SimEnvironment.Agents)
            {
                agent.Selected = false;
            }
            
        }


        private void delayedUpdate_Tick(object sender, EventArgs e)
        {
            //lblCanvasCoordinate.Text = (string)lblCanvasCoordinate.Tag;
            delayedUpdate.Enabled = false;
        }

        private void redrawTimer_Tick(object sender, EventArgs e)
        {
            pbCanvas.Invalidate();
            redrawTimer.Enabled = false;
        }

        private void trkZoom_Scroll(object sender, EventArgs e)
        {
            canvasHelper.Scale = trkZoom.Value;
        }

        private void pbCanvas_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void pbCanvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Double click RMB pan to origin
            if (e.Button == MouseButtons.Right)
                canvasHelper.PanToOrigin();
            else if (e.Button == MouseButtons.Middle)
                canvasHelper.ResetScale();
        }

        private void clearProperties()
        {
            foreach(Control ctrl in panelProperties.Controls)
            {
                if(ctrl is IPropControl)
                {
                    IPropControl prop = (IPropControl)ctrl;
                    prop.OnPropertiesChanged -= Prop_OnPropertiesChanged;

                }
                ctrl.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }

        private void addProperties(Control ctrl)
        {
            panelProperties.Controls.Add(ctrl);
            ctrl.Dock = DockStyle.Fill;
            if (ctrl is IPropControl)
            {
                IPropControl prop = (IPropControl)ctrl;
                prop.OnPropertiesChanged += Prop_OnPropertiesChanged;
                prop.OnPropertiesNameChanged += Prop_OnPropertiesNameChanged;
            }
        }

        private void Prop_OnPropertiesNameChanged(object obj)
        {
            //refresh list
            RefreshObjectLists();

        }

        private void Prop_OnPropertiesChanged(object obj)
        {
            pbCanvas.Invalidate();
        }

        private void btnProp_Click(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            if(unsavedChanges)
            {
                if (MessageBox.Show("Do you want to save changes?","Unsaved Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //TODO: Do save environment here
                }
            }
            */
            //this.Close();
            if (MessageBox.Show("Exit application? Any unsaved work will be lost.", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                FormWorkspace.Instance.Close();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void btnAddWall_Click(object sender, EventArgs e)
        {
            unselectAllObject();
            SetAddMode(AddModes.Wall);

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }



        private void RefreshObjectLists()
        {
            lstObstacles.Items.Clear();
            lstObstacles.Items.AddRange(SimulationWorkspace.Instance.SimEnvironment.Doors.ToArray());
            lstObstacles.Items.AddRange(SimulationWorkspace.Instance.SimEnvironment.Walls.ToArray());

            lstAgents.Items.Clear();
            lstAgents.Items.AddRange(SimulationWorkspace.Instance.SimEnvironment.Agents.ToArray());

            lstEvacuationArea.Items.Clear();
            lstEvacuationArea.Items.AddRange(SimulationWorkspace.Instance.SimEnvironment.EvacuationAreas.ToArray());

            clearProperties();

        }

        #region CanvasMouse
        private void pbCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            PointF canvasPosition = canvasHelper.ScreenToCanvas(new PointF(e.X, e.Y), snapToGrid);
            //lblCanvasCoordinate.Tag = canvasPosition.ToString();

            //delayedUpdate.Enabled = false;
            //delayedUpdate.Enabled = true;


            if (e.Button == MouseButtons.Right) //panning
            {
                float xDelta = e.X - panScreenAnchor.X;
                float yDelta = e.Y - panScreenAnchor.Y;

                float newPanX = xDelta / canvasHelper.Scale + panCanvasAnchor.X;
                float newPanY = panCanvasAnchor.Y - yDelta / canvasHelper.Scale;

                canvasHelper.PanOffset = new PointF(newPanX, newPanY);
            }
            else if (e.Button == MouseButtons.Left)
            {

                PointF ptc = canvasHelper.ScreenToCanvas(e.Location, snapToGrid);

                switch(addMode)
                {
                    case AddModes.Wall:

                        newWall.SetPoints(newWall.P1, ptc);
                        break;
                    case AddModes.Door:

                        newDoor.SetPoints(newDoor.P1, ptc);
                        break;

                    case AddModes.Agent:
                        newAgent.Destination = ptc;
                        newAgent.ResetPosition();
                        //GenerateAgentWaypoint(newAgent);
                        break;
                    case AddModes.ComfortZone:
                        ptComfortUp = canvasPosition;
                        UpdateComfortTestZone();
                        break;
                    case AddModes.EvacuationArea:
                        ptEvacUp = canvasPosition;
                        UpdateEvacuationArea(false);
                        break;
                    default:
                        //manipulate
                        bool modified = false;
                        if (canvasHelper.ActivePOI.POIIndex >= 0 && SimulationWorkspace.Instance.SimEnvironment.DrawableObjects.ContainsKey(canvasHelper.ActivePOI.ObjectID))
                        {
                            if (ptDown != e.Location)
                            {
                                modified = SimulationWorkspace.Instance.SimEnvironment.DrawableObjects[canvasHelper.ActivePOI.ObjectID].Manipulate(ref canvasHelper.ActivePOI, canvasHelper, new PointF(), ptc);
                                ptDown = e.Location;
                            }
                            if (!SimulationWorkspace.Instance.SimEnvironment.DrawableObjects[canvasHelper.ActivePOI.ObjectID].Selected)
                            {
                                clearProperties();
                                unselectAllObject();
                                //simEnvironment.DrawableObjects[canvasHelper.ActivePOI.ObjectID].Selected = true;
                                TrySelectObject(canvasHelper.ActivePOI.ObjectID);
                            }
                            else
                            {
                                IPropControl prop = GetPropControl();
                                if (prop != null)
                                    prop.RefreshValues();


                            }

                        }
                        break;
                }
                pbCanvas.Invalidate();
            }
            else
            {
                bool markInvalidate = false;
                bool poiActive = false;
                /*
                if(selectedObject is Agent)
                {
                    //canvasHelper.ptNextWaypointTest = canvasHelper.ScreenToCanvas(e.Location);
                    //markInvalidate = true;
                }
                */

                if (addMode == AddModes.None)
                {
                    //pois
                    List<PointOfInterest> pois = new List<PointOfInterest>();


                    RectangleF bound = canvasHelper.ScreenToCanvas(new RectangleF(new PointF(e.X - MaxGrabDistance / 2, e.Y - MaxGrabDistance / 2), new SizeF(MaxGrabDistance, MaxGrabDistance)));

                    foreach (KeyValuePair<int, IDrawableObject> obj in SimulationWorkspace.Instance.SimEnvironment.DrawableObjects)
                    {
                        pois.AddRange(obj.Value.GetPointOfInterests(canvasHelper, bound));
                    }
                    
                    poiActive = (canvasHelper.ActivePOI.POIIndex >= 0);

                    canvasHelper.ResetPOI();

                    double lastDist = MaxGrabDistance;

                    foreach (PointOfInterest poi in pois)
                    {
                        //grab nearest
                        double dist = MathHelper.GetDistance(e.Location, poi.Position);
                        if (dist < lastDist)
                        {
                            canvasHelper.ActivePOI = poi;
                            if (poiActive != (poi.POIIndex >= 0))
                                markInvalidate = true;

                            lastDist = dist;
                        }
                        break;
                    }

                    if (canvasHelper.ActivePOI.POIIndex >= 0)
                    {
                        if (pbCanvas.Cursor != Cursors.Hand)
                            pbCanvas.Cursor = canvasHelper.ActivePOI.Pointer;

                    }
                    else if (pbCanvas.Cursor == Cursors.Hand)
                    {
                        pbCanvas.Cursor = Cursors.Cross;
                    }

                    if(markInvalidate)
                        pbCanvas.Invalidate();
                }
                else
                {
                    if (pbCanvas.Cursor != Cursors.Cross)
                        pbCanvas.Cursor = Cursors.Cross;
                }

            }
        }
        private void UpdateComfortTestZone()
        {
            RectangleF rect = new RectangleF(
                ptComfortDown.X < ptComfortUp.X ? ptComfortDown.X : ptComfortUp.X,
                ptComfortDown.Y < ptComfortUp.Y ? ptComfortDown.Y : ptComfortUp.Y,
                (float)Math.Abs(ptComfortDown.X - ptComfortUp.X),
                (float)Math.Abs(ptComfortDown.Y - ptComfortUp.Y)
            );

            SimulationWorkspace.Instance.SimEnvironment.ComfortTestZone = rect;
        }
        private void UpdateEvacuationArea(bool addArea)
        {
            RectangleF rect = new RectangleF(
                ptEvacDown.X < ptEvacUp.X ? ptEvacDown.X : ptEvacUp.X,
                ptEvacDown.Y < ptEvacUp.Y ? ptEvacDown.Y : ptEvacUp.Y,
                (float)Math.Abs(ptEvacDown.X - ptEvacUp.X),
                (float)Math.Abs(ptEvacDown.Y - ptEvacUp.Y)
            );



            //SimulationWorkspace.Instance.SimEnvironment.ComfortTestZone = rect;
            evacTemp = rect;
            if (addArea)
            {
                EvacuationArea evac = SimulationWorkspace.Instance.SimEnvironment.CreateEvacuationArea();
                evac.Area = evacTemp;

                RefreshEvacuationAreaList();
            }
        }

        private void RefreshEvacuationAreaList()
        {
            lstEvacuationArea.Items.Clear();
            foreach(EvacuationArea evac in SimulationWorkspace.Instance.SimEnvironment.EvacuationAreas)
            {
                lstEvacuationArea.Items.Add(evac);
            }
        }

        Point ptDown;
        private void pbCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                panScreenAnchor = new PointF(e.X, e.Y);
                panCanvasAnchor = canvasHelper.PanOffset;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (addMode != AddModes.None)
                {

                    PointF ptc = canvasHelper.ScreenToCanvas(e.Location, snapToGrid);
       
                    if (addMode == AddModes.Wall)
                    {
                        newWall = SimulationWorkspace.Instance.SimEnvironment.CreateWall();

                        newWall.Selected = true;
                        newWall.SetPoints(ptc, ptc);
                    }
                    else if(addMode == AddModes.Door)
                    {
                        newDoor = SimulationWorkspace.Instance.SimEnvironment.CreateDoor();

                        newDoor.Selected = true;
                        newDoor.SetPoints(ptc, ptc);

                    }
                    else if(addMode == AddModes.Agent)
                    {
                        newAgent = SimulationWorkspace.Instance.SimEnvironment.CreateAgent();
                        newAgent.Selected = true;
                        newAgent.StartPosition = ptc;
                        newAgent.Destination = ptc;
                        newAgent.ResetPosition();

                    }
                    else if(addMode == AddModes.ComfortZone)
                    {
                        ptComfortDown = ptc;
                        ptComfortUp = ptc;
                        UpdateComfortTestZone();
                    }
                    else if (addMode == AddModes.EvacuationArea)
                    {
                        ptEvacDown = ptc;
                        ptEvacUp = ptc;
                        UpdateEvacuationArea(false);
                    }
                }
                else
                {
                    ptDown = e.Location;
                    if (canvasHelper.ActivePOI.POIIndex >= 0 && SimulationWorkspace.Instance.SimEnvironment.DrawableObjects.ContainsKey(canvasHelper.ActivePOI.ObjectID))
                    {
                        if (!SimulationWorkspace.Instance.SimEnvironment.DrawableObjects[canvasHelper.ActivePOI.ObjectID].Selected)
                        {
                            clearProperties();
                            unselectAllObject();
                            //simEnvironment.DrawableObjects[canvasHelper.ActivePOI.ObjectID].Selected = true;
                            TrySelectObject(canvasHelper.ActivePOI.ObjectID);
                        }

                    }
                }
                

                pbCanvas.Invalidate();
            }
        }
        private void pbCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (addMode != AddModes.None)
                {
                    PointF ptc = canvasHelper.ScreenToCanvas(e.Location, snapToGrid);
                    int objId = -1;
                    bool modified = false;

                    if (addMode == AddModes.Wall)
                    {
                        modified = newWall.SetPoints(newWall.P1, ptc);
                        objId = newWall.ObjectID;
                    }
                    else if (addMode == AddModes.Door)
                    {
                        modified = newDoor.SetPoints(newDoor.P1, ptc);
                        objId = newDoor.ObjectID;
                    }
                    else if(addMode == AddModes.Agent)
                    {
                        newAgent.Destination = ptc;
                        newAgent.ResetPosition();
                        objId = newAgent.ObjectID;
                    }
                    else if(addMode == AddModes.ComfortZone)
                    {
                        ptComfortUp = ptc;
                        UpdateComfortTestZone();
                    }
                    else if (addMode == AddModes.EvacuationArea)
                    {
                        ptEvacUp = ptc;
                        UpdateEvacuationArea(true);
                    }
                    SetAddMode(AddModes.None);
                    unselectAllObject();
                    RefreshObjectLists();
                    if (objId != -1)
                        TrySelectObject(objId);




                }
            }
        }
        #endregion

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void FormEditor_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void FormEditor_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                //cancel all mode
                if (addMode != AddModes.None)
                    SetAddMode(AddModes.None);

                clearProperties();
                unselectAllObject();
                RefreshObjectLists();
                pbCanvas.Invalidate();
            }
            if(e.KeyCode == Keys.Delete)
            {
                //delete selected object
                if (selectedObject == null)
                    return;


                TryDeleteObject(selectedObject.ObjectID);
                selectedObject = null;

                clearProperties();
                unselectAllObject();
                RefreshObjectLists();
                pbCanvas.Invalidate();
            }
        }

        public void TryDeleteObject(int objectId)
        {
            IDrawableObject obj = SimulationWorkspace.Instance.SimEnvironment.RemoveObject(objectId);
            if (obj == null)
                return;

            if(obj is Wall || obj is Door)
                UpdateZoneMap();
        }

        public void TrySelectObject(int objectId)
        {
            lstAgents.ClearSelected();
            lstObstacles.ClearSelected();
            lstEvacuationArea.ClearSelected();

            for(int i = 0;i<lstObstacles.Items.Count;i++)
            {
                IDrawableObject obj = (IDrawableObject)lstObstacles.Items[i];
                if(obj.ObjectID == objectId)
                {
                    lstObstacles.SelectedIndex = i;
                    selectedObject = obj;
                    return;
                }
            }
            for(int i= 0;i< lstAgents.Items.Count;i++)
            {
                IDrawableObject obj = (IDrawableObject)lstAgents.Items[i];
                if (obj.ObjectID == objectId)
                {
                    lstAgents.SelectedIndex = i;
                    selectedObject = obj;
                    zoneMap.GenerateAgentWaypoint((Agent)obj);
                    return;
                }
            }
            for (int i = 0; i < lstEvacuationArea.Items.Count; i++)
            {
                IDrawableObject obj = (IDrawableObject)lstEvacuationArea.Items[i];
                if (obj.ObjectID == objectId)
                {
                    lstEvacuationArea.SelectedIndex = i;
                    selectedObject = obj;
                    return;
                }
            }
            selectedObject = null;

            
        }


        private void lstObstacles_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearProperties();
            unselectAllObject();

            lstAgents.ClearSelected();
            lstEvacuationArea.ClearSelected();

            if (lstObstacles.SelectedItem is IDrawableObject)
            {
                IDrawableObject obj = (IDrawableObject)lstObstacles.SelectedItem;
                obj.Selected = true;
                Control prop = obj.CreatePropertiesControl();
                if(prop != null)
                {
                    addProperties(prop);
                }
                selectedObject = obj;
            }
            pbCanvas.Invalidate();
            
        }

        

        private void btnSnap_Click(object sender, EventArgs e)
        {
            snapToGrid = !snapToGrid;
            
            //btnSnap.BorderStyle = snapToGrid ? Border3DStyle.SunkenOuter : Border3DStyle.RaisedOuter;
            canvasHelper.DrawSnapPoint = snapToGrid;
            pbCanvas.Invalidate();
        }

        private void btnGrid_Click(object sender, EventArgs e)
        {
            canvasHelper.DrawAxisLine = !canvasHelper.DrawAxisLine;
            canvasHelper.DrawAxisText = canvasHelper.DrawAxisLine;

            
            pbCanvas.Invalidate();
        }
        private void SetSnapSizePresetClick(object sender, EventArgs e)
        {
            ToolStripMenuItem btn = (ToolStripMenuItem)sender;
            float v;
            if (btn == null || btn.Tag == null || !float.TryParse(btn.Tag.ToString(),out v))
                return;

            SetSnapSize(v);

        }
        private void SetSnapSize(float v)
        {
            if (v <= 0)
                return;

            canvasHelper.SnapGridSize = v;
        }
        private void UpdateSnapSizeChecked()
        {
            ToolStripMenuItem[] presetSizeCtrls = { mToolStripMenuItem, mToolStripMenuItem1, mToolStripMenuItem2, mToolStripMenuItem3 };

            bool presetSize = false;
            foreach(ToolStripMenuItem item in presetSizeCtrls)
            {
                float v;
                if (item.Tag == null || (!float.TryParse(item.Tag.ToString(), out v)))
                {
                    item.Checked = false;
                    continue;
                }
                if (v == canvasHelper.SnapGridSize)
                {
                    item.Checked = true;
                    presetSize = true;
                    continue;
                }
                
                item.Checked = false;
            }

            if(presetSize)
            {
                btnCustomSnap.Text = "Custom";
                btnCustomSnap.Checked = false;
            }
            else
            {
                btnCustomSnap.Text = "Custom (" + canvasHelper.SnapGridSize.ToString() + ")";
                btnCustomSnap.Checked = true;
            }

            
            
        }
        private void btnOrigin_Click(object sender, EventArgs e)
        {
            canvasHelper.DrawOriginLine = !canvasHelper.DrawOriginLine;
            //btnOrigin.BorderStyle = canvasHelper.DrawOriginLine ? Border3DStyle.SunkenOuter : Border3DStyle.RaisedOuter;
            pbCanvas.Invalidate();
        }

        private void btnAddDoor_Click(object sender, EventArgs e)
        {
            unselectAllObject();
            SetAddMode(AddModes.Door);
        }

        private void btnAddAgent_Click(object sender, EventArgs e)
        {
            unselectAllObject();
            SetAddMode(AddModes.Agent);
        }

        private void lstAgents_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearProperties();
            unselectAllObject();
            lstObstacles.ClearSelected();
            lstEvacuationArea.ClearSelected();

            if (lstAgents.SelectedItem is IDrawableObject)
            {
                IDrawableObject obj = (IDrawableObject)lstAgents.SelectedItem;
                obj.Selected = true;
                Control prop = obj.CreatePropertiesControl();
                if (prop != null)
                {
                    addProperties(prop);
                }
                selectedObject = obj;
            }
            pbCanvas.Invalidate();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    File.WriteAllText(dlg.FileName, SimulationWorkspace.Instance.SimEnvironment.Export());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Export", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void snapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSnap_Click(this, e);
        }

        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnGrid_Click(this, e);
        }

        private void originToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnOrigin_Click(this, e);
        }

        private void btnCustomSnap_Click(object sender, EventArgs e)
        {
            using(FormInputDialogBox dlg = new FormInputDialogBox("Snap Size","Input custom snap size (m).",canvasHelper.SnapGridSize.ToString()))
            {
                while(true)
                {
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    float v;
                    if(float.TryParse(dlg.InputText,out v) && v > 0)
                    {
                        SetSnapSize(v);
                        return;
                    }
                }
                
            }
        }
        private void UpdateZoneMap()
        {
            zoneMap.GenerateZoneMap(SimulationWorkspace.Instance.SimEnvironment);
            InvalidateAgentWaypoints();
        }

        private void runSimulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SimulationWorkspace.Instance.RunSimulation();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "Sim Environment|*.jsv|All Files|*";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    File.WriteAllText(dlg.FileName, SimulationWorkspace.Instance.SimEnvironment.Export());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Sim Environment|*.jsv|All Files|*";
                if (dlg.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    string s = File.ReadAllText(dlg.FileName);
                    SimulationWorkspace.Instance.SimEnvironment.Import(s);
                    ptComfortDown.X = SimulationWorkspace.Instance.SimEnvironment.ComfortTestZone.Left;
                    ptComfortDown.Y = SimulationWorkspace.Instance.SimEnvironment.ComfortTestZone.Top;

                    ptComfortUp.X = SimulationWorkspace.Instance.SimEnvironment.ComfortTestZone.Right;
                    ptComfortUp.Y = SimulationWorkspace.Instance.SimEnvironment.ComfortTestZone.Bottom;

                    UpdateZoneMap();
                    RefreshObjectLists();
                    pbCanvas.Invalidate();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Open", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
            }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nodeTest =  zoneMap.GenerateZoneMap(SimulationWorkspace.Instance.SimEnvironment);

            

            pbCanvas.Invalidate();

        }

        PointF[] nodeTest = null;
        private void TestDraw(Graphics g)
        {
            if (nodeTest == null)
                return;

            PointF[] pts = canvasHelper.CanvasToScreen(nodeTest);

            foreach (PointF pt in pts)
            {
                g.FillRectangle(Brushes.Red, pt.X, pt.Y, 1, 1);
            }
        }

        private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSimulationParameter dlg = new FormSimulationParameter();
            SimulationParameters simParam = SimulationWorkspace.Instance.SimParameter;
            dlg.LoadParameter(simParam);
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            dlg.ApplyParameter(ref simParam);

            SimulationWorkspace.Instance.SaveLastSimParameter();
        }

        private void btnSetComfortRegion_Click(object sender, EventArgs e)
        {
            unselectAllObject();
            SetAddMode(AddModes.ComfortZone);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(SimulationWorkspace.Instance.SimEnvironment.Agents.Count > 0 || SimulationWorkspace.Instance.SimEnvironment.Walls.Count > 0 || SimulationWorkspace.Instance.SimEnvironment.Doors.Count > 0)
            {
                if (MessageBox.Show("Clear current environment?", "New Environment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            if (addMode != AddModes.None)
                SetAddMode(AddModes.None);

            clearProperties();
            unselectAllObject();

            SimulationWorkspace.Instance.SimEnvironment.Agents.Clear();
            SimulationWorkspace.Instance.SimEnvironment.Doors.Clear();
            SimulationWorkspace.Instance.SimEnvironment.Walls.Clear();
            SimulationWorkspace.Instance.SimEnvironment.EvacuationAreas.Clear();

            RefreshObjectLists();
            pbCanvas.Invalidate();

        }

        private void btnSetEvacuationArea_Click(object sender, EventArgs e)
        {
            unselectAllObject();
            if(addMode != AddModes.EvacuationArea)
            {
                evacTemp = new RectangleF();
            }
            SetAddMode(AddModes.EvacuationArea);
        }

        private void lstEvacuationArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearProperties();
            unselectAllObject();

            lstObstacles.ClearSelected();
            lstAgents.ClearSelected();

            if (lstEvacuationArea.SelectedItem is IDrawableObject)
            {
                IDrawableObject obj = (IDrawableObject)lstEvacuationArea.SelectedItem;
                obj.Selected = true;
                Control prop = obj.CreatePropertiesControl();
                if (prop != null)
                {
                    addProperties(prop);
                }
                selectedObject = obj;
            }
            pbCanvas.Invalidate();
            
        }
    }
}

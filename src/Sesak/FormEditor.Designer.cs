namespace Sesak
{
    partial class FormEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditor));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSnap = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnGrid = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnOrigin = new System.Windows.Forms.ToolStripStatusLabel();
            this.delayedUpdate = new System.Windows.Forms.Timer(this.components);
            this.redrawTimer = new System.Windows.Forms.Timer(this.components);
            this.trkZoom = new System.Windows.Forms.TrackBar();
            this.panelProperties = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSetEvacuationArea = new System.Windows.Forms.Button();
            this.btnSetComfortRegion = new System.Windows.Forms.Button();
            this.btnAddDoor = new System.Windows.Forms.Button();
            this.btnAddAgent = new System.Windows.Forms.Button();
            this.btnAddWall = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.originToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.snapSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCustomSnap = new System.Windows.Forms.ToolStripMenuItem();
            this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runSimulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lstEvacuationArea = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstObstacles = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstAgents = new System.Windows.Forms.ListBox();
            this.tooltips = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel6,
            this.btnSnap,
            this.btnGrid,
            this.btnOrigin});
            this.statusStrip1.Location = new System.Drawing.Point(0, 637);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 24);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(879, 19);
            this.toolStripStatusLabel6.Spring = true;
            // 
            // btnSnap
            // 
            this.btnSnap.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.btnSnap.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.Size = new System.Drawing.Size(37, 19);
            this.btnSnap.Text = "Snap";
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // btnGrid
            // 
            this.btnGrid.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.btnGrid.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.btnGrid.Name = "btnGrid";
            this.btnGrid.Size = new System.Drawing.Size(33, 19);
            this.btnGrid.Text = "Grid";
            this.btnGrid.Click += new System.EventHandler(this.btnGrid_Click);
            // 
            // btnOrigin
            // 
            this.btnOrigin.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.btnOrigin.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.btnOrigin.Name = "btnOrigin";
            this.btnOrigin.Size = new System.Drawing.Size(44, 19);
            this.btnOrigin.Text = "Origin";
            this.btnOrigin.Click += new System.EventHandler(this.btnOrigin_Click);
            // 
            // delayedUpdate
            // 
            this.delayedUpdate.Interval = 50;
            this.delayedUpdate.Tick += new System.EventHandler(this.delayedUpdate_Tick);
            // 
            // redrawTimer
            // 
            this.redrawTimer.Interval = 1;
            this.redrawTimer.Tick += new System.EventHandler(this.redrawTimer_Tick);
            // 
            // trkZoom
            // 
            this.trkZoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkZoom.Location = new System.Drawing.Point(6, 3);
            this.trkZoom.Maximum = 500;
            this.trkZoom.Minimum = 1;
            this.trkZoom.Name = "trkZoom";
            this.trkZoom.Size = new System.Drawing.Size(223, 45);
            this.trkZoom.TabIndex = 2;
            this.trkZoom.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trkZoom.Value = 10;
            this.trkZoom.Scroll += new System.EventHandler(this.trkZoom_Scroll);
            // 
            // panelProperties
            // 
            this.panelProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProperties.Location = new System.Drawing.Point(0, 0);
            this.panelProperties.Name = "panelProperties";
            this.panelProperties.Size = new System.Drawing.Size(232, 257);
            this.panelProperties.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSetEvacuationArea);
            this.panel1.Controls.Add(this.btnSetComfortRegion);
            this.panel1.Controls.Add(this.btnAddDoor);
            this.panel1.Controls.Add(this.btnAddAgent);
            this.panel1.Controls.Add(this.btnAddWall);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(50, 613);
            this.panel1.TabIndex = 5;
            // 
            // btnSetEvacuationArea
            // 
            this.btnSetEvacuationArea.Image = ((System.Drawing.Image)(resources.GetObject("btnSetEvacuationArea.Image")));
            this.btnSetEvacuationArea.Location = new System.Drawing.Point(3, 163);
            this.btnSetEvacuationArea.Name = "btnSetEvacuationArea";
            this.btnSetEvacuationArea.Size = new System.Drawing.Size(44, 34);
            this.btnSetEvacuationArea.TabIndex = 4;
            this.btnSetEvacuationArea.UseVisualStyleBackColor = true;
            this.btnSetEvacuationArea.Click += new System.EventHandler(this.btnSetEvacuationArea_Click);
            // 
            // btnSetComfortRegion
            // 
            this.btnSetComfortRegion.Image = ((System.Drawing.Image)(resources.GetObject("btnSetComfortRegion.Image")));
            this.btnSetComfortRegion.Location = new System.Drawing.Point(3, 123);
            this.btnSetComfortRegion.Name = "btnSetComfortRegion";
            this.btnSetComfortRegion.Size = new System.Drawing.Size(44, 34);
            this.btnSetComfortRegion.TabIndex = 3;
            this.btnSetComfortRegion.UseVisualStyleBackColor = true;
            this.btnSetComfortRegion.Click += new System.EventHandler(this.btnSetComfortRegion_Click);
            // 
            // btnAddDoor
            // 
            this.btnAddDoor.Image = ((System.Drawing.Image)(resources.GetObject("btnAddDoor.Image")));
            this.btnAddDoor.Location = new System.Drawing.Point(3, 43);
            this.btnAddDoor.Name = "btnAddDoor";
            this.btnAddDoor.Size = new System.Drawing.Size(44, 34);
            this.btnAddDoor.TabIndex = 2;
            this.btnAddDoor.UseVisualStyleBackColor = true;
            this.btnAddDoor.Click += new System.EventHandler(this.btnAddDoor_Click);
            // 
            // btnAddAgent
            // 
            this.btnAddAgent.Image = ((System.Drawing.Image)(resources.GetObject("btnAddAgent.Image")));
            this.btnAddAgent.Location = new System.Drawing.Point(3, 83);
            this.btnAddAgent.Name = "btnAddAgent";
            this.btnAddAgent.Size = new System.Drawing.Size(44, 34);
            this.btnAddAgent.TabIndex = 1;
            this.btnAddAgent.UseVisualStyleBackColor = true;
            this.btnAddAgent.Click += new System.EventHandler(this.btnAddAgent_Click);
            // 
            // btnAddWall
            // 
            this.btnAddWall.Image = ((System.Drawing.Image)(resources.GetObject("btnAddWall.Image")));
            this.btnAddWall.Location = new System.Drawing.Point(3, 3);
            this.btnAddWall.Name = "btnAddWall";
            this.btnAddWall.Size = new System.Drawing.Size(44, 34);
            this.btnAddWall.TabIndex = 0;
            this.btnAddWall.UseVisualStyleBackColor = true;
            this.btnAddWall.Click += new System.EventHandler(this.btnAddWall_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editorToolStripMenuItem,
            this.simulationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripMenuItem5,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(177, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.closeToolStripMenuItem.Text = "E&xit";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // editorToolStripMenuItem
            // 
            this.editorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snapToolStripMenuItem,
            this.gridToolStripMenuItem,
            this.originToolStripMenuItem,
            this.toolStripMenuItem2,
            this.snapSizeToolStripMenuItem});
            this.editorToolStripMenuItem.Name = "editorToolStripMenuItem";
            this.editorToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.editorToolStripMenuItem.Text = "&View";
            // 
            // snapToolStripMenuItem
            // 
            this.snapToolStripMenuItem.Checked = true;
            this.snapToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.snapToolStripMenuItem.Name = "snapToolStripMenuItem";
            this.snapToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.snapToolStripMenuItem.Text = "&Snap";
            this.snapToolStripMenuItem.Click += new System.EventHandler(this.snapToolStripMenuItem_Click);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.Checked = true;
            this.gridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.gridToolStripMenuItem.Text = "&Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // originToolStripMenuItem
            // 
            this.originToolStripMenuItem.Checked = true;
            this.originToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.originToolStripMenuItem.Name = "originToolStripMenuItem";
            this.originToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.originToolStripMenuItem.Text = "&Origin";
            this.originToolStripMenuItem.Click += new System.EventHandler(this.originToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(120, 6);
            // 
            // snapSizeToolStripMenuItem
            // 
            this.snapSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mToolStripMenuItem,
            this.mToolStripMenuItem1,
            this.mToolStripMenuItem2,
            this.mToolStripMenuItem3,
            this.toolStripMenuItem3,
            this.btnCustomSnap});
            this.snapSizeToolStripMenuItem.Name = "snapSizeToolStripMenuItem";
            this.snapSizeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.snapSizeToolStripMenuItem.Text = "Snap Size";
            // 
            // mToolStripMenuItem
            // 
            this.mToolStripMenuItem.Name = "mToolStripMenuItem";
            this.mToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.mToolStripMenuItem.Tag = "0.1";
            this.mToolStripMenuItem.Text = "0.1m";
            this.mToolStripMenuItem.Click += new System.EventHandler(this.SetSnapSizePresetClick);
            // 
            // mToolStripMenuItem1
            // 
            this.mToolStripMenuItem1.Name = "mToolStripMenuItem1";
            this.mToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.mToolStripMenuItem1.Tag = "0.2";
            this.mToolStripMenuItem1.Text = "0.2m";
            this.mToolStripMenuItem1.Click += new System.EventHandler(this.SetSnapSizePresetClick);
            // 
            // mToolStripMenuItem2
            // 
            this.mToolStripMenuItem2.Checked = true;
            this.mToolStripMenuItem2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mToolStripMenuItem2.Name = "mToolStripMenuItem2";
            this.mToolStripMenuItem2.Size = new System.Drawing.Size(116, 22);
            this.mToolStripMenuItem2.Tag = "0.5";
            this.mToolStripMenuItem2.Text = "0.5m";
            this.mToolStripMenuItem2.Click += new System.EventHandler(this.SetSnapSizePresetClick);
            // 
            // mToolStripMenuItem3
            // 
            this.mToolStripMenuItem3.Name = "mToolStripMenuItem3";
            this.mToolStripMenuItem3.Size = new System.Drawing.Size(116, 22);
            this.mToolStripMenuItem3.Tag = "1";
            this.mToolStripMenuItem3.Text = "1.0m";
            this.mToolStripMenuItem3.Click += new System.EventHandler(this.SetSnapSizePresetClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(113, 6);
            // 
            // btnCustomSnap
            // 
            this.btnCustomSnap.Name = "btnCustomSnap";
            this.btnCustomSnap.Size = new System.Drawing.Size(116, 22);
            this.btnCustomSnap.Text = "Custom";
            this.btnCustomSnap.Click += new System.EventHandler(this.btnCustomSnap_Click);
            // 
            // simulationToolStripMenuItem
            // 
            this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runSimulationToolStripMenuItem,
            this.toolStripMenuItem4,
            this.parametersToolStripMenuItem});
            this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
            this.simulationToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.simulationToolStripMenuItem.Text = "&Simulation";
            // 
            // runSimulationToolStripMenuItem
            // 
            this.runSimulationToolStripMenuItem.Name = "runSimulationToolStripMenuItem";
            this.runSimulationToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.runSimulationToolStripMenuItem.Text = "&Run Simulation";
            this.runSimulationToolStripMenuItem.Click += new System.EventHandler(this.runSimulationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 6);
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.parametersToolStripMenuItem.Text = "&Parameters";
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.parametersToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(50, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pbCanvas);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 200;
            this.splitContainer1.Size = new System.Drawing.Size(958, 613);
            this.splitContainer1.SplitterDistance = 722;
            this.splitContainer1.TabIndex = 7;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // pbCanvas
            // 
            this.pbCanvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCanvas.Location = new System.Drawing.Point(0, 0);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(722, 613);
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            this.pbCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pbCanvas_Paint);
            this.pbCanvas.DoubleClick += new System.EventHandler(this.pbCanvas_DoubleClick);
            this.pbCanvas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDoubleClick);
            this.pbCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseDown);
            this.pbCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseMove);
            this.pbCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbCanvas_MouseUp);
            this.pbCanvas.Resize += new System.EventHandler(this.pbCanvas_Resize);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel1.Controls.Add(this.trkZoom);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panelProperties);
            this.splitContainer2.Size = new System.Drawing.Size(232, 613);
            this.splitContainer2.SplitterDistance = 352;
            this.splitContainer2.TabIndex = 4;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lstEvacuationArea);
            this.groupBox3.Location = new System.Drawing.Point(0, 248);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(229, 96);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Evacuation Area";
            // 
            // lstEvacuationArea
            // 
            this.lstEvacuationArea.FormattingEnabled = true;
            this.lstEvacuationArea.Location = new System.Drawing.Point(6, 19);
            this.lstEvacuationArea.Name = "lstEvacuationArea";
            this.lstEvacuationArea.Size = new System.Drawing.Size(214, 69);
            this.lstEvacuationArea.TabIndex = 0;
            this.lstEvacuationArea.SelectedIndexChanged += new System.EventHandler(this.lstEvacuationArea_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lstObstacles);
            this.groupBox1.Location = new System.Drawing.Point(0, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 97);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Obstacles";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lstObstacles
            // 
            this.lstObstacles.FormattingEnabled = true;
            this.lstObstacles.Location = new System.Drawing.Point(9, 19);
            this.lstObstacles.Name = "lstObstacles";
            this.lstObstacles.Size = new System.Drawing.Size(214, 69);
            this.lstObstacles.TabIndex = 0;
            this.lstObstacles.SelectedIndexChanged += new System.EventHandler(this.lstObstacles_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lstAgents);
            this.groupBox2.Location = new System.Drawing.Point(0, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(229, 96);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Agents";
            // 
            // lstAgents
            // 
            this.lstAgents.FormattingEnabled = true;
            this.lstAgents.Location = new System.Drawing.Point(6, 19);
            this.lstAgents.Name = "lstAgents";
            this.lstAgents.Size = new System.Drawing.Size(214, 69);
            this.lstAgents.TabIndex = 0;
            this.lstAgents.SelectedIndexChanged += new System.EventHandler(this.lstAgents_SelectedIndexChanged);
            // 
            // FormEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 661);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Environment Editor";
            this.Load += new System.EventHandler(this.FormEditor_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormEditor_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormEditor_KeyUp);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkZoom)).EndInit();
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Timer delayedUpdate;
        private System.Windows.Forms.Timer redrawTimer;
        private System.Windows.Forms.TrackBar trkZoom;
        private System.Windows.Forms.Panel panelProperties;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Button btnAddDoor;
        private System.Windows.Forms.Button btnAddAgent;
        private System.Windows.Forms.Button btnAddWall;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolTip tooltips;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstObstacles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstAgents;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel btnSnap;
        private System.Windows.Forms.ToolStripStatusLabel btnGrid;
        private System.Windows.Forms.ToolStripStatusLabel btnOrigin;
        private System.Windows.Forms.ToolStripMenuItem editorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem originToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem snapSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mToolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem btnCustomSnap;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runSimulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.Button btnSetComfortRegion;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.Button btnSetEvacuationArea;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lstEvacuationArea;
    }
}
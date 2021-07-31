namespace Sesak.Simulation
{
    partial class FormPlot
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
            this.tmrFrame = new System.Windows.Forms.Timer(this.components);
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.tmrDelayClose = new System.Windows.Forms.Timer(this.components);
            this.panelControl = new System.Windows.Forms.Panel();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.trkPlayback = new System.Windows.Forms.TrackBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmrOptimizerUpdateFrame = new System.Windows.Forms.Timer(this.components);
            this.x480ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x600ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x720ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x1080ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setViewportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblViewportSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkPlayback)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrFrame
            // 
            this.tmrFrame.Enabled = true;
            this.tmrFrame.Interval = 1;
            this.tmrFrame.Tick += new System.EventHandler(this.tmrFrame_Tick);
            // 
            // pbMain
            // 
            this.pbMain.BackColor = System.Drawing.Color.White;
            this.pbMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMain.Location = new System.Drawing.Point(0, 24);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(784, 462);
            this.pbMain.TabIndex = 1;
            this.pbMain.TabStop = false;
            this.pbMain.SizeChanged += new System.EventHandler(this.pbMain_SizeChanged);
            this.pbMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMain_Paint);
            this.pbMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseDown);
            this.pbMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseMove);
            this.pbMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbMain_MouseUp);
            this.pbMain.Move += new System.EventHandler(this.pbMain_Move);
            // 
            // tmrDelayClose
            // 
            this.tmrDelayClose.Interval = 1000;
            this.tmrDelayClose.Tick += new System.EventHandler(this.tmrDelayClose_Tick);
            // 
            // panelControl
            // 
            this.panelControl.Controls.Add(this.lblViewportSize);
            this.panelControl.Controls.Add(this.btnStop);
            this.panelControl.Controls.Add(this.btnPlay);
            this.panelControl.Controls.Add(this.trkPlayback);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 486);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(784, 75);
            this.panelControl.TabIndex = 2;
            // 
            // btnStop
            // 
            this.btnStop.Image = global::Sesak.Properties.Resources.stop;
            this.btnStop.Location = new System.Drawing.Point(52, 38);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(43, 34);
            this.btnStop.TabIndex = 2;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = global::Sesak.Properties.Resources.play;
            this.btnPlay.Location = new System.Drawing.Point(3, 38);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(43, 34);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // trkPlayback
            // 
            this.trkPlayback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trkPlayback.Location = new System.Drawing.Point(0, 0);
            this.trkPlayback.Maximum = 0;
            this.trkPlayback.Name = "trkPlayback";
            this.trkPlayback.Size = new System.Drawing.Size(784, 45);
            this.trkPlayback.TabIndex = 0;
            this.trkPlayback.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trkPlayback.Scroll += new System.EventHandler(this.trkPlayback_Scroll);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Visible = false;
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imageSequenceToolStripMenuItem,
            this.videoToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportToolStripMenuItem.Text = "E&xport";
            // 
            // imageSequenceToolStripMenuItem
            // 
            this.imageSequenceToolStripMenuItem.Name = "imageSequenceToolStripMenuItem";
            this.imageSequenceToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.imageSequenceToolStripMenuItem.Text = "&Image Sequence";
            this.imageSequenceToolStripMenuItem.Click += new System.EventHandler(this.imageSequenceToolStripMenuItem_Click);
            // 
            // videoToolStripMenuItem
            // 
            this.videoToolStripMenuItem.Name = "videoToolStripMenuItem";
            this.videoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.videoToolStripMenuItem.Text = "&Video";
            this.videoToolStripMenuItem.Click += new System.EventHandler(this.videoToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.setViewportToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            this.viewToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // tmrOptimizerUpdateFrame
            // 
            this.tmrOptimizerUpdateFrame.Interval = 10;
            this.tmrOptimizerUpdateFrame.Tick += new System.EventHandler(this.tmrOptimizerUpdateFrame_Tick);
            // 
            // x480ToolStripMenuItem
            // 
            this.x480ToolStripMenuItem.Name = "x480ToolStripMenuItem";
            this.x480ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.x480ToolStripMenuItem.Text = "640x480";
            this.x480ToolStripMenuItem.Click += new System.EventHandler(this.videoToolStripMenuItem_Click);
            // 
            // x600ToolStripMenuItem
            // 
            this.x600ToolStripMenuItem.Name = "x600ToolStripMenuItem";
            this.x600ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.x600ToolStripMenuItem.Text = "800x600";
            this.x600ToolStripMenuItem.Click += new System.EventHandler(this.x600ToolStripMenuItem_Click);
            // 
            // x720ToolStripMenuItem
            // 
            this.x720ToolStripMenuItem.Name = "x720ToolStripMenuItem";
            this.x720ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.x720ToolStripMenuItem.Text = "1280x720";
            this.x720ToolStripMenuItem.Click += new System.EventHandler(this.x720ToolStripMenuItem_Click);
            // 
            // x1080ToolStripMenuItem
            // 
            this.x1080ToolStripMenuItem.Name = "x1080ToolStripMenuItem";
            this.x1080ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.x1080ToolStripMenuItem.Text = "1920x1080";
            this.x1080ToolStripMenuItem.Click += new System.EventHandler(this.x1080ToolStripMenuItem_Click);
            // 
            // setViewportToolStripMenuItem
            // 
            this.setViewportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x480ToolStripMenuItem,
            this.x600ToolStripMenuItem,
            this.x720ToolStripMenuItem,
            this.x1080ToolStripMenuItem});
            this.setViewportToolStripMenuItem.Name = "setViewportToolStripMenuItem";
            this.setViewportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.setViewportToolStripMenuItem.Text = "Set Viewport";
            // 
            // lblViewportSize
            // 
            this.lblViewportSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblViewportSize.Location = new System.Drawing.Point(554, 48);
            this.lblViewportSize.Name = "lblViewportSize";
            this.lblViewportSize.Size = new System.Drawing.Size(230, 27);
            this.lblViewportSize.TabIndex = 3;
            this.lblViewportSize.Text = "000x000";
            this.lblViewportSize.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // FormPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.panelControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormPlot";
            this.Text = "FormPlot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPlot_FormClosing);
            this.Load += new System.EventHandler(this.FormPlot_Load);
            this.Resize += new System.EventHandler(this.FormPlot_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkPlayback)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrFrame;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Timer tmrDelayClose;
        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.TrackBar trkPlayback;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.Timer tmrOptimizerUpdateFrame;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imageSequenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoToolStripMenuItem;
        private System.Windows.Forms.Label lblViewportSize;
        private System.Windows.Forms.ToolStripMenuItem setViewportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x480ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x600ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x720ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x1080ToolStripMenuItem;
    }
}
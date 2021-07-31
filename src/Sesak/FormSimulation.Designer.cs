namespace Sesak
{
    partial class FormSimulation
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
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDiscomfort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProcessTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSimulationTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTickCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAgentCounter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tmrUpdateStats = new System.Windows.Forms.Timer(this.components);
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExportVideo = new System.Windows.Forms.Button();
            this.btnExportImageSequence = new System.Windows.Forms.Button();
            this.chkHeatMap = new System.Windows.Forms.CheckBox();
            this.txtEvacuationTime = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCanvas
            // 
            this.pbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCanvas.Location = new System.Drawing.Point(0, 0);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(254, 412);
            this.pbCanvas.TabIndex = 0;
            this.pbCanvas.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEvacuationTime);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtDiscomfort);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtProcessTime);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSimulationTime);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtStatus);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTickCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAgentCounter);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(232, 222);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statisitics";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(194, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "s";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(194, 136);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "s";
            // 
            // txtDiscomfort
            // 
            this.txtDiscomfort.Location = new System.Drawing.Point(115, 159);
            this.txtDiscomfort.Name = "txtDiscomfort";
            this.txtDiscomfort.ReadOnly = true;
            this.txtDiscomfort.Size = new System.Drawing.Size(78, 20);
            this.txtDiscomfort.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Discomfort";
            // 
            // txtProcessTime
            // 
            this.txtProcessTime.Location = new System.Drawing.Point(115, 133);
            this.txtProcessTime.Name = "txtProcessTime";
            this.txtProcessTime.ReadOnly = true;
            this.txtProcessTime.Size = new System.Drawing.Size(78, 20);
            this.txtProcessTime.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Process Time";
            // 
            // txtSimulationTime
            // 
            this.txtSimulationTime.Location = new System.Drawing.Point(115, 107);
            this.txtSimulationTime.Name = "txtSimulationTime";
            this.txtSimulationTime.ReadOnly = true;
            this.txtSimulationTime.Size = new System.Drawing.Size(78, 20);
            this.txtSimulationTime.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Simulation Time";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(115, 29);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(78, 20);
            this.txtStatus.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Status";
            // 
            // txtTickCount
            // 
            this.txtTickCount.Location = new System.Drawing.Point(115, 81);
            this.txtTickCount.Name = "txtTickCount";
            this.txtTickCount.ReadOnly = true;
            this.txtTickCount.Size = new System.Drawing.Size(78, 20);
            this.txtTickCount.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tick Count";
            // 
            // txtAgentCounter
            // 
            this.txtAgentCounter.Location = new System.Drawing.Point(115, 55);
            this.txtAgentCounter.Name = "txtAgentCounter";
            this.txtAgentCounter.ReadOnly = true;
            this.txtAgentCounter.Size = new System.Drawing.Size(78, 20);
            this.txtAgentCounter.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Agents";
            // 
            // tmrUpdateStats
            // 
            this.tmrUpdateStats.Interval = 50;
            this.tmrUpdateStats.Tick += new System.EventHandler(this.tmrUpdateStats_Tick);
            // 
            // btnExport
            // 
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(148, 240);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(94, 29);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export Data";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 240);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 29);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExportVideo);
            this.groupBox2.Controls.Add(this.btnExportImageSequence);
            this.groupBox2.Controls.Add(this.chkHeatMap);
            this.groupBox2.Location = new System.Drawing.Point(12, 275);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 127);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Plotting";
            // 
            // btnExportVideo
            // 
            this.btnExportVideo.Enabled = false;
            this.btnExportVideo.Location = new System.Drawing.Point(31, 86);
            this.btnExportVideo.Name = "btnExportVideo";
            this.btnExportVideo.Size = new System.Drawing.Size(162, 29);
            this.btnExportVideo.TabIndex = 7;
            this.btnExportVideo.Text = "Export Video";
            this.btnExportVideo.UseVisualStyleBackColor = true;
            this.btnExportVideo.Click += new System.EventHandler(this.btnExportVideo_Click);
            // 
            // btnExportImageSequence
            // 
            this.btnExportImageSequence.Enabled = false;
            this.btnExportImageSequence.Location = new System.Drawing.Point(31, 51);
            this.btnExportImageSequence.Name = "btnExportImageSequence";
            this.btnExportImageSequence.Size = new System.Drawing.Size(162, 29);
            this.btnExportImageSequence.TabIndex = 6;
            this.btnExportImageSequence.Text = "Export Image Sequence";
            this.btnExportImageSequence.UseVisualStyleBackColor = true;
            this.btnExportImageSequence.Click += new System.EventHandler(this.btnExportImageSequence_Click);
            // 
            // chkHeatMap
            // 
            this.chkHeatMap.AutoSize = true;
            this.chkHeatMap.Location = new System.Drawing.Point(31, 28);
            this.chkHeatMap.Name = "chkHeatMap";
            this.chkHeatMap.Size = new System.Drawing.Size(73, 17);
            this.chkHeatMap.TabIndex = 5;
            this.chkHeatMap.Text = "Heat Map";
            this.chkHeatMap.UseVisualStyleBackColor = true;
            this.chkHeatMap.CheckedChanged += new System.EventHandler(this.chkHeatMap_CheckedChanged);
            // 
            // txtEvacuationTime
            // 
            this.txtEvacuationTime.Location = new System.Drawing.Point(115, 185);
            this.txtEvacuationTime.Name = "txtEvacuationTime";
            this.txtEvacuationTime.ReadOnly = true;
            this.txtEvacuationTime.Size = new System.Drawing.Size(78, 20);
            this.txtEvacuationTime.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(28, 188);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Evacuation Time";
            // 
            // FormSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 412);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pbCanvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSimulation";
            this.Text = "Simulation";
            this.Load += new System.EventHandler(this.FormSimulation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTickCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAgentCounter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrUpdateStats;
        private System.Windows.Forms.TextBox txtProcessTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSimulationTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtDiscomfort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkHeatMap;
        private System.Windows.Forms.Button btnExportImageSequence;
        private System.Windows.Forms.Button btnExportVideo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEvacuationTime;
        private System.Windows.Forms.Label label9;
    }
}
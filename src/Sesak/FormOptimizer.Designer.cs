namespace Sesak
{
    partial class FormOptimizer
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
            this.tmrSimUpdate = new System.Windows.Forms.Timer(this.components);
            this.btnRunOptimizer = new System.Windows.Forms.Button();
            this.chkShowSimPlot = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtYParameterCount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.chkMultiObjective = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.OnlookerInput = new System.Windows.Forms.TextBox();
            this.cmbWorkerThread = new System.Windows.Forms.ComboBox();
            this.ColonySizeInput = new System.Windows.Forms.TextBox();
            this.MaxIterationInput = new System.Windows.Forms.TextBox();
            this.nPop = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LimitInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEnvironment = new System.Windows.Forms.TextBox();
            this.btnBrowseEnvironment = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSaveEnvironment = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkEnableOptimizer = new System.Windows.Forms.CheckBox();
            this.cmbYIndex = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lstStaticDoor = new System.Windows.Forms.ListBox();
            this.txtDoorCount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAgentCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.simulationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSimulationParameters = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmrSimUpdate
            // 
            this.tmrSimUpdate.Enabled = true;
            // 
            // btnRunOptimizer
            // 
            this.btnRunOptimizer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunOptimizer.Location = new System.Drawing.Point(132, 610);
            this.btnRunOptimizer.Name = "btnRunOptimizer";
            this.btnRunOptimizer.Size = new System.Drawing.Size(110, 32);
            this.btnRunOptimizer.TabIndex = 10;
            this.btnRunOptimizer.Text = "Run Optimizer";
            this.btnRunOptimizer.UseVisualStyleBackColor = true;
            this.btnRunOptimizer.Click += new System.EventHandler(this.btnRunOptimizer_Click);
            // 
            // chkShowSimPlot
            // 
            this.chkShowSimPlot.AutoSize = true;
            this.chkShowSimPlot.Checked = true;
            this.chkShowSimPlot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowSimPlot.Location = new System.Drawing.Point(8, 49);
            this.chkShowSimPlot.Name = "chkShowSimPlot";
            this.chkShowSimPlot.Size = new System.Drawing.Size(94, 17);
            this.chkShowSimPlot.TabIndex = 11;
            this.chkShowSimPlot.Text = "Show Sim Plot";
            this.chkShowSimPlot.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.txtYParameterCount);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.chkMultiObjective);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.OnlookerInput);
            this.groupBox5.Controls.Add(this.cmbWorkerThread);
            this.groupBox5.Controls.Add(this.chkShowSimPlot);
            this.groupBox5.Controls.Add(this.ColonySizeInput);
            this.groupBox5.Controls.Add(this.MaxIterationInput);
            this.groupBox5.Controls.Add(this.nPop);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.LimitInput);
            this.groupBox5.Location = new System.Drawing.Point(12, 50);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(230, 204);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Optimizer Settings";
            // 
            // txtYParameterCount
            // 
            this.txtYParameterCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtYParameterCount.Location = new System.Drawing.Point(87, 167);
            this.txtYParameterCount.Margin = new System.Windows.Forms.Padding(2);
            this.txtYParameterCount.Name = "txtYParameterCount";
            this.txtYParameterCount.Size = new System.Drawing.Size(97, 20);
            this.txtYParameterCount.TabIndex = 45;
            this.txtYParameterCount.Text = "2";
            this.txtYParameterCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtYParameterCount.Validated += new System.EventHandler(this.txtYParameterCount_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 170);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 46;
            this.label4.Text = "Y ParamCount";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 146);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 44;
            this.label10.Text = "nOnlooker";
            // 
            // chkMultiObjective
            // 
            this.chkMultiObjective.AutoSize = true;
            this.chkMultiObjective.Checked = true;
            this.chkMultiObjective.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMultiObjective.Location = new System.Drawing.Point(108, 49);
            this.chkMultiObjective.Name = "chkMultiObjective";
            this.chkMultiObjective.Size = new System.Drawing.Size(96, 17);
            this.chkMultiObjective.TabIndex = 36;
            this.chkMultiObjective.Text = "Multi Objective";
            this.chkMultiObjective.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Worker Count";
            // 
            // OnlookerInput
            // 
            this.OnlookerInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OnlookerInput.Location = new System.Drawing.Point(87, 143);
            this.OnlookerInput.Margin = new System.Windows.Forms.Padding(2);
            this.OnlookerInput.Name = "OnlookerInput";
            this.OnlookerInput.Size = new System.Drawing.Size(97, 20);
            this.OnlookerInput.TabIndex = 37;
            this.OnlookerInput.Text = "50";
            this.OnlookerInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.OnlookerInput.Validated += new System.EventHandler(this.OnlookerInput_Validated);
            // 
            // cmbWorkerThread
            // 
            this.cmbWorkerThread.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkerThread.FormattingEnabled = true;
            this.cmbWorkerThread.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"});
            this.cmbWorkerThread.Location = new System.Drawing.Point(87, 22);
            this.cmbWorkerThread.Name = "cmbWorkerThread";
            this.cmbWorkerThread.Size = new System.Drawing.Size(55, 21);
            this.cmbWorkerThread.TabIndex = 12;
            // 
            // ColonySizeInput
            // 
            this.ColonySizeInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ColonySizeInput.Location = new System.Drawing.Point(87, 119);
            this.ColonySizeInput.Margin = new System.Windows.Forms.Padding(2);
            this.ColonySizeInput.Name = "ColonySizeInput";
            this.ColonySizeInput.Size = new System.Drawing.Size(97, 20);
            this.ColonySizeInput.TabIndex = 38;
            this.ColonySizeInput.Text = "50";
            this.ColonySizeInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ColonySizeInput.Validated += new System.EventHandler(this.ColonySizeInput_Validated);
            // 
            // MaxIterationInput
            // 
            this.MaxIterationInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxIterationInput.Location = new System.Drawing.Point(87, 71);
            this.MaxIterationInput.Margin = new System.Windows.Forms.Padding(2);
            this.MaxIterationInput.Name = "MaxIterationInput";
            this.MaxIterationInput.Size = new System.Drawing.Size(97, 20);
            this.MaxIterationInput.TabIndex = 39;
            this.MaxIterationInput.Text = "300";
            this.MaxIterationInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MaxIterationInput.Validated += new System.EventHandler(this.MaxIterationInput_Validated);
            // 
            // nPop
            // 
            this.nPop.AutoSize = true;
            this.nPop.Location = new System.Drawing.Point(47, 123);
            this.nPop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nPop.Name = "nPop";
            this.nPop.Size = new System.Drawing.Size(32, 13);
            this.nPop.TabIndex = 43;
            this.nPop.Text = "nPop";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 76);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 40;
            this.label8.Text = "Max.It";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(51, 98);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 41;
            this.label9.Text = "Limit";
            // 
            // LimitInput
            // 
            this.LimitInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LimitInput.Location = new System.Drawing.Point(87, 95);
            this.LimitInput.Margin = new System.Windows.Forms.Padding(2);
            this.LimitInput.Name = "LimitInput";
            this.LimitInput.Size = new System.Drawing.Size(97, 20);
            this.LimitInput.TabIndex = 42;
            this.LimitInput.Text = "500";
            this.LimitInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.LimitInput.Validated += new System.EventHandler(this.LimitInput_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Path";
            // 
            // txtEnvironment
            // 
            this.txtEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEnvironment.Location = new System.Drawing.Point(80, 19);
            this.txtEnvironment.Name = "txtEnvironment";
            this.txtEnvironment.ReadOnly = true;
            this.txtEnvironment.Size = new System.Drawing.Size(104, 20);
            this.txtEnvironment.TabIndex = 38;
            this.txtEnvironment.Text = "<Not Loaded>";
            // 
            // btnBrowseEnvironment
            // 
            this.btnBrowseEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseEnvironment.Location = new System.Drawing.Point(190, 17);
            this.btnBrowseEnvironment.Name = "btnBrowseEnvironment";
            this.btnBrowseEnvironment.Size = new System.Drawing.Size(34, 23);
            this.btnBrowseEnvironment.TabIndex = 39;
            this.btnBrowseEnvironment.Text = "...";
            this.btnBrowseEnvironment.UseVisualStyleBackColor = true;
            this.btnBrowseEnvironment.Click += new System.EventHandler(this.btnBrowseEnvironment_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSaveEnvironment);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtDoorCount);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAgentCount);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnBrowseEnvironment);
            this.groupBox1.Controls.Add(this.txtEnvironment);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 260);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 344);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Environment";
            // 
            // btnSaveEnvironment
            // 
            this.btnSaveEnvironment.Location = new System.Drawing.Point(11, 315);
            this.btnSaveEnvironment.Name = "btnSaveEnvironment";
            this.btnSaveEnvironment.Size = new System.Drawing.Size(115, 23);
            this.btnSaveEnvironment.TabIndex = 41;
            this.btnSaveEnvironment.Text = "Save Environment";
            this.btnSaveEnvironment.UseVisualStyleBackColor = true;
            this.btnSaveEnvironment.Click += new System.EventHandler(this.btnSaveEnvironment_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkEnableOptimizer);
            this.groupBox2.Controls.Add(this.cmbYIndex);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lstStaticDoor);
            this.groupBox2.Location = new System.Drawing.Point(11, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 212);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Door Optimization";
            // 
            // chkEnableOptimizer
            // 
            this.chkEnableOptimizer.AutoSize = true;
            this.chkEnableOptimizer.Location = new System.Drawing.Point(69, 149);
            this.chkEnableOptimizer.Name = "chkEnableOptimizer";
            this.chkEnableOptimizer.Size = new System.Drawing.Size(66, 17);
            this.chkEnableOptimizer.TabIndex = 47;
            this.chkEnableOptimizer.Text = "Optimize";
            this.chkEnableOptimizer.UseVisualStyleBackColor = true;
            this.chkEnableOptimizer.CheckedChanged += new System.EventHandler(this.chkEnableOptimizer_CheckedChanged);
            // 
            // cmbYIndex
            // 
            this.cmbYIndex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbYIndex.Enabled = false;
            this.cmbYIndex.FormattingEnabled = true;
            this.cmbYIndex.Location = new System.Drawing.Point(69, 172);
            this.cmbYIndex.Name = "cmbYIndex";
            this.cmbYIndex.Size = new System.Drawing.Size(104, 21);
            this.cmbYIndex.TabIndex = 46;
            this.cmbYIndex.SelectedIndexChanged += new System.EventHandler(this.cmbYIndex_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 45;
            this.label5.Text = "Y Index";
            // 
            // lstStaticDoor
            // 
            this.lstStaticDoor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstStaticDoor.FormattingEnabled = true;
            this.lstStaticDoor.Location = new System.Drawing.Point(8, 19);
            this.lstStaticDoor.Name = "lstStaticDoor";
            this.lstStaticDoor.Size = new System.Drawing.Size(199, 121);
            this.lstStaticDoor.TabIndex = 44;
            this.lstStaticDoor.SelectedIndexChanged += new System.EventHandler(this.lstStaticDoor_SelectedIndexChanged);
            // 
            // txtDoorCount
            // 
            this.txtDoorCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDoorCount.Location = new System.Drawing.Point(80, 71);
            this.txtDoorCount.Name = "txtDoorCount";
            this.txtDoorCount.ReadOnly = true;
            this.txtDoorCount.Size = new System.Drawing.Size(104, 20);
            this.txtDoorCount.TabIndex = 43;
            this.txtDoorCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Door Count";
            // 
            // txtAgentCount
            // 
            this.txtAgentCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAgentCount.Location = new System.Drawing.Point(80, 45);
            this.txtAgentCount.Name = "txtAgentCount";
            this.txtAgentCount.ReadOnly = true;
            this.txtAgentCount.Size = new System.Drawing.Size(104, 20);
            this.txtAgentCount.TabIndex = 41;
            this.txtAgentCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Agent Count";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.simulationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(254, 24);
            this.menuStrip1.TabIndex = 41;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // simulationToolStripMenuItem
            // 
            this.simulationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametersToolStripMenuItem});
            this.simulationToolStripMenuItem.Name = "simulationToolStripMenuItem";
            this.simulationToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.simulationToolStripMenuItem.Text = "&Simulation";
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.parametersToolStripMenuItem.Text = "&Parameters";
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.parametersToolStripMenuItem_Click);
            // 
            // btnSimulationParameters
            // 
            this.btnSimulationParameters.Location = new System.Drawing.Point(12, 12);
            this.btnSimulationParameters.Name = "btnSimulationParameters";
            this.btnSimulationParameters.Size = new System.Drawing.Size(146, 32);
            this.btnSimulationParameters.TabIndex = 42;
            this.btnSimulationParameters.Text = "Simulation Parameters";
            this.btnSimulationParameters.UseVisualStyleBackColor = true;
            this.btnSimulationParameters.Click += new System.EventHandler(this.btnSimulationParameters_Click);
            // 
            // FormOptimizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 655);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.btnSimulationParameters);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.btnRunOptimizer);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptimizer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Optimizer";
            this.Load += new System.EventHandler(this.FormOptimizer_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrSimUpdate;
        private System.Windows.Forms.Button btnRunOptimizer;
        private System.Windows.Forms.CheckBox chkShowSimPlot;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmbWorkerThread;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkMultiObjective;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEnvironment;
        private System.Windows.Forms.Button btnBrowseEnvironment;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDoorCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAgentCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstStaticDoor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox OnlookerInput;
        private System.Windows.Forms.TextBox ColonySizeInput;
        private System.Windows.Forms.TextBox MaxIterationInput;
        private System.Windows.Forms.Label nPop;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox LimitInput;
        private System.Windows.Forms.TextBox txtYParameterCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbYIndex;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkEnableOptimizer;
        private System.Windows.Forms.Button btnSaveEnvironment;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem simulationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.Button btnSimulationParameters;
    }
}
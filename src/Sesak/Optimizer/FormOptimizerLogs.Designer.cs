namespace Sesak.Optimizer
{
    partial class FormOptimizerLogs
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
            this.txtLogs = new System.Windows.Forms.TextBox();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.btnShowPlot = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.tmrLogUpdater = new System.Windows.Forms.Timer(this.components);
            this.simProgress = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.tmrUpdateTime = new System.Windows.Forms.Timer(this.components);
            this.MaxIterationInput = new System.Windows.Forms.TextBox();
            this.btnSaveEnvironment = new System.Windows.Forms.Button();
            this.btnExportData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLogs
            // 
            this.txtLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.txtLogs.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLogs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.txtLogs.Location = new System.Drawing.Point(12, 56);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLogs.Size = new System.Drawing.Size(972, 140);
            this.txtLogs.TabIndex = 0;
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            // 
            // btnShowPlot
            // 
            this.btnShowPlot.Location = new System.Drawing.Point(12, 12);
            this.btnShowPlot.Name = "btnShowPlot";
            this.btnShowPlot.Size = new System.Drawing.Size(91, 38);
            this.btnShowPlot.TabIndex = 1;
            this.btnShowPlot.Text = "Show Plot";
            this.btnShowPlot.UseVisualStyleBackColor = true;
            this.btnShowPlot.Click += new System.EventHandler(this.btnShowPlot_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(109, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(91, 38);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tmrLogUpdater
            // 
            this.tmrLogUpdater.Interval = 200;
            this.tmrLogUpdater.Tick += new System.EventHandler(this.tmrLogUpdater_Tick);
            // 
            // simProgress
            // 
            this.simProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.simProgress.Location = new System.Drawing.Point(431, 12);
            this.simProgress.Name = "simProgress";
            this.simProgress.Size = new System.Drawing.Size(541, 23);
            this.simProgress.TabIndex = 3;
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Location = new System.Drawing.Point(431, 38);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(541, 15);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.Text = "0/0";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrUpdateTime
            // 
            this.tmrUpdateTime.Enabled = true;
            this.tmrUpdateTime.Interval = 500;
            this.tmrUpdateTime.Tick += new System.EventHandler(this.tmrUpdateTime_Tick);
            // 
            // MaxIterationInput
            // 
            this.MaxIterationInput.Location = new System.Drawing.Point(103, 157);
            this.MaxIterationInput.Margin = new System.Windows.Forms.Padding(2);
            this.MaxIterationInput.Name = "MaxIterationInput";
            this.MaxIterationInput.Size = new System.Drawing.Size(87, 20);
            this.MaxIterationInput.TabIndex = 18;
            this.MaxIterationInput.Text = "300";
            // 
            // btnSaveEnvironment
            // 
            this.btnSaveEnvironment.Location = new System.Drawing.Point(206, 12);
            this.btnSaveEnvironment.Name = "btnSaveEnvironment";
            this.btnSaveEnvironment.Size = new System.Drawing.Size(115, 38);
            this.btnSaveEnvironment.TabIndex = 5;
            this.btnSaveEnvironment.Text = "View Itteration Results";
            this.btnSaveEnvironment.UseVisualStyleBackColor = true;
            this.btnSaveEnvironment.Click += new System.EventHandler(this.btnSaveEnvironment_Click);
            // 
            // btnExportData
            // 
            this.btnExportData.Location = new System.Drawing.Point(327, 12);
            this.btnExportData.Name = "btnExportData";
            this.btnExportData.Size = new System.Drawing.Size(98, 38);
            this.btnExportData.TabIndex = 6;
            this.btnExportData.Text = "Export Data";
            this.btnExportData.UseVisualStyleBackColor = true;
            this.btnExportData.Click += new System.EventHandler(this.btnExportData_Click);
            // 
            // FormOptimizerLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 196);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.btnExportData);
            this.Controls.Add(this.btnSaveEnvironment);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.simProgress);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnShowPlot);
            this.Controls.Add(this.txtLogs);
            this.Name = "FormOptimizerLogs";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Optimizer";
            this.Load += new System.EventHandler(this.FormOptimizerLogs_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogs;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Button btnShowPlot;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer tmrLogUpdater;
        private System.Windows.Forms.ProgressBar simProgress;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.Timer tmrUpdateTime;
        private System.Windows.Forms.TextBox MaxIterationInput;
        private System.Windows.Forms.Button btnSaveEnvironment;
        private System.Windows.Forms.Button btnExportData;
    }
}
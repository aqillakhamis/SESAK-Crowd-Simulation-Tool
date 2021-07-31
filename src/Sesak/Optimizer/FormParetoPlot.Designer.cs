namespace Sesak.Optimizer
{
    partial class FormParetoPlot
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ParetoChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.chkHideDominated = new System.Windows.Forms.CheckBox();
            this.btnExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ParetoChart)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ParetoChart
            // 
            chartArea1.AxisX.Title = "Time (s)";
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.Title = "Comfort";
            chartArea1.Name = "ChartArea1";
            this.ParetoChart.ChartAreas.Add(chartArea1);
            this.ParetoChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.ParetoChart.Legends.Add(legend1);
            this.ParetoChart.Location = new System.Drawing.Point(0, 28);
            this.ParetoChart.Name = "ParetoChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Legend = "Legend1";
            series1.MarkerSize = 6;
            series1.Name = "Dominated";
            series2.BorderColor = System.Drawing.Color.White;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Color = System.Drawing.Color.Red;
            series2.Legend = "Legend1";
            series2.MarkerSize = 7;
            series2.Name = "Non Dominated";
            this.ParetoChart.Series.Add(series1);
            this.ParetoChart.Series.Add(series2);
            this.ParetoChart.Size = new System.Drawing.Size(586, 412);
            this.ParetoChart.TabIndex = 0;
            this.ParetoChart.Text = "chart1";
            this.ParetoChart.Click += new System.EventHandler(this.ParetoChart_Click);
            this.ParetoChart.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ParetoChart_MouseDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.chkHideDominated);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 28);
            this.panel1.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(76, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(109, 22);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh Graph";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // chkHideDominated
            // 
            this.chkHideDominated.AutoSize = true;
            this.chkHideDominated.Location = new System.Drawing.Point(191, 7);
            this.chkHideDominated.Name = "chkHideDominated";
            this.chkHideDominated.Size = new System.Drawing.Size(128, 17);
            this.chkHideDominated.TabIndex = 2;
            this.chkHideDominated.Text = "Hide Dominated Data";
            this.chkHideDominated.UseVisualStyleBackColor = true;
            this.chkHideDominated.CheckedChanged += new System.EventHandler(this.chkHideDominated_CheckedChanged);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(3, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(67, 22);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export Raw Result";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormParetoPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 440);
            this.Controls.Add(this.ParetoChart);
            this.Controls.Add(this.panel1);
            this.Name = "FormParetoPlot";
            this.Text = "FormParetoPlot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormParetoPlot_FormClosing);
            this.Load += new System.EventHandler(this.FormParetoPlot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ParetoChart)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ParetoChart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.CheckBox chkHideDominated;
        private System.Windows.Forms.Button btnRefresh;
    }
}
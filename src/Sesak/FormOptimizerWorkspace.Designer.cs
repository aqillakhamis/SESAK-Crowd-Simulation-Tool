namespace Sesak
{
    partial class FormOptimizerWorkspace
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
            this.dockOptimizer = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.SuspendLayout();
            // 
            // dockOptimizer
            // 
            this.dockOptimizer.BackColor = System.Drawing.SystemColors.ControlDark;
            this.dockOptimizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockOptimizer.DockLeftPortion = 270D;
            this.dockOptimizer.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockOptimizer.Location = new System.Drawing.Point(0, 0);
            this.dockOptimizer.Name = "dockOptimizer";
            this.dockOptimizer.Size = new System.Drawing.Size(800, 450);
            this.dockOptimizer.TabIndex = 1;
            this.dockOptimizer.ActiveContentChanged += new System.EventHandler(this.dockOptimizer_ActiveContentChanged);
            // 
            // FormOptimizerWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dockOptimizer);
            this.Name = "FormOptimizerWorkspace";
            this.Text = "Optimizer";
            this.Load += new System.EventHandler(this.FormOptimizerWorkspace_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel dockOptimizer;
    }
}
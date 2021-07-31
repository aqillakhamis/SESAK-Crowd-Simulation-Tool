namespace Sesak
{
    partial class FormEditorWorkspace
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
            this.dockEditorMode = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.dockSimulationMode = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.SuspendLayout();
            // 
            // dockEditorMode
            // 
            this.dockEditorMode.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
            this.dockEditorMode.Location = new System.Drawing.Point(35, 67);
            this.dockEditorMode.Name = "dockEditorMode";
            this.dockEditorMode.Size = new System.Drawing.Size(656, 450);
            this.dockEditorMode.TabIndex = 0;
            // 
            // dockSimulationMode
            // 
            this.dockSimulationMode.DockLeftPortion = 270D;
            this.dockSimulationMode.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
            this.dockSimulationMode.Location = new System.Drawing.Point(261, 387);
            this.dockSimulationMode.Name = "dockSimulationMode";
            this.dockSimulationMode.Size = new System.Drawing.Size(601, 307);
            this.dockSimulationMode.TabIndex = 3;
            // 
            // FormEditorWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1085, 835);
            this.Controls.Add(this.dockSimulationMode);
            this.Controls.Add(this.dockEditorMode);
            this.Name = "FormEditorWorkspace";
            this.Text = "Simulation";
            this.Load += new System.EventHandler(this.FormEditorWorkspace_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel dockEditorMode;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockSimulationMode;
    }
}
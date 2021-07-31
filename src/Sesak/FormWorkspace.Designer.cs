namespace Sesak
{
    partial class FormWorkspace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormWorkspace));
            this.mainDock = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dockOptimizerWorkspace = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainDock
            // 
            this.mainDock.BackColor = System.Drawing.SystemColors.Control;
            this.mainDock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDock.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
            this.mainDock.Location = new System.Drawing.Point(3, 3);
            this.mainDock.Name = "mainDock";
            this.mainDock.Size = new System.Drawing.Size(1031, 661);
            this.mainDock.TabIndex = 1;
            this.mainDock.ActiveContentChanged += new System.EventHandler(this.mainDock_ActiveContentChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1045, 693);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mainDock);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1037, 667);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Simulation";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dockOptimizerWorkspace);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1037, 667);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Optimizer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dockOptimizerWorkspace
            // 
            this.dockOptimizerWorkspace.BackColor = System.Drawing.SystemColors.Control;
            this.dockOptimizerWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockOptimizerWorkspace.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
            this.dockOptimizerWorkspace.Location = new System.Drawing.Point(3, 3);
            this.dockOptimizerWorkspace.Name = "dockOptimizerWorkspace";
            this.dockOptimizerWorkspace.Size = new System.Drawing.Size(1031, 661);
            this.dockOptimizerWorkspace.TabIndex = 2;
            // 
            // FormWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1045, 693);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormWorkspace";
            this.Text = "Sesak";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormWorkspace_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private WeifenLuo.WinFormsUI.Docking.DockPanel mainDock;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockOptimizerWorkspace;
    }
}
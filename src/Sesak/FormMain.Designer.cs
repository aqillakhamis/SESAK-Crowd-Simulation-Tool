namespace Sesak
{
    partial class FormMain
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
            this.btnFormEditor = new System.Windows.Forms.Button();
            this.btnOptimizer = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFormEditor
            // 
            this.btnFormEditor.Location = new System.Drawing.Point(67, 15);
            this.btnFormEditor.Name = "btnFormEditor";
            this.btnFormEditor.Size = new System.Drawing.Size(129, 41);
            this.btnFormEditor.TabIndex = 0;
            this.btnFormEditor.Text = "&Environment Editor";
            this.btnFormEditor.UseVisualStyleBackColor = true;
            this.btnFormEditor.Click += new System.EventHandler(this.btnFormEditor_Click);
            // 
            // btnOptimizer
            // 
            this.btnOptimizer.Location = new System.Drawing.Point(67, 62);
            this.btnOptimizer.Name = "btnOptimizer";
            this.btnOptimizer.Size = new System.Drawing.Size(129, 41);
            this.btnOptimizer.TabIndex = 2;
            this.btnOptimizer.Text = "&Optimizer";
            this.btnOptimizer.UseVisualStyleBackColor = true;
            this.btnOptimizer.Click += new System.EventHandler(this.btnOptimizer_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(67, 109);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(129, 41);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 171);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnOptimizer);
            this.Controls.Add(this.btnFormEditor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SESAK V2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFormEditor;
        private System.Windows.Forms.Button btnOptimizer;
        private System.Windows.Forms.Button btnExit;
    }
}


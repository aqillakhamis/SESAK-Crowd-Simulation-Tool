namespace Sesak.SimulationObjects.PropertiesControl
{
    partial class ucWallProp
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtY2 = new System.Windows.Forms.TextBox();
            this.txtX2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtY2);
            this.groupBox1.Controls.Add(this.txtX2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtY1);
            this.groupBox1.Controls.Add(this.txtX1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 394);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wall";
            // 
            // txtY2
            // 
            this.txtY2.Location = new System.Drawing.Point(125, 56);
            this.txtY2.Name = "txtY2";
            this.txtY2.Size = new System.Drawing.Size(45, 20);
            this.txtY2.TabIndex = 5;
            this.txtY2.Text = "0";
            this.txtY2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtY2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtY2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtY2_KeyPress);
            this.txtY2.Validated += new System.EventHandler(this.txtY2_Validated);
            // 
            // txtX2
            // 
            this.txtX2.Location = new System.Drawing.Point(56, 56);
            this.txtX2.Name = "txtX2";
            this.txtX2.Size = new System.Drawing.Size(45, 20);
            this.txtX2.TabIndex = 4;
            this.txtX2.Text = "0";
            this.txtX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtX2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtX2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtX2_KeyPress);
            this.txtX2.Validated += new System.EventHandler(this.txtX2_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Point 2";
            // 
            // txtY1
            // 
            this.txtY1.Location = new System.Drawing.Point(125, 30);
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(45, 20);
            this.txtY1.TabIndex = 2;
            this.txtY1.Text = "0";
            this.txtY1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtY1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtY1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtY1_KeyPress);
            this.txtY1.Validated += new System.EventHandler(this.txtY1_Validated);
            // 
            // txtX1
            // 
            this.txtX1.Location = new System.Drawing.Point(56, 30);
            this.txtX1.Name = "txtX1";
            this.txtX1.Size = new System.Drawing.Size(45, 20);
            this.txtX1.TabIndex = 1;
            this.txtX1.Text = "0";
            this.txtX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtX1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtX1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtX1_KeyPress);
            this.txtX1.Validated += new System.EventHandler(this.txtX1_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Point 1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(103, 33);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "m";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(103, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "m";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "m";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(173, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "m";
            // 
            // ucWallProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucWallProp";
            this.Size = new System.Drawing.Size(200, 400);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtY2;
        private System.Windows.Forms.TextBox txtX2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtY1;
        private System.Windows.Forms.TextBox txtX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
    }
}

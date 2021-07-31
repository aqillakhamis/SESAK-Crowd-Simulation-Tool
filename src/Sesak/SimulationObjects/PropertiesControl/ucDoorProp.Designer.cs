namespace Sesak.SimulationObjects.PropertiesControl
{
    partial class ucDoorProp
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtX1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtY1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtX2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtY2 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtDoorWidth = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDoorOffset = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDoorWidthPercent = new System.Windows.Forms.TextBox();
            this.txtDoorOffsetPercent = new System.Windows.Forms.TextBox();
            this.txtLabel = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtLabel);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 394);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Door";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtX1);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtY1);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtX2);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtY2);
            this.groupBox3.Location = new System.Drawing.Point(6, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 80);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Wall";
            // 
            // txtX1
            // 
            this.txtX1.Location = new System.Drawing.Point(56, 19);
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
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Point 1";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(166, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(15, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "m";
            // 
            // txtY1
            // 
            this.txtY1.Location = new System.Drawing.Point(121, 19);
            this.txtY1.Name = "txtY1";
            this.txtY1.Size = new System.Drawing.Size(45, 20);
            this.txtY1.TabIndex = 2;
            this.txtY1.Text = "0";
            this.txtY1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtY1.TextChanged += new System.EventHandler(this.txtY1_TextChanged);
            this.txtY1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtY1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtY1_KeyPress);
            this.txtY1.Validated += new System.EventHandler(this.txtY1_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(101, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "m";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Point 2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "m";
            // 
            // txtX2
            // 
            this.txtX2.Location = new System.Drawing.Point(56, 45);
            this.txtX2.Name = "txtX2";
            this.txtX2.Size = new System.Drawing.Size(45, 20);
            this.txtX2.TabIndex = 4;
            this.txtX2.Text = "0";
            this.txtX2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtX2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtX2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtX2_KeyPress);
            this.txtX2.Validated += new System.EventHandler(this.txtX2_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(101, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "m";
            // 
            // txtY2
            // 
            this.txtY2.Location = new System.Drawing.Point(121, 45);
            this.txtY2.Name = "txtY2";
            this.txtY2.Size = new System.Drawing.Size(45, 20);
            this.txtY2.TabIndex = 5;
            this.txtY2.Text = "0";
            this.txtY2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtY2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtY2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtY2_KeyPress);
            this.txtY2.Validated += new System.EventHandler(this.txtY2_Validated);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtDoorWidth);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtDoorOffset);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtDoorWidthPercent);
            this.groupBox2.Controls.Add(this.txtDoorOffsetPercent);
            this.groupBox2.Location = new System.Drawing.Point(6, 138);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 81);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Door";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Width";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(166, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(15, 13);
            this.label13.TabIndex = 22;
            this.label13.Text = "%";
            // 
            // txtDoorWidth
            // 
            this.txtDoorWidth.Location = new System.Drawing.Point(56, 20);
            this.txtDoorWidth.Name = "txtDoorWidth";
            this.txtDoorWidth.Size = new System.Drawing.Size(45, 20);
            this.txtDoorWidth.TabIndex = 8;
            this.txtDoorWidth.Text = "0";
            this.txtDoorWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDoorWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtDoorWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDoorWidth_KeyPress);
            this.txtDoorWidth.Validated += new System.EventHandler(this.txtDoorWidth_Validated);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(101, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 13);
            this.label12.TabIndex = 21;
            this.label12.Text = "m";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Offset";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(166, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 13);
            this.label11.TabIndex = 20;
            this.label11.Text = "%";
            // 
            // txtDoorOffset
            // 
            this.txtDoorOffset.Location = new System.Drawing.Point(56, 46);
            this.txtDoorOffset.Name = "txtDoorOffset";
            this.txtDoorOffset.Size = new System.Drawing.Size(45, 20);
            this.txtDoorOffset.TabIndex = 10;
            this.txtDoorOffset.Text = "0";
            this.txtDoorOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDoorOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtDoorOffset.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDoorOffset_KeyPress);
            this.txtDoorOffset.Validated += new System.EventHandler(this.txtDoorOffset_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(101, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "m";
            // 
            // txtDoorWidthPercent
            // 
            this.txtDoorWidthPercent.Location = new System.Drawing.Point(121, 20);
            this.txtDoorWidthPercent.Name = "txtDoorWidthPercent";
            this.txtDoorWidthPercent.Size = new System.Drawing.Size(45, 20);
            this.txtDoorWidthPercent.TabIndex = 11;
            this.txtDoorWidthPercent.Text = "0";
            this.txtDoorWidthPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDoorWidthPercent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtDoorWidthPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDoorWidthPercent_KeyPress);
            this.txtDoorWidthPercent.Validated += new System.EventHandler(this.txtDoorWidthPercent_Validated);
            // 
            // txtDoorOffsetPercent
            // 
            this.txtDoorOffsetPercent.Location = new System.Drawing.Point(121, 46);
            this.txtDoorOffsetPercent.Name = "txtDoorOffsetPercent";
            this.txtDoorOffsetPercent.Size = new System.Drawing.Size(45, 20);
            this.txtDoorOffsetPercent.TabIndex = 12;
            this.txtDoorOffsetPercent.Text = "0";
            this.txtDoorOffsetPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDoorOffsetPercent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxField_KeyDown);
            this.txtDoorOffsetPercent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDoorOffsetPercent_KeyPress);
            this.txtDoorOffsetPercent.Validated += new System.EventHandler(this.txtDoorOffsetPercent_Validated);
            // 
            // txtLabel
            // 
            this.txtLabel.Location = new System.Drawing.Point(56, 26);
            this.txtLabel.Name = "txtLabel";
            this.txtLabel.Size = new System.Drawing.Size(124, 20);
            this.txtLabel.TabIndex = 14;
            this.txtLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLabel.Validated += new System.EventHandler(this.txtLabel_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Label";
            // 
            // ucDoorProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "ucDoorProp";
            this.Size = new System.Drawing.Size(200, 400);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.TextBox txtDoorOffset;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDoorWidth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDoorOffsetPercent;
        private System.Windows.Forms.TextBox txtDoorWidthPercent;
        private System.Windows.Forms.TextBox txtLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

namespace Proj
{
    partial class MainForm
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.minTextBox = new System.Windows.Forms.TextBox();
            this.maxTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.y0TextBox = new System.Windows.Forms.TextBox();
            this.x0TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.radiusTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.y1TextBox = new System.Windows.Forms.TextBox();
            this.x1TextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.y2TextBox = new System.Windows.Forms.TextBox();
            this.x2TextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.bTextBox = new System.Windows.Forms.TextBox();
            this.aTextBox = new System.Windows.Forms.TextBox();
            this.circleButton = new System.Windows.Forms.Button();
            this.ellipseButton = new System.Windows.Forms.Button();
            this.renderControl1 = new Proj.RenderControl();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // minTextBox
            // 
            this.minTextBox.Location = new System.Drawing.Point(37, 31);
            this.minTextBox.Name = "minTextBox";
            this.minTextBox.Size = new System.Drawing.Size(71, 20);
            this.minTextBox.TabIndex = 3;
            this.minTextBox.TextChanged += new System.EventHandler(this.minTextBox_TextChanged);
            // 
            // maxTextBox
            // 
            this.maxTextBox.Location = new System.Drawing.Point(37, 57);
            this.maxTextBox.Name = "maxTextBox";
            this.maxTextBox.Size = new System.Drawing.Size(71, 20);
            this.maxTextBox.TabIndex = 4;
            this.maxTextBox.TextChanged += new System.EventHandler(this.maxTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "min:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "max:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ellipseButton);
            this.groupBox1.Controls.Add(this.circleButton);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.bTextBox);
            this.groupBox1.Controls.Add(this.aTextBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.y2TextBox);
            this.groupBox1.Controls.Add(this.x2TextBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.y1TextBox);
            this.groupBox1.Controls.Add(this.x1TextBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.radiusTextBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.y0TextBox);
            this.groupBox1.Controls.Add(this.x0TextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.maxTextBox);
            this.groupBox1.Controls.Add(this.minTextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 265);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "параметры";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "x0:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "y0:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // y0TextBox
            // 
            this.y0TextBox.Location = new System.Drawing.Point(37, 109);
            this.y0TextBox.Name = "y0TextBox";
            this.y0TextBox.Size = new System.Drawing.Size(71, 20);
            this.y0TextBox.TabIndex = 9;
            this.y0TextBox.TextChanged += new System.EventHandler(this.y0TextBox_TextChanged);
            // 
            // x0TextBox
            // 
            this.x0TextBox.Location = new System.Drawing.Point(37, 83);
            this.x0TextBox.Name = "x0TextBox";
            this.x0TextBox.Size = new System.Drawing.Size(71, 20);
            this.x0TextBox.TabIndex = 8;
            this.x0TextBox.TextChanged += new System.EventHandler(this.x0TextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 227F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.renderControl1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 271);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "r:";
            // 
            // radiusTextBox
            // 
            this.radiusTextBox.Location = new System.Drawing.Point(37, 135);
            this.radiusTextBox.Name = "radiusTextBox";
            this.radiusTextBox.Size = new System.Drawing.Size(71, 20);
            this.radiusTextBox.TabIndex = 12;
            this.radiusTextBox.TextChanged += new System.EventHandler(this.radiusTextBox_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "x1:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 190);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "y1:";
            // 
            // y1TextBox
            // 
            this.y1TextBox.Location = new System.Drawing.Point(37, 187);
            this.y1TextBox.Name = "y1TextBox";
            this.y1TextBox.Size = new System.Drawing.Size(71, 20);
            this.y1TextBox.TabIndex = 15;
            this.y1TextBox.TextChanged += new System.EventHandler(this.y1TextBox_TextChanged);
            // 
            // x1TextBox
            // 
            this.x1TextBox.Location = new System.Drawing.Point(37, 161);
            this.x1TextBox.Name = "x1TextBox";
            this.x1TextBox.Size = new System.Drawing.Size(71, 20);
            this.x1TextBox.TabIndex = 14;
            this.x1TextBox.TextChanged += new System.EventHandler(this.x1TextBox_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 216);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "x2:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 242);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(21, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "y2:";
            // 
            // y2TextBox
            // 
            this.y2TextBox.Location = new System.Drawing.Point(37, 239);
            this.y2TextBox.Name = "y2TextBox";
            this.y2TextBox.Size = new System.Drawing.Size(71, 20);
            this.y2TextBox.TabIndex = 19;
            this.y2TextBox.TextChanged += new System.EventHandler(this.y2TextBox_TextChanged);
            // 
            // x2TextBox
            // 
            this.x2TextBox.Location = new System.Drawing.Point(37, 213);
            this.x2TextBox.Name = "x2TextBox";
            this.x2TextBox.Size = new System.Drawing.Size(71, 20);
            this.x2TextBox.TabIndex = 18;
            this.x2TextBox.TextChanged += new System.EventHandler(this.x2TextBox_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(116, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "a:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(116, 60);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "b:";
            // 
            // bTextBox
            // 
            this.bTextBox.Location = new System.Drawing.Point(145, 57);
            this.bTextBox.Name = "bTextBox";
            this.bTextBox.Size = new System.Drawing.Size(71, 20);
            this.bTextBox.TabIndex = 23;
            this.bTextBox.TextChanged += new System.EventHandler(this.bTextBox_TextChanged);
            // 
            // aTextBox
            // 
            this.aTextBox.Location = new System.Drawing.Point(145, 31);
            this.aTextBox.Name = "aTextBox";
            this.aTextBox.Size = new System.Drawing.Size(71, 20);
            this.aTextBox.TabIndex = 22;
            this.aTextBox.TextChanged += new System.EventHandler(this.aTextBox_TextChanged);
            // 
            // circleButton
            // 
            this.circleButton.Location = new System.Drawing.Point(140, 81);
            this.circleButton.Name = "circleButton";
            this.circleButton.Size = new System.Drawing.Size(75, 23);
            this.circleButton.TabIndex = 26;
            this.circleButton.Text = "окружность";
            this.circleButton.UseVisualStyleBackColor = true;
            this.circleButton.Click += new System.EventHandler(this.circleButton_Click);
            // 
            // ellipseButton
            // 
            this.ellipseButton.Location = new System.Drawing.Point(140, 107);
            this.ellipseButton.Name = "ellipseButton";
            this.ellipseButton.Size = new System.Drawing.Size(75, 23);
            this.ellipseButton.TabIndex = 27;
            this.ellipseButton.Text = "эллипс";
            this.ellipseButton.UseVisualStyleBackColor = true;
            this.ellipseButton.Click += new System.EventHandler(this.ellipseButton_Click);
            // 
            // renderControl1
            // 
            this.renderControl1.BackColor = System.Drawing.Color.White;
            this.renderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderControl1.Location = new System.Drawing.Point(230, 3);
            this.renderControl1.Name = "renderControl1";
            this.renderControl1.Size = new System.Drawing.Size(235, 265);
            this.renderControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 271);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "ИКГ л4 в25 явн. окр. + отр. и парам. эллипс";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

    }










    #endregion

    private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private RenderControl renderControl1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.TextBox minTextBox;
        private System.Windows.Forms.TextBox maxTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox y0TextBox;
        private System.Windows.Forms.TextBox x0TextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox radiusTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox y1TextBox;
        private System.Windows.Forms.TextBox x1TextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox y2TextBox;
        private System.Windows.Forms.TextBox x2TextBox;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox bTextBox;
        private System.Windows.Forms.TextBox aTextBox;
        private System.Windows.Forms.Button ellipseButton;
        private System.Windows.Forms.Button circleButton;
    }
}


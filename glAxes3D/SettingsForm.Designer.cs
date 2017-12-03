namespace glAxes3D
{
    partial class SettingsForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundRadioButton2 = new System.Windows.Forms.RadioButton();
            this.backgroundRadioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nameRadioButton2 = new System.Windows.Forms.RadioButton();
            this.nameRadioButton1 = new System.Windows.Forms.RadioButton();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.backgroundRadioButton2);
            this.groupBox1.Controls.Add(this.backgroundRadioButton1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(75, 73);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "фон";
            // 
            // backgroundRadioButton2
            // 
            this.backgroundRadioButton2.AutoSize = true;
            this.backgroundRadioButton2.Location = new System.Drawing.Point(7, 44);
            this.backgroundRadioButton2.Name = "backgroundRadioButton2";
            this.backgroundRadioButton2.Size = new System.Drawing.Size(57, 17);
            this.backgroundRadioButton2.TabIndex = 1;
            this.backgroundRadioButton2.Text = "белый";
            this.backgroundRadioButton2.UseVisualStyleBackColor = true;
            // 
            // backgroundRadioButton1
            // 
            this.backgroundRadioButton1.AutoSize = true;
            this.backgroundRadioButton1.Checked = true;
            this.backgroundRadioButton1.Location = new System.Drawing.Point(7, 20);
            this.backgroundRadioButton1.Name = "backgroundRadioButton1";
            this.backgroundRadioButton1.Size = new System.Drawing.Size(62, 17);
            this.backgroundRadioButton1.TabIndex = 0;
            this.backgroundRadioButton1.TabStop = true;
            this.backgroundRadioButton1.Text = "черный";
            this.backgroundRadioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nameRadioButton2);
            this.groupBox2.Controls.Add(this.nameRadioButton1);
            this.groupBox2.Location = new System.Drawing.Point(94, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(75, 73);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "вращение";
            // 
            // nameRadioButton2
            // 
            this.nameRadioButton2.AutoSize = true;
            this.nameRadioButton2.Location = new System.Drawing.Point(7, 44);
            this.nameRadioButton2.Name = "nameRadioButton2";
            this.nameRadioButton2.Size = new System.Drawing.Size(67, 17);
            this.nameRadioButton2.TabIndex = 1;
            this.nameRadioButton2.Text = "по кругу";
            this.nameRadioButton2.UseVisualStyleBackColor = true;
            // 
            // nameRadioButton1
            // 
            this.nameRadioButton1.AutoSize = true;
            this.nameRadioButton1.Checked = true;
            this.nameRadioButton1.Location = new System.Drawing.Point(7, 20);
            this.nameRadioButton1.Name = "nameRadioButton1";
            this.nameRadioButton1.Size = new System.Drawing.Size(54, 17);
            this.nameRadioButton1.TabIndex = 0;
            this.nameRadioButton1.TabStop = true;
            this.nameRadioButton1.Text = "центр";
            this.nameRadioButton1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 92);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "готово";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 122);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsForm";
            this.Text = "Настройка";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton backgroundRadioButton2;
        private System.Windows.Forms.RadioButton backgroundRadioButton1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton nameRadioButton2;
        private System.Windows.Forms.RadioButton nameRadioButton1;
        private System.Windows.Forms.Button button1;
    }
}
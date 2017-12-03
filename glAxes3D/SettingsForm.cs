using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace glAxes3D
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm f=new MainForm();
            f.renderControl1.BackColor =
                backgroundRadioButton1.Checked ? Color.Black : Color.White;
            f.renderControl1.simpleRotate =
                nameRadioButton1.Checked;
            f.Show();
            this.Hide();
        }
    }
}

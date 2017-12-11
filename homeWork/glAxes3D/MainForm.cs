using System.Windows.Forms;
using System;
namespace glAxes3D
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, System.EventArgs e)
        {
            renderControl1.oy = trackBar1.Value * Math.PI / 180;
            renderControl1.Invalidate();
        }
    }
}

using System.Windows.Forms;

namespace Proj
{
    public struct Point
    {
        public float x, y;
        public Point(float x,float y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return "(" + x.ToString("F2") + "," + y.ToString("F2") + ")";
        }
    }

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private void minTextBox_TextChanged(object sender, System.EventArgs e)
        {
            float min;
            float.TryParse(minTextBox.Text, out min);
            if (min >= -10 && min <= 10) renderControl1.xmin = min;
            renderControl1.Invalidate();
        }

        private void maxTextBox_TextChanged(object sender, System.EventArgs e)
        {
            float max;
            float.TryParse(maxTextBox.Text, out max);
            if (max >= -10 && max <= 10 && max > renderControl1.xmin) renderControl1.xmax = max;
            renderControl1.Invalidate();
        }

        private void x0TextBox_TextChanged(object sender, System.EventArgs e)
        {
            float x0;
            float.TryParse(x0TextBox.Text, out x0);
            if (x0 >= -10 && x0 <= 10) renderControl1.x0 = x0;
            renderControl1.Invalidate();
        }

        private void y0TextBox_TextChanged(object sender, System.EventArgs e)
        {
            float y0;
            float.TryParse(y0TextBox.Text, out y0);
            if (y0 >= -10 && y0 <= 10) renderControl1.y0 = y0;
            renderControl1.Invalidate();
        }

        private void radiusTextBox_TextChanged(object sender, System.EventArgs e)
        {
            float radius;
            float.TryParse(radiusTextBox.Text, out radius);
            if (radius >= 0 && radius <= 10) renderControl1.radius = radius;
            renderControl1.Invalidate();
        }

        private void groupBox1_Enter(object sender, System.EventArgs e)
        {

        }

        private void label4_Click(object sender, System.EventArgs e)
        {

        }

        private void label5_Click(object sender, System.EventArgs e)
        {

        }

        private void x2TextBox_TextChanged(object sender, System.EventArgs e)
        {
            float x2;
            float.TryParse(x2TextBox.Text, out x2);
            if (x2 >= -10 && x2 <= 10) renderControl1.x2 = x2;
            if (x2 < renderControl1.x1)
            {
                float z = renderControl1.x1;
                renderControl1.x1 = renderControl1.x2;
                renderControl1.x2 = z;

                z = renderControl1.y1;
                renderControl1.y1 = renderControl1.x2;
                renderControl1.y2 = z;
            }
            renderControl1.Invalidate();
        }

        private void y2TextBox_TextChanged(object sender, System.EventArgs e)
        {
            float y2;
            float.TryParse(y2TextBox.Text, out y2);
            if (y2 >= -10 && y2 <= 10) renderControl1.y2 = y2;
            renderControl1.Invalidate();
        }

        private void x1TextBox_TextChanged(object sender, System.EventArgs e)
        {
            float x1;
            float.TryParse(x1TextBox.Text, out x1);
            if (x1 >= -10 && x1 <= 10) renderControl1.x1 = x1;
            if (x1 > renderControl1.x2)
            {
                float z = renderControl1.x1;
                renderControl1.x1 = renderControl1.x2;
                renderControl1.x2 = z;

                z = renderControl1.y1;
                renderControl1.y1 = renderControl1.x2;
                renderControl1.y2 = z;
            }
            renderControl1.Invalidate();
        }

        private void y1TextBox_TextChanged(object sender, System.EventArgs e)
        {
            float y1;
            float.TryParse(y1TextBox.Text, out y1);
            if (y1 >= -10 && y1 <= 10) renderControl1.y1 = y1;
            renderControl1.Invalidate();
        }

        private void aTextBox_TextChanged(object sender, System.EventArgs e)
        {
            float a;
            float.TryParse(aTextBox.Text, out a);
            if (a >= -10 && a <= 10) renderControl1.a = a;
            renderControl1.Invalidate();
        }

        private void bTextBox_TextChanged(object sender, System.EventArgs e)
        {
            float b;
            float.TryParse(aTextBox.Text, out b);
            if (b >= -10 && b <= 10) renderControl1.b = b;
            renderControl1.Invalidate();
        }

        private void circleButton_Click(object sender, System.EventArgs e)
        {
            renderControl1.mode = true;
            renderControl1.Invalidate();
        }

        private void ellipseButton_Click(object sender, System.EventArgs e)
        {
            renderControl1.mode = false;
            renderControl1.Invalidate();
        }
    }
}

using System.Windows.Forms;

namespace glAxes3D
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //изменение параметров плоскости и перерисовка элемента
        private void aTextBox_TextChanged(object sender, System.EventArgs e)
        {
            double.TryParse(aTextBox.Text, out renderControl1.a);
            renderControl1.Invalidate();
        }

        private void bTextBox_TextChanged(object sender, System.EventArgs e)
        {
            double.TryParse(bTextBox.Text, out renderControl1.b);
            renderControl1.Invalidate();
        }

        private void cTextBox_TextChanged(object sender, System.EventArgs e)
        {
            double.TryParse(cTextBox.Text, out renderControl1.c);
            renderControl1.Invalidate();
        }

        private void dTextBox_TextChanged(object sender, System.EventArgs e)
        {
            double.TryParse(dTextBox.Text, out renderControl1.d);
            renderControl1.Invalidate();
        }
    }
}

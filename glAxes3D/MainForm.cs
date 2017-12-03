using System.Drawing;
using System.Windows.Forms;

namespace glAxes3D
{
    public partial class MainForm : Form
    {

        //нужно ли прятать курсор
        //*в полноэкранном режиме нужно
        public bool cursorHide = true;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, System.EventArgs e)
        {
            if (cursorHide)
                Cursor.Hide();
        }
    }
}

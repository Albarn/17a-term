using System.ComponentModel;

namespace glAxes3D
{
    [ToolboxItem(true)]
    public partial class RenderControl : OpenGL
    {
        public RenderControl()
        {
            InitializeComponent();
            MouseWheel += RenderControl_MouseWheel;
        }

        public override void OnRender()
        {
            glClearColor(BackColor);
            glColor(ForeColor);

            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
            glLoadIdentity();

            if (Width > Height)
                glViewport((Width-Height)/2, 0, Height, Height);
            else
                glViewport(0, (Height-Width)/2, Width, Width);
            glOrtho(-2, 2, -2, 2, -2, 2);

            double size = 1.5;
            glRotated(rx, 1, 0, 0);
            glRotated(ry, 0, 1, 0);
            glScaled(m, m, m);
            DrawAxes(size, 3);
            //OutText("OpenGL version - " + glGetString(GL_VERSION), 10, (3 + FontHeight) * 1);
            //OutText("OpenGL vendor - " + glGetString(GL_VENDOR), 10, (3 + FontHeight) * 2);
            // OutText( "Cyrilic test  - ъЪ эЭ юЮ яЯ ёЁ іІ їЇ", 10,(3 + FontHeight) * 3);
        }

        private void DrawAxes(double size, float w)
        {
            glLineWidth(w);
            glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
            glBegin(GL_LINES);
            glVertex3d(-size / 10, 0, 0);
            glVertex3d(size, 0, 0);
            glVertex3d(0, -size / 10, 0);
            glVertex3d(0, size, 0);
            glVertex3d(0, 0, -size / 10);
            glVertex3d(0, 0, size);
            glEnd();
            OutText("X", size, 0, 0);
            OutText("Y", 0, size, 0);
            OutText("Z", 0, 0, size);
        }

        bool MouseActive = false;
        int x0, y0;
        double rx = 0, ry = 0;
        private void RenderControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseActive = !MouseActive;
            x0 = e.X;
            y0 = e.Y;
        }

        private void RenderControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (MouseActive)
            {
                rx = rx + (e.Y - y0);
                ry = ry + (e.X - x0);
                x0 = e.X;
                y0 = e.Y;
                Invalidate();
            }
        }

        double m = 1;
        private void RenderControl_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            m += e.Delta / 1000.0;
            Invalidate();
        }

        private void RenderControl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseActive = !MouseActive;

        }
    }
}

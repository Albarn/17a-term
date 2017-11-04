using System.ComponentModel;
using System;

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

            glLineWidth(3 / 2f);
            glColor(System.Drawing.Color.Red);
            double x1 = 2 * size / 5, y1 = -1.5 * size / 5, z1 = -2 * size / 5, 
                r1 = 1 * size / 5;
            glTranslated(x1, y1, z1);
            IntPtr q = gluNewQuadric();
            gluQuadricDrawStyle(q, GL_LINE);
            gluSphere(q, r1, 8, 8);
            glTranslated(-x1, -y1, -z1);

            glColor(System.Drawing.Color.Yellow);
            double x2 = -2.5 * size / 5, y2 = -2 * size / 5, z2 = -2.5 * size / 5,
                r2 = 1 * size / 5, h2 = 1.5;
            glTranslated(x2, y2, z2);
            glRotated(90, 1, 0, 0);
            q = gluNewQuadric();
            gluQuadricDrawStyle(q, GL_LINE);
            gluCylinder(q, r2, r2, h2, 8, 8);
            glRotated(-90, 1, 0, 0);
            glTranslated(-x2, -y2, -z2);

            glColor(System.Drawing.Color.Blue);
            double x3 = -3 * size / 5, y3 = -3 * size / 5, z3 = 1.5 * size / 5,
                r3 = 1 * size / 5;
            glTranslated(x3, y3, z3);
            glRotated(90, 1, 0, 0);
            q = gluNewQuadric();
            gluQuadricDrawStyle(q, GL_LINE);
            gluDisk(q, 0, r3, 8, 4);
            glRotated(-90, 1, 0, 0);
            glTranslated(-x3, -y3, -z3);
        }

        private void DrawAxes(double size, float w)
        {
            glLineWidth(w/2);

            glLineStipple(1, 255);
            glEnable(GL_LINE_STIPPLE);
            glColor(System.Drawing.Color.Gray);
            glBegin(GL_LINES);
            for (double i = -size; i <= size; i += size / 5)
            {
                if (i != 0)
                {
                    glVertex3d(-size, 0, i);
                    glVertex3d(size, 0, i);
                    glVertex3d(i, 0, -size);
                    glVertex3d(i, 0, size);
                }
            }
            glEnd();
            glDisable(GL_LINE_STIPPLE);
            glColor(System.Drawing.Color.Black);


            glLineWidth(w / 2);
            glPolygonMode(GL_FRONT_AND_BACK, GL_LINE);
            glBegin(GL_LINES);
            glVertex3d(-size, 0, 0);
            glVertex3d(size, 0, 0);
            glVertex3d(0, -size, 0);
            glVertex3d(0, size, 0);
            glVertex3d(0, 0, -size);
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

using System.ComponentModel;

namespace Proj
{
    [ToolboxItem(true)]
    public partial class RenderControl : OpenGL
    {
        public RenderControl()
        {
            InitializeComponent();
        }

        public override void OnRender()
        {
            glClearColor(BackColor);
            glColor(ForeColor);

            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
            glLoadIdentity();

            glViewport(0, 0, Width, Height);

            float x1 = -9.5f, x2 = -0.5f, y1 = -3.5f, y2 = 0.5f;
            x1 /= 10;
            x2 /= 10;
            y1 /= 10;
            y2 /= 10;
            float vx = (x2 - x1) / 9;
            float vy = (y2 - y1) / 4;

            glLineWidth(1);
            glColor(System.Drawing.Color.Black);
            glBegin(GL_LINE_STRIP);
            glVertex3f(0, -1, 0);
            glVertex3f(0, 1, 0);
            glVertex3f(-0.01f, 0.92f, 0);
            glVertex3f(0, 1, 0);
            glVertex3f(0.01f, 0.92f, 0);
            glVertex3f(0, 1, 0);
            glVertex3f(0, 0, 0);
            glVertex3f(-1, 0, 0);
            glVertex3f(1, 0, 0);
            glVertex3f(0.95f, -0.02f, 0);
            glVertex3f(1, 0, 0);
            glVertex3f(0.95f, 0.02f, 0);
            glEnd();

            glEnable(GL_LINE_STIPPLE);
            glLineStipple(1, 61455);
            glBegin(GL_LINES);
            for (float i = -1; i < 1; i += 0.1f)
            {
                glVertex3f(i, -1, 0);
                glVertex3f(i, 1, 0);
            }
            for (float i = -1; i < 1; i += 0.1f)
            {
                glVertex3f(-1, i, 0);
                glVertex3f(1, i, 0);
            }
            glEnd();
            glDisable(GL_LINE_STIPPLE);

            glPointSize(5);
            glEnable(GL_POINT_SMOOTH);
            glBegin(GL_POINTS);
            glVertex3f(5 * vx + x1, y1, 0);
            glVertex3f(5 * vx + x1, vy + y1, 0);
            glVertex3f(6 * vx + x1, 4 * vy + y1, 0);
            glVertex3f(8 * vx + x1, 4 * vy + y1, 0);
            glVertex3f(8 * vx + x1, vy + y1, 0);
            glVertex3f(6 * vx + x1, y1, 0);
            glEnd();
            glDisable(GL_POINT_SMOOTH);

            glLineWidth(2);
            glBegin(GL_LINE_LOOP);
            glVertex3f(x1, y1, 0);
            glVertex3f(x1, vy + y1, 0);
            glVertex3f(vx + x1, 4 * vy + y1, 0);
            glVertex3f(3 * vx + x1, 4 * vy + y1, 0);
            glVertex3f(3 * vx + x1, vy + y1, 0);
            glVertex3f(vx + x1, y1, 0);
            glEnd();

            x1 = -1f; x2 = 8f; y1 = 1f; y2 = 5f;
            x1 /= 10;
            x2 /= 10;
            y1 /= 10;
            y2 /= 10;
            vx = (x2 - x1) / 9;
            vy = (y2 - y1) / 4;

            glColor(System.Drawing.Color.Blue);
            glBegin(GL_TRIANGLE_STRIP);
            glVertex3f(7 * vx + x1, y1, 0);
            glVertex3f(2 * vx + x1, y1, 0);
            glVertex3f(x2, vy + y1, 0);
            glVertex3f(x1, vy + y1, 0);
            glVertex3f(vx + x1, 2 * vy + y1, 0);
            glVertex3f(3 * vx + x1, 2 * vy + y1, 0);

            glVertex3f(x1, 3 * vy + y1, 0);
            glVertex3f(x2, 3 * vy + y1, 0);
            glVertex3f(2 * vx + x1, y2, 0);
            glVertex3f(7 * vx + x1, y2, 0);
            glEnd();
            //OutText("OpenGL version - " + glGetString(GL_VERSION), 10, (3 + FontHeight) * 1);
            //OutText("OpenGL vendor - " + glGetString(GL_VENDOR), 10, (3 + FontHeight) * 2);
            // OutText( "Cyrilic test  - ъЪ эЭ юЮ яЯ ёЁ іІ їЇ", 10,(3 + FontHeight) * 3);
        }

    }
}

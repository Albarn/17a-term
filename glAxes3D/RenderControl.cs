using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace glAxes3D
{

    [ToolboxItem(true)]
    public partial class RenderControl : OpenGL
    {
        public RenderControl()
        {
            InitializeComponent();
            timer1.Start();
        }

        public bool simpleRotate = true;

        public override void OnRender()
        {

            //установка начальных параметров
            glClearColor(BackColor);
            glColor(ForeColor);

            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
            glLoadIdentity();

            //границы области
            if (Width > Height)
                glViewport((Width - Height) / 2, 0, Height, Height);
            else
                glViewport(0, (Height - Width) / 2, Width, Width);
            glOrtho(-2, 2, -2, 2, -2, 2);

            //включение проверки глубины
            glEnable(GL_DEPTH_TEST);
            glRotated(-90, 1, 0, 0);
            glRotated(r, 0, 0, 1);

            //если выбрана анимация по центру,
            //фигура остается как есть,
            //иначе смещаем, объект будет ходить по кругу
            if (!simpleRotate)
                glTranslated(0.3, 0, 0);

            glPushMatrix();

            //освещение
            glEnable(GL_LIGHTING);
            glEnable(GL_LIGHT0);
            float[] lightpos = { 0.5f, 1, 0, 0 };
            glLightfv(GL_LIGHT0, GL_POSITION, lightpos);
            float[] color = { 0.5f, 1, 0 };
            glLightfv(GL_LIGHT0, GL_COLOR, color);
            glColorMaterial(GL_FRONT_AND_BACK, GL_EMISSION);
            glEnable(GL_COLOR_MATERIAL);
            glColorMaterial(GL_FRONT_AND_BACK, GL_AMBIENT_AND_DIFFUSE);

            glPopMatrix();

            //сохранение матрицы
            glPushMatrix();

            //рисуем шляпу
            glTranslated(0, 0, 1);
            glColor(System.Drawing.Color.Red);
            //перенос и поворот
            IntPtr q = gluNewQuadric();
            gluCylinder(q, 0.7, 0, 0.5, 20, 20);
            glPopMatrix();

            //рисуем голову
            glPushMatrix();

            glTranslated(0, 0, 1);
            glColor(System.Drawing.Color.White);
            //перенос и поворот
            q = gluNewQuadric();
            gluSphere(q, 0.3, 20, 20);
            glPopMatrix();


            //рисуем тело
            glPushMatrix();
            glTranslated(0, 0, -0.5);
            glColor(System.Drawing.Color.Brown);
            //перенос и поворот
            q = gluNewQuadric();
            gluCylinder(q, 0.5, 0, 1.5, 20, 20);
            glPopMatrix();

            //рисуем ножны
            glPushMatrix();
            glTranslated(-1, 0.3, 0);
            glRotated(80, 0, 1, 0);
            glColor(System.Drawing.Color.DarkGray);
            //перенос и поворот
            q = gluNewQuadric();
            gluCylinder(q, 0.03, 0.03, 1.5, 20, 20);
            glPopMatrix();
        }

        double r = 0;

        //таймер, что запускает анимацию
        private void timer1_Tick(object sender, EventArgs e)
        {
            r += 1;
            if (r > 360)
                r = 0;
            Invalidate();
        }

        //выход по нажатию кнопки мыши
        private void RenderControl_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RenderControl_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

        //выход по нажатию клавиши
        private void RenderControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            Application.Exit();
        }

        MouseEventArgs old = null;

        //выход из программы по передвижению мыши
        private void RenderControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (old == null)
                old = e;
            if (Math.Abs(old.X - e.X) > 2 ||
                Math.Abs(old.Y - e.Y) > 2)
            {
                Application.Exit();
            }
            old = e;
        }
    }
}
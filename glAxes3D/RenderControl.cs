using System.ComponentModel;
using System;

namespace glAxes3D
{
    //идентификатор списка отображения
    enum Shape
    {
        Sphere = 1,
        Disk,
        Cylinder
    }

    [ToolboxItem(true)]
    public partial class RenderControl : OpenGL
    {
        public RenderControl()
        {
            InitializeComponent();
            MouseWheel += RenderControl_MouseWheel;
        }

        //размер координатной плоскости
        double size = 1.5;

        //рисуется ли элемент в первый раз
        public bool first = true;

        //параметры плоскости
        public double a = 0, b = 0, c = 0, d = 0;

        public override void OnRender()
        {

            //установка начальных параметров
            glClearColor(BackColor);
            glColor(ForeColor);

            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
            glLoadIdentity();

            //границы области
            if (Width > Height)
                glViewport((Width-Height)/2, 0, Height, Height);
            else
                glViewport(0, (Height-Width)/2, Width, Width);
            glOrtho(-2, 2, -2, 2, -2, 2);

            //включение проверки глубины
            glEnable(GL_DEPTH_TEST);

            //поворот и масштабирование рисунка
            glRotated(rx, 1, 0, 0);
            glRotated(ry, 0, 1, 0);
            glScaled(m, m, m);
            DrawAxes(size, 3);

            //запись списков отображения
            if (first)
            {

                //сфера
                glNewList((uint)Shape.Sphere, GL_COMPILE);
                
                glLineWidth(3 / 2f);
                //устанавливаем красный цвет фигуры и координаты центра
                glColor(System.Drawing.Color.Red);
                double x1 = 2 * size / 5, y1 = -1.5 * size / 5, z1 = -2 * size / 5,
                    r1 = 1 * size / 5;

                //сохранение матрицы
                glPushMatrix();

                //перенос и поворот
                glTranslated(x1, y1, z1);
                glRotated(90, 0, 1, 0);
                IntPtr q = gluNewQuadric();
                gluQuadricDrawStyle(q, GL_LINE);
                
                gluSphere(q, r1, 8, 8);

                //востановление
                glPopMatrix();
                glEndList();

                glNewList((uint)Shape.Cylinder, GL_COMPILE);

                //цвет и центр
                glColor(System.Drawing.Color.Yellow);
                double x2 = -2.5 * size / 5, y2 = 1.5 * size / 5, z2 = -2 * size / 5,
                    r2 = 1 * size / 5, h2 = 1.5;

                //запись состояния
                glPushMatrix();

                //рисуем фигуру
                glTranslated(x2, y2, z2);
                glRotated(90, 1, 0, 0);
                q = gluNewQuadric();
                gluQuadricDrawStyle(q, GL_LINE);
                gluCylinder(q, r2, r2, h2, 8, 8);

                //востановление
                glPopMatrix();
                glEndList();

                //рисуем диск
                glNewList((uint)Shape.Disk, GL_COMPILE);
                glColor(System.Drawing.Color.Blue);
                double x3 = -3 * size / 5, y3 = -3 * size / 5, z3 = 1.5 * size / 5,
                    r3 = 1 * size / 5;
                glPushMatrix();
                glTranslated(x3, y3, z3);
                glRotated(90, 1, 0, 0);
                q = gluNewQuadric();
                gluQuadricDrawStyle(q, GL_LINE);
                gluDisk(q, 0, r3, 8, 4);
                glPopMatrix();
                glEndList();

                //списки записаны, можем использовать их повторно
                //еще раз их записывать не нужно
                first = false;
            }
            
            //описываем плоскость отсечения для цилиндра
            double[] plane = new double[] { a, b, c, d };
            glEnable(GL_CLIP_PLANE0);
            glClipPlane(GL_CLIP_PLANE0, plane);
            glCallList((uint)Shape.Cylinder);
            glDisable(GL_CLIP_PLANE0);

            //остальные фигуры рисуются в обычном режиме
            glCallList((uint)Shape.Disk);
            glCallList((uint)Shape.Sphere);
        }

        //рисуем координатную плоскость
        private void DrawAxes(double size, float w)
        {
            glLineWidth(w/2);

            //штриховая линия и серый цвет для сетки хоz
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

            //рисуем и подписываем оси
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

        //поворот и масштабирование рисунка по требованию пользователя
        bool MouseActive = false;
        int x0, y0;
        double rx = 0, ry = 0;

        //кнопка мыши отпущена, запоминаем координаты
        private void RenderControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MouseActive = !MouseActive;
            x0 = e.X;
            y0 = e.Y;
        }

        //движение мыши приводит к повороту рисунка
        private void RenderControl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (MouseActive)
            {
                //вычисляем угол поворота
                rx = rx + (e.Y - y0);
                ry = ry + (e.X - x0);
                x0 = e.X;
                y0 = e.Y;
                Invalidate();
            }
        }

        double m = 1;

        //масштабируем рисунок, когда пользователь крутит колесико мыши
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

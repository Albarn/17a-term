using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Proj
{
    [ToolboxItem(true)]
    public partial class RenderControl : OpenGL
    {

        public int n = 70;
        public RenderControl()
        {
            InitializeComponent();
        }

        //режим рисование(истина - окружность, ложь - эллипс)
        public bool mode = true;

        //границы масштабирования
        public float xmin = -1, xmax = 1, ymin = 0, ymax = 0;

        //параметры окружности и элипса, радиус, центр, ширина и высота
        public float radius = 1, x0 = 0, y0 = 0, a = 1, b = 1;

        //параметры отрезка
        public float x1 = 0, x2 = 0, y1 = 0, y2 = 0;

        /// <summary>
        /// функция окружности(верхняя часть)
        /// </summary>
        float f1P(float x)
        {
            if (radius >= x - x0)
                return y0 + (float)Math.Sqrt(radius * radius - (x - x0) * (x - x0));
            else
                return -1;
        }

        /// <summary>
        /// функция окружности(нижняя часть)
        /// </summary>
        float f1N(float x)
        {
            if (radius >= x - x0)
                return y0 - (float)Math.Sqrt(radius * radius - (x - x0) * (x - x0));
            else
                return -1;
        }

        /// <summary>
        /// рисуем окружность и отрезок
        /// </summary>
        void DrawCircle()
        {
            ymax = ymin = 0;

            //начало и конец ооф окружности
            float start = x0 - radius;
            float end = x0 + radius;
            //шаг
            float d = (end - start) / (n - 1);

            //вычисляем границы у для выполнения масштабирования
            ymax = y0 + radius;
            ymin = y0 - radius;

            //масштабируем
            //gluOrtho2D(xmin / 10, xmax / 10, ymin / 10, (ymin + xmax - xmin) / 10);
            gluOrtho2D(xmin / 10, xmax / 10, ymin / 10, (ymin + xmax - xmin) / 10);

            List<Point> points = new List<Point>();
            float xold = 0, yold = 0;
            float xt = 0, yt = 0;
            //строим график
            glBegin(GL_LINE_STRIP);
            for (int i = 0; i < n; i++)
            {
                float x = start + d * i;
                float y = f1P(x);

                glVertex3f(x / 10, y / 10, 0);

                //пересечение отрезков
                if (i != 0 && x > x1 && xold < x2)
                {
                    //коэффициенты при параметрах
                    float tx1 = x2 - x1;
                    float tx2 = x - xold;
                    float ty1 = y2 - y1;
                    float ty2 = y - yold;

                    //значение параметров
                    float tb = (tx1 * (y - y1) - ty1 * (x - x1)) / (ty1 * tx2 - tx1 * ty2);
                    if (ty1 * tx2 - tx1 * ty2 == 0) tb = 0;
                    float ta = (x - x1 + tx2 * tb) / tx1;

                    if (x1 + tx1 * ta >= xold && x1 + tx1 * ta <= x + d / 5)
                    {
                        xt = x1 + tx1 * ta;
                        yt = y1 + ty1 * ta;
                        points.Add(new Point(xt, yt));
                    }
                }
                xold = x;
                yold = y;
            }
            glEnd();
            glBegin(GL_LINE_STRIP);
            for (int i = 0; i < n; i++)
            {
                float x = start + d * i;
                float y = f1N(x);
                glVertex3f(x / 10, y / 10, 0);
            }
            glEnd();

            glColor(System.Drawing.Color.Red);
            glPointSize(5);

            glBegin(GL_POINTS);
            foreach (Point p in points)
                glVertex3f(p.x / 10, p.y / 10, 0);
            glEnd();

            glColor(System.Drawing.Color.Black);
            glBegin(GL_LINES);
            glVertex3f(x1 / 10, y1 / 10, 0);
            glVertex3f(x2 / 10, y2 / 10, 0);
            glEnd();

            //также выводим значение х
            ((MainForm)this.ParentForm).label1.Text = "";
            foreach (Point p in points)
                ((MainForm)this.ParentForm).label1.Text += p.ToString() + " ";
            glColor(System.Drawing.Color.Black);
        }

        /// <summary>
        /// рисуем эллипс
        /// </summary>
        void DrawEllipse()
        {
            ymax = ymin = 0;
            
            //шаг
            float d = (float)(2*Math.PI) / (n - 1);

            //вычисляем границы у для выполнения масштабирования
            ymax = y0 + b;
            ymin = y0 - b;

            //масштабируем
            //gluOrtho2D(xmin / 10, xmax / 10, ymin / 10, (ymin + xmax - xmin) / 10);
            gluOrtho2D(xmin / 10, xmax / 10, ymin / 10, (ymin + xmax - xmin) / 10);

            //float xold = sx, yold = sy;
            float xt = 0, yt = 0;
            //строим график
            glBegin(GL_LINE_STRIP);
            //float C = (float)Math.Cos(d);
            //float S = (float)Math.Sin(d);
            for (int i = 0; i < n; i++)
            {
                float x = a * (float)Math.Cos(d * i) + x0;
                float y = b * (float)Math.Sin(d * i) + y0;
                glVertex3f(x / 10, y / 10, 0);
            }
                //if (i == 0)
                //    glVertex3f(sx / 10, sy / 10, 0);
                //else
                //{
                //    float x = (xold - x0) * C - (yold - y0) * S * a / b + x0;
                //    float y = (yold - y0) * C - (xold - x0) * S * b / a + y0;

                //    glVertex3f(x / 10, y / 10, 0);
                //    xold = x;
                //    yold = y;
                //}
            glEnd();

        }

        public override void OnRender()
        {

            glClearColor(BackColor);
            glColor(ForeColor);

            //очистка и установка системы координат
            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
            glLoadIdentity();

            //делаем систему изотропной
            if (Width > Height)
                glViewport(0, 0, Height, Height);
            else
                glViewport(0, 0, Width, Width);

            if (mode)
                DrawCircle();
            else
                DrawEllipse();

            //масштабируем
            gluOrtho2D(xmin / 10, xmax / 10, ymin / 10, (ymin + xmax - xmin) / 10);

            //установка толщины и цвета линиии
            glLineWidth(1);
            glColor(System.Drawing.Color.Black);

            //чертим главные оси ох и оу
            glBegin(GL_LINE_STRIP);
            //ось ох
                glVertex3f(0, -1, 0);
                glVertex3f(0, 1, 0);
                glVertex3f(-0.01f, 0.92f, 0);   //
                glVertex3f(0, 1, 0);            //
                glVertex3f(0.01f, 0.92f, 0);    //правая стрелка
                                            //ось оу
                glVertex3f(0, 1, 0);
                glVertex3f(0, 0, 0);
                glVertex3f(-1, 0, 0);
                glVertex3f(1, 0, 0);
                glVertex3f(0.95f, -0.02f, 0);   //
                glVertex3f(1, 0, 0);            //
                glVertex3f(0.95f, 0.02f, 0);    //верхняя стрелка
            glEnd();

            //установка пунктирной линии
            glEnable(GL_LINE_STIPPLE);
            glLineStipple(1, 61455);

            //на каждом делении чертим линии
            //параллельные осям координат
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

            
        }
    }
}

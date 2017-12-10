using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace glAxes3D
{
    [ToolboxItem(true)]
    public partial class RenderControl : OpenGL
    {
        public RenderControl()
        {
            InitializeComponent();
            MouseWheel += RenderControl_MouseWheel;
            s = Math.Sqrt(a * a + c * c);

            //начальные позиции точек
            O1 = new double[,] { 
                { c }, 
                { 0 }, 
                { 0 }, 
                { 1 } };
            B = new double[,] {
                { 0 },
                { a },
                { 0 },
                { 1 } };
            C = new double[,] {
                { 0 },
                { B[1,0]+0.75*a },
                { 0 },
                { 1 } };
            A = new double[,] {
                { 0 },
                { C[1,0]+b },
                { 0 },
                { 1 } };
        }

        public override void OnRender()
        {
            glClearColor(BackColor);
            glColor(ForeColor);

            glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
            glLoadIdentity();

            if (Width > Height)
                glViewport((Width - Height) / 2, 0, Height, Height);
            else
                glViewport(0, (Height - Width) / 2, Width, Width);
            glOrtho(-2, 2, -2, 2, -2, 2);

            double size = 1.5;
            glRotated(rx, 1, 0, 0);
            glRotated(ry, 0, 1, 0);
            glScaled(m, m, m);
            DrawAxes(size, 3);

            //обновление положения точек
            O1 = new double[,] {
                { c },
                { 0 },
                { 0 },
                { 1 } };
            B = new double[,] {
                { 0 },
                { a },
                { 0 },
                { 1 } };
            C = new double[,] {
                { 0 },
                { B[1,0]+0.75*a },
                { 0 },
                { 1 } };
            A = new double[,] {
                { 0 },
                { C[1,0]+b },
                { 0 },
                { 1 } };

            double t = Math.Acos((0.52 - s * s) / 0.48);

            A = MulMatrixes(Txy(-C[0, 0], -C[1, 0]), A, 4, 1, 4);
            A = MulMatrixes(Rz(f+(Math.PI / 2) - t), A, 4, 1, 4);
            C = MulMatrixes(Rz((Math.PI / 2) - t), C, 4, 1, 4);
            A = MulMatrixes(Txy(C[0, 0], C[1, 0]), A, 4, 1, 4);

            B = MulMatrixes(Rz((Math.PI / 2 - t)), B, 4, 1, 4);

            double[,] Sa, Ss, Sb;
            Sa = new double[,]
            {
                {0,C[0,0] },
                {0,C[1,0] },
                {0,C[2,0] },
                {1,1 }
            };
            Ss = new double[,]
            {
                {O1[0,0],B[0,0] },
                {O1[1,0],B[1,0] },
                {O1[2,0],B[2,0] },
                {1,1 }
            };
            Sb = new double[,]
            {
                {C[0,0],A[0,0] },
                {C[1,0],A[1,0] },
                {C[2,0],A[2,0] },
                {1,1 }
            };

            Segment(Sa, 2, Color.Red);
            Segment(Sb, 2, Color.Green);
            Segment(Ss, 2, Color.Blue);
            //OutText("OpenGL version - " + glGetString(GL_VERSION), 10, (3 + FontHeight) * 1);
            //OutText("OpenGL vendor - " + glGetString(GL_VENDOR), 10, (3 + FontHeight) * 2);
            // OutText( "Cyrilic test  - ъЪ эЭ юЮ яЯ ёЁ іІ їЇ", 10,(3 + FontHeight) * 3);
        }

        //начальные парметры
        double a = 0.4;
        double b = 0.9;
        double c = 0.6;

        //отрезок s с ограничениями
        double s = 0.4;
        public double S {
            get
            {
                return s;
            }
            set
            {
                if (value >= c - a && value <= c + a)
                    s = value;
            }
        }

        //угол поворота CA
        public double f = 0;

        //ключевые точки

        double[,] O1, B, C, A;

        //поворот по оси ОZ
        private double[,] Rz(double a)
        {
            return new double[,]
            {
                {Math.Cos(a),Math.Sin(a),0,0 },
                {-Math.Sin(a),Math.Cos(a),0,0 },
                {0,0,1,0 },
                {0,0,0,1 }
            };
        }

        //перенос по XOY
        private double[,] Txy(double a,double b)
        {
            return new double[,]
            {
                {1,0,0,a },
                {0,1,0,b },
                {0,0,1,0 },
                {0,0,0,1 }
            };
        }

        //поворот от OY
        private double[,] Ry(double a)
        {
            return new double[,]
            {
                {Math.Cos(a),0,Math.Sin(a),0 },
                {0,1,0,0 },
                {-Math.Sin(a),0,Math.Cos(a),0 },
                {0,0,0,1 }
            };
        }

        /// <summary>
        /// рисование сегмента
        /// </summary>
        /// <param name="seg">матрица сегмента</param>
        /// <param name="width">длина линии</param>
        /// <param name="color">цвет линии</param>
        private void Segment(double[,] seg, int width, Color color)
        {
            glColor(color);
            glLineWidth(width);

            glBegin(GL_LINES);
            glVertex3d(seg[0, 0], seg[1, 0], seg[2, 0]);
            glVertex3d(seg[0, 1], seg[1, 1], seg[2, 1]);
            glEnd();
        }

        //перемножение матриц
        private double[,] MulMatrixes(double[,] op1, double[,] op2, int n,int m,int p)
        {
            double[,] res = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    for (int k = 0; k < p; k++)
                        res[i, j] += op1[i, k] * op2[k, j];

            return res;

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
        
        ///////////////////////////////////////////////
        //методы вращения и масштабирования изображения
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
        ///////////////////////////////////////////////

        //изменение модели
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //поворот CA
            if (keyData == Keys.Up)
            {
                f += 1/Math.PI;
                Invalidate();
            }
            if (keyData == Keys.Down)
            {
                f -= 1 / Math.PI;
                Invalidate();
            }

            //изменение S
            if (keyData == Keys.Left)
            {
                S -= 0.005;
                Invalidate();
            }
            if (keyData == Keys.Right)
            {
                S += 0.005;
                Invalidate();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}

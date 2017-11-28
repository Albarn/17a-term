using Class_Library;
using System;
using System.Windows.Forms;

namespace WinForm_App
{
    public partial class Form1 : Form
    {
        //левая и правая часть системы
        Matrix B = new Matrix();
        Matrix A = new Matrix();

        //генерируется ли таблица в данный момент
        bool gen = false;

        public Form1()
        {
            InitializeComponent();

            //создаем матрицу 1х1
            sizeNumericUpDown_ValueChanged(null, null);
        }

        //перестраиваем таблицу, по изменению значения в sizeNumericUpDown
        private void sizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            //извлекаем размер
            int size = (int)sizeNumericUpDown.Value;

            //чистим таблицы
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();

            //заголовок таблицы
            dataGridView2.Columns.Add("", "X");
            dataGridView2.Columns[dataGridView2.Columns.Count - 1].Width = 50;

            //делаем столько колонок и столбцов, сколько указал пользователь
            for (int i = 0; i < size; i++)
            {
                dataGridView1.Columns.Add("c" + i, (i + 1).ToString());
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width = 50;
                dataGridView1.Rows.Add("", "");
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = (i + 1).ToString();
                dataGridView2.Rows.Add("", "");
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].HeaderCell.Value = (i + 1).ToString();
            }

            //заголовок для правой части выражения
            dataGridView1.Columns.Add("a", "A");
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width = 50;

            //объявление матриц
            B = new Matrix();
            A = new Matrix();
        }

        //запись изменений из таблицы в матрицу
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //если нажата активная ячейка и таблица не генерируется в данный момент
            if (!gen && e.ColumnIndex >= 0 && e.RowIndex >= 0 && dataGridView1[e.ColumnIndex, e.RowIndex] != null)
                if (e.ColumnIndex != dataGridView1.Columns.Count - 1)
                {
                    //если выбрана не последняя колонка, записываем в В
                    float f;
                    float.TryParse(
                        dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(), out f);
                    B[e.RowIndex + 1, e.ColumnIndex + 1] = f;
                }
                else
                {
                    //иначе в А
                    float f;
                    float.TryParse(
                        dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(), out f);
                    A[e.RowIndex + 1, 1] = f;
                }
        }

        //генерация случайной системы
        private void randButton_Click(object sender, EventArgs e)
        {
            //отмечаем что матрица сейчас генерируется
            gen = true;

            //создаем матрицу и записываем ее в таблицу
            int size = (int)sizeNumericUpDown.Value;
            B = new Matrix(size, (float)trackBar1.Value / 20);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    dataGridView1[j, i].Value = B[i + 1, j + 1].ToString("F2");

            //тоже самое для правой части,
            //только генерируем вручную
            A = new Matrix();
            int count = (int)(size * (float)trackBar1.Value / 20);
            Random f = new Random(DateTime.Now.Millisecond);
            while (count > 0)
            {
                A[f.Next(1, size + 1), 1] = f.Next(100, 10000) / 100;
                count--;
            }
            for (int i = 0; i < size; i++)
                dataGridView1[dataGridView1.Columns.Count - 1, i].Value =
                    A[i + 1, 1].ToString("F2");
            gen = false;
        }

        //решаем систему
        private void resolveButton_Click(object sender, EventArgs e)
        {
            //метод обращения изменяет матрицу, поэтому копируем исходную
            Matrix V = B.Copy();
            if (!V.Invert() || B.Size != (int)sizeNumericUpDown.Value)
            {
                MessageBox.Show("главная диагональ содержит нули,\n" +
                    "обратную матрицу вычислить нельзя");
                MessageBox.Show("при текущих данных решение найти нельзя,\n" +
                    "заполните главную диагональ таблицы");
                return;
            }

            //получаем решение перемножением обращенной матрицы
            //на правую часть
            Matrix X = Matrix.MultiplyMatrixes(V, A);

            //запись решения в вектор справа
            for (int i = 0; i < B.Size; i++)
                dataGridView2.Rows[i].Cells[0].Value =
                    X[i + 1, 1].ToString("F2");
        }
    }
}

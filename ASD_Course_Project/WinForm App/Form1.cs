using Class_Library;
using System;
using System.Windows.Forms;

namespace WinForm_App
{
    public partial class Form1 : Form
    {
        Matrix B = new Matrix();
        Matrix A = new Matrix();
        bool gen = false;

        public Form1()
        {
            InitializeComponent();
            sizeNumericUpDown_ValueChanged(null, null);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int size = (int)sizeNumericUpDown.Value;
            dataGridView1.Columns.Clear();
            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("", "X");
            dataGridView2.Columns[dataGridView2.Columns.Count - 1].Width = 50;
            for (int i = 0; i < size; i++)
            {
                dataGridView1.Columns.Add("c" + i, (i + 1).ToString());
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width = 50;
                dataGridView1.Rows.Add("", "");
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = (i + 1).ToString();
                dataGridView2.Rows.Add("", "");
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].HeaderCell.Value = (i + 1).ToString();
            }
            dataGridView1.Columns.Add("a", "A");
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width = 50;
            B = new Matrix();
            A = new Matrix();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!gen && e.ColumnIndex >= 0 && e.RowIndex >= 0 && dataGridView1[e.ColumnIndex, e.RowIndex] != null)
                if (e.ColumnIndex != dataGridView1.Columns.Count - 1)
                {
                    float f;
                    float.TryParse(
                        dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(), out f);
                    B[e.RowIndex + 1, e.ColumnIndex + 1] = f;
                }
                else
                {

                    float f;
                    float.TryParse(
                        dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString(), out f);
                    A[e.RowIndex + 1, 1] = f;
                }
        }

        private void randButton_Click(object sender, EventArgs e)
        {
            gen = true;
            int size = (int)sizeNumericUpDown.Value;
            B = new Matrix(size, (float)trackBar1.Value / 20);
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    dataGridView1[j, i].Value = B[i + 1, j + 1].ToString("F2");
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

        private void resolveButton_Click(object sender, EventArgs e)
        {
            Matrix V = B.Copy();
            if (!V.Invert() || B.Size != (int)sizeNumericUpDown.Value)
            {
                MessageBox.Show("главная диагональ содержит нули,\n" +
                    "обратную матрицу вычислить нельзя");
                MessageBox.Show("при текущих данных решение найти нельзя,\n" +
                    "заполните главную диагональ таблицы");
                return;
            }
            Matrix X = Matrix.MultiplyMatrixes(V, A);
            for (int i = 0; i < B.Size; i++)
                dataGridView2.Rows[i].Cells[0].Value =
                    X[i + 1, 1].ToString("F2");
        }
    }
}

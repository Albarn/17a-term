using Class_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void sizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            int size = (int)sizeNumericUpDown.Value;
            dataGridView1.Columns.Clear();
            for (int i = 0; i < size; i++)
            {
                dataGridView1.Columns.Add("c" + i, (i + 1).ToString());
                dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width = 50;
                dataGridView1.Rows.Add("", "");
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].HeaderCell.Value = (i + 1).ToString();

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
                    dataGridView1[i, j].Value = B[i + 1, j + 1].ToString("F2");
            A = new Matrix();
            int count = (int)(size * (float)trackBar1.Value / 20);
            Random f = new Random(DateTime.Now.Millisecond);
            while (count > 0)
            {
                A[f.Next(1, size + 1), 1] = (float)f.Next(100, 10000) / 100;
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
            V.Invert();
            Matrix res = Matrix.MultiplyMatrixes(V, A);
            int size = res.Size;
            richTextBox1.Text = "";
            for (int i = 0; i < res.Size; i++)
                richTextBox1.Text += "x" + (i + 1) + ": " + 
                    res[i + 1, 1].ToString("F2") + " ";
        }
    }
}

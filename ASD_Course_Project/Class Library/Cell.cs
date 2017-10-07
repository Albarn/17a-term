namespace Class_Library
{
    /// <summary>
    /// ячейка разряженной матрицы
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// верхняя ячейка
        /// </summary>
        public Cell Up { get; private set; }

        /// <summary>
        /// ячейка слева
        /// </summary>
        public Cell Left { get; private set; }

        /// <summary>
        /// номер строки в матрице
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// номер столбца в матрице
        /// </summary>
        public int Col { get; private set; }

        /// <summary>
        /// значение ячейки
        /// </summary>
        public double val;

        public Cell(int row, int col, double val = 0)
        {
            Row = row;
            Col = col;
            this.val = val;
            Up = new Cell(this, false);
            Left = new Cell(this, true);
        }

        Cell(Cell c, bool isRow)
        {
            if (isRow)
            {
                Col = -1;
                Row = 0;
                Left = c;
            }
            else
            {
                Col = 0;
                Row = -1;
                Up = c;
            }
        }

        static public void Insert(Cell cell, Matrix m)
        {
            if (cell.val == 0) return;
            foreach (Cell c in m)
                if (c.Col == cell.Col && c.Row == cell.Row)
                    return;

            if (m.Count > 0)
            {
                bool row = true, col = true;
                foreach (Cell c in m)
                    if (!row && !col)
                        break;
                    else if (c.Row == cell.Row &&
                        c.Col > cell.Col &&
                        (c.Left.Col < cell.Col || c.Left.Col == -1))
                    {
                        cell.Left = c.Left;
                        c.Left = cell;
                        row = false;
                    }
                    else if (c.Col == cell.Col &&
                            c.Row > cell.Row &&
                            (c.Up.Row < cell.Row || c.Up.Row == -1))
                    {
                        cell.Up = c.Up;
                        c.Up = cell;
                        col = false;
                    }

                if(row)
                    foreach(Cell c in m)
                        if (c.Col == cell.Col && c.Up.Row == -1)
                        {
                            cell.Up = c.Up.Up;
                            c.Up.Up = cell;
                        }
                if(col)
                    foreach (Cell c in m)
                        if (c.Row == cell.Row && c.Left.Col == -1)
                        {
                            cell.Left = c.Left.Left;
                            c.Left.Left = cell;
                        }
            }
            m.Add(cell);
        }

        static public void Remove(Cell cell, Matrix m)
        {
            foreach (Cell c in m)
            {
                if (c.Up == cell)
                    c.Up = cell.Up;
                if (c.Left == cell)
                    c.Left = cell.Left;
            }

            m.Remove(cell);
        }

        static public void MakeStep(Cell centre, Matrix m)
        {
            if (centre.val == 0) return;

            Cell hor = centre.Left;
            Cell ver;
            while (hor != centre)
            {
                int col = hor.Col;
                if (col == -1)
                {
                    hor = hor.Left;
                    continue;
                }

                ver = centre.Up;
                while (ver != centre)
                {
                    int row = ver.Row;
                    if (row == -1) {
                        ver = ver.Up;
                        continue;
                    }

                    Cell cell = m[row, col];
                    if (cell == null)
                        Insert(new Cell(row, col, -((ver.val * hor.val) / centre.val)), m);
                    else
                    {
                        cell.val -= (ver.val * hor.val) / centre.val;
                        if (cell.val == 0)
                            Remove(cell, m);,
                    }

                    ver = ver.Up;
                }

                hor = hor.Left;
            }

            hor = centre.Left;
            while (hor != centre)
            {
                if (hor.Col == -1)
                {
                    hor = hor.Left;
                    continue;
                }
                hor.val /= -centre.val;
                hor = hor.Left;
            }

            ver = centre.Up;
            while (ver != centre)
            {
                if (ver.Row == -1)
                {
                    ver = ver.Up;
                    continue;
                }
                ver.val /= centre.val;
                ver = ver.Up;
            }

            centre.val = 1 / centre.val;
        }

        public static void InvertMatrix(Matrix m)
        {
            int pow = 0;
            foreach (Cell c in m)
            {
                if (c.Row > pow) pow = c.Row;
                if (c.Col > pow) pow = c.Col;
            }

            for (int i = 1; i <= pow; i++)
            {
                Cell cell = m[i, i];
                if (cell == null) return;
                MakeStep(cell, m);
            }
        }

        public static Matrix MultiplyMatrixes(Matrix op1, Matrix op2)
        {
            Matrix res = new Matrix();
            int r1, r2, c1, c2;
            r1 = r2 = c1 = c2 = 0;
            foreach (Cell c in op1)
            {
                if (c.Row > r1) r1 = c.Row;
                if (c.Col > c1) c1 = c.Col;
            }
            foreach (Cell c in op2)
            {
                if (c.Row > r2) r2 = c.Row;
                if (c.Col > c2) c2 = c.Col;
            }
            int size = r2 > c1 ? c1 : r2;

            for (int i = 1; i <= r1; i++)
            {
                for (int j = 1; j <= c2; j++)
                {
                    Cell cell = new Cell(i, j);
                    
                    for(int k = 1; k <= size; k++)
                    {
                        Cell o1 = op1[i, k];
                        Cell o2 = op2[k, j];
                        if (o1 != null && o2 != null)
                            cell.val += o1.val * o2.val;
                    }

                    Insert(cell, res);
                }
            }

            return res;
        }
    }
}

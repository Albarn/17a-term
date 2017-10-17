using System.Collections;
using System.Collections.Generic;

namespace Class_Library
{
    public class Matrix:IEnumerable<Cell>
    {
        List<Cell> cells;

        public Matrix()
        {
            cells = new List<Cell>();
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return ((IEnumerable<Cell>)cells).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Cell>)cells).GetEnumerator();
        }

        Cell this[int row, int col, int t]
        {
            get {
                foreach (Cell cell in cells)
                    if (cell.Row == row && cell.Col == col)
                        return cell;
                return null;
            }
        }

        public double this[int row, int col]
        {
            get
            {
                foreach (Cell cell in cells)
                    if (cell.Row == row && cell.Col == col)
                        return cell.val;
                return 0;
            }
            set {
                foreach (Cell cell in cells)
                    if (cell.Row == row && cell.Col == col)
                    {
                        cell.val = value;
                        return;
                    }
            }
        }
        

        public void Insert(int x, int y, double v)
        {
            Cell cell = new Cell(x, y, v);
            if (cell.val == 0) return;
            foreach (Cell c in this)
                if (c.Col == cell.Col && c.Row == cell.Row)
                    return;

            if (cells.Count > 0)
            {
                bool row = true, col = true;
                foreach (Cell c in this)
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

                if (row)
                    foreach (Cell c in this)
                        if (c.Col == cell.Col && c.Up.Row == -1)
                        {
                            cell.Up = c.Up.Up;
                            c.Up.Up = cell;
                        }
                if (col)
                    foreach (Cell c in this)
                        if (c.Row == cell.Row && c.Left.Col == -1)
                        {
                            cell.Left = c.Left.Left;
                            c.Left.Left = cell;
                        }
            }
            cells.Add(cell);
        }

        public void Remove(int x, int y)
        {
            Cell cell = this[x, y, 0];
            foreach (Cell c in this)
            {
                if (c.Up == cell)
                    c.Up = cell.Up;
                if (c.Left == cell)
                    c.Left = cell.Left;
            }

            cells.Remove(cell);
        }

        public void MakeStep(int x, int y)
        {
            Cell centre = this[x, y, 0];
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
                    if (row == -1)
                    {
                        ver = ver.Up;
                        continue;
                    }

                    Cell cell = this[row, col,0];
                    if (cell == null)
                        Insert(row, col, -((ver.val * hor.val) / centre.val));
                    else
                    {
                        cell.val -= (ver.val * hor.val) / centre.val;
                        if (cell.val == 0)
                            Remove(row, col);
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

        public void Invert()
        {
            int pow = 0;
            foreach (Cell c in this)
            {
                if (c.Row > pow) pow = c.Row;
                if (c.Col > pow) pow = c.Col;
            }

            for (int i = 1; i <= pow; i++)
            {
                Cell cell = this[i, i,0];
                if (cell == null) return;
                MakeStep(i,i);
            }
        }

        static public Matrix MultiplyMatrixes(Matrix op1, Matrix op2)
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

                    for (int k = 1; k <= size; k++)
                    {
                        Cell o1 = op1[i, k,0];
                        Cell o2 = op2[k, j,0];
                        if (o1 != null && o2 != null)
                            cell.val += o1.val * o2.val;
                    }

                    res.Insert(i, j, cell.val);
                }
            }

            return res;
        }
    }
}

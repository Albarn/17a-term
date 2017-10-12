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
        public Cell Up;

        /// <summary>
        /// ячейка слева
        /// </summary>
        public Cell Left;

        /// <summary>
        /// номер строки в матрице
        /// </summary>
        public int Row;

        /// <summary>
        /// номер столбца в матрице
        /// </summary>
        public int Col;

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
    }
}

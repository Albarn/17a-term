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

        /// <summary>
        /// создание обычного узла
        /// </summary>
        /// <param name="row">номер строки</param>
        /// <param name="col">номер столбца</param>
        /// <param name="val">значение</param>
        public Cell(int row, int col, double val = 0)
        {
            //заполняем поля и добавляем конечные узлы
            Row = row;
            Col = col;
            this.val = val;
            Up = new Cell(this, false);
            Left = new Cell(this, true);
        }

        /// <summary>
        /// конечный узел
        /// </summary>
        /// <param name="c">соседняя ячейка</param>
        /// <param name="isRow">это строковый узел</param>
        Cell(Cell c, bool isRow)
        {
            //если строковый
            if (isRow)
            {
                //он выходит за пределы матрицы
                Col = -1;
                Row = 0;

                //и ссылается на соседнюю ячейку,
                //образуя цикл
                Left = c;
            }
            //аналогично для столбца
            else
            {
                Col = 0;
                Row = -1;
                Up = c;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace Class_Library
{
    /// <summary>
    /// разряженная матрица
    /// </summary>
    public class SparseMatrix:IEnumerable<CellOfSparseMatrix>
    {
        /// <summary>
        /// список узлов
        /// </summary>
        List<CellOfSparseMatrix> cells;

        bool powerChanged = false;
        int power = 0;
        public int Power
        {
            get
            {
                if (powerChanged)
                {
                    //вычисляем ранг матрицы
                    power = 0;
                    foreach (CellOfSparseMatrix c in this)
                    {
                        if (c.Row > power) power = c.Row;
                        if (c.Col > power) power = c.Col;
                    }
                    powerChanged = false;
                }
                return power;
            }
        }

        /// <summary>
        /// создание пустой матрицы
        /// </summary>
        public SparseMatrix()
        {
            cells = new List<CellOfSparseMatrix>();
        }

        /// <summary>
        /// создание случайной матрицы
        /// </summary>
        /// <param name="size">ранг</param>
        /// <param name="density">плотность(от 0 до 1)</param>
        public SparseMatrix(int size, float density)
        {
            Random f = new Random(DateTime.Now.Millisecond);
            cells = new List<CellOfSparseMatrix>();

            //заполняем главную диагональ
            for (int i = 1; i <= size; i++)
#if DEBUG
                this[i, i] = f.Next(1, 100);
#else
                    this[i, i] = f.Next(100, 10000) / 100f;
#endif

            //оставшееся количество узлов, что нужно добавить
            int count = (int)(size * size * density) - size;
            while (count > 0)
            {
                int i = f.Next(1, size + 1);
                int j = f.Next(1, size + 1);
                if (this[i, j] == 0)
                {
#if DEBUG
                    this[i, j] = f.Next(1, 100);
#else
                    this[i, j] = f.Next(100, 10000) / 100f;
#endif
                    count--;
                }
            }
        }

        //методы поддержки перечисления коллекции cells
        public IEnumerator<CellOfSparseMatrix> GetEnumerator()
        {
            return ((IEnumerable<CellOfSparseMatrix>)cells).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<CellOfSparseMatrix>)cells).GetEnumerator();
        }

        /// <summary>
        /// индексация со взятием ячейки
        /// </summary>
        /// <param name="row">строка</param>
        /// <param name="col">столбец</param>
        /// <param name="t">параметр для перегрузки</param>
        CellOfSparseMatrix this[int row, int col, int t]
        {
            get {
                foreach (CellOfSparseMatrix cell in cells)
                    //перебираем до совпадения и возвращаем поле val
                    if (cell.Row == row)
                    {
                        CellOfSparseMatrix c = cell;
                        do
                        {
                            if (c.Col == col)
                                return c;
                            c = c.Left;
                        } while (c != cell);
                        return null;
                    }
                else if (cell.Col == col)
                    {
                        CellOfSparseMatrix c = cell;
                        do
                        {
                            if (c.Row == row)
                                return c;
                            c = c.Up;
                        } while (c != cell);
                        return null;
                    }
                return null;
            }
        }

        /// <summary>
        /// индексация со взятием значения
        /// </summary>
        /// <param name="row">строка</param>
        /// <param name="col">столбец</param>
        public double this[int row, int col]
        {
            get
            {
                CellOfSparseMatrix res = this[row, col, 0];

                if (res != null) return res.val;
                else
                    //если узла в списке нет - это ноль
                    return 0;
            }
            set {
                CellOfSparseMatrix c = this[row, col, 0];
                if (c == null)
                {
                    //если такого узла нет, его нужно вставить
                    InsertCell(row, col, value);
                }
                else
                {
                    c.val = value;
                    if (c.val == 0) RemoveCell(c.Row, c.Col);
                }
            }
        }
        
        /// <summary>
        /// вставка нового узла в матрицу
        /// </summary>
        /// <param name="i">строка</param>
        /// <param name="j">столбец</param>
        /// <param name="value">значение</param>
        void InsertCell(int i, int j, double value)
        {

            //создаем ячейку и проверяем на ноль
            CellOfSparseMatrix cell = new CellOfSparseMatrix(i, j, value);
            if (cell.val == 0) return;

            //если узел в этом месте уже есть,
            //операция неверна
            foreach (CellOfSparseMatrix c in this)
                if (c.Col == cell.Col && c.Row == cell.Row)
                    return;

            //если в матрице есть узлы
            if (cells.Count > 0)
            {
                //ищем соседнюю ячейку
                foreach (CellOfSparseMatrix c in this)

                    //если узел на той же строке и между текущей ячейкой
                    //и следующей добавляем ссылки
                    if (c.Row == cell.Row)
                    {
                        if (c.Col > cell.Col &&
                        (c.Left.Col < cell.Col || c.Left.Col == -1))
                        {
                            cell.Left = c.Left;
                            c.Left = cell;
                        }
                        else if (c.Col < cell.Col && c.Left.Col == -1 &&
                            c.Left.Left.Col < cell.Col)
                        {
                            if (c.Left.Left == c)
                            {
                                c.Left.Left = cell;
                                cell.Left = c;
                            }
                            else
                            {
                                cell.Left = c.Left.Left;
                                c.Left.Left = cell;
                            }
                        }
                    }
                    //аналогично для столбца
                    else if (c.Col == cell.Col)
                    {
                        if (c.Row > cell.Row &&
                            (c.Up.Row < cell.Row || c.Up.Row == -1))
                        {
                            cell.Up = c.Up;
                            c.Up = cell;
                        }
                        else if (c.Row < cell.Row && c.Up.Row == -1 &&
                            c.Up.Up.Row < cell.Row)
                        {
                            if (c.Up.Up == c)
                            {
                                cell.Up = c;
                                c.Up.Up = cell;
                            }
                            else
                            {
                                cell.Up = c.Up.Up;
                                c.Up.Up = cell;
                            }
                        }
                    }
            }

            //и наконец добавляем узел в список ячеек матрицы
            cells.Add(cell);
            powerChanged = true;
        }

        /// <summary>
        /// удаление узла из матрицы
        /// </summary>
        /// <param name="i">строка</param>
        /// <param name="j">столбец</param>
        void RemoveCell(int i, int j)
        {
            CellOfSparseMatrix cell = this[i, j, 0];
            foreach (CellOfSparseMatrix c in this)
            {
                //делаем обход удалемой ячейки
                if (c.Up == cell)
                    c.Up = cell.Up;
                if (c.Left == cell)
                    c.Left = cell.Left;
            }

            //и удаляем узел из списка ячеек
            cells.Remove(cell);
            powerChanged = true;
        }

        /// <summary>
        /// шаг ОЖИ
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public bool MakeAxialStep(int i, int j)
        {
            //Шаг 1.[Начальная установка]
            //получаем ячейку и проверяем на равенство нулю
            CellOfSparseMatrix centre = this[i, j, 0];
            if (centre.val == 0) return false;

            //Шаг 2.[Найти новый столбец]
            //начинаем обработку узлов, что не принадлежат
            //осевой строке/столбцу
            //сдвигаемся от осевого элемента
            CellOfSparseMatrix hor = centre.Left;
            CellOfSparseMatrix ver;

            //и ведем цикл пока на него не наткнемся
            while (hor != centre)
            {
                int col = hor.Col;
                if (col == -1)
                {
                    hor = hor.Left;
                    continue;
                }

                //Шаг 3.[Найти новую строку]
                //такой же цикл для столбца
                ver = centre.Up;
                while (ver != centre)
                {
                    int row = ver.Row;
                    if (row == -1)
                    {
                        ver = ver.Up;
                        continue;
                    }

                    //Шаг 4.[Осевая операция]
                    //если элемента не было, добавляем
                    //если обнулился, убираем
                    CellOfSparseMatrix cell = this[row, col,0];
                    if (cell == null)
                        InsertCell(row, col, -((ver.val * hor.val) / centre.val));
                    else
                    {
                        cell.val -= (ver.val * hor.val) / centre.val;
                        if (cell.val == 0)
                            RemoveCell(row, col);
                    }

                    ver = ver.Up;
                }

                hor = hor.Left;
            }

            //Шаг 5.[Обработка осевой строки]
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

            //Шаг 6.[Обработка осевого столбца]
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

            //Шаг 7.[Обработка осевого элемента]
            centre.val = 1 / centre.val;

            return true;
        }

        /// <summary>
        /// обращение матрицы
        /// </summary>
        public bool InvertMatrix()
        {
            //вычисляем ранг матрицы
            int pow = 0;
            foreach (CellOfSparseMatrix c in this)
            {
                if (c.Row > pow) pow = c.Row;
                if (c.Col > pow) pow = c.Col;
            }

            //проверка главной диагонали,
            //не должно быть нулевых элементов
            for (int i = 1; i <= pow; i++)
                if (this[i, i] == 0) return false;

            //для элементов главной диагонали вызываем
            //метод ОЖИ
            for (int i = 1; i <= pow; i++)
                if (!MakeAxialStep(i, i))
                    return false;
            return true;
        }

        /// <summary>
        /// умножение матриц
        /// </summary>
        /// <param name="op1">левый операнд</param>
        /// <param name="op2">правый операнд</param>
        /// <returns></returns>
        static public SparseMatrix MultiplyMatrixes(SparseMatrix op1, SparseMatrix op2)
        {
            //правая часть выражения
            SparseMatrix res = new SparseMatrix();

            //размеры матриц
            int r1, r2, c1, c2;
            r1 = r2 = c1 = c2 = 0;
            foreach (CellOfSparseMatrix c in op1)
            {
                if (c.Row > r1) r1 = c.Row;
                if (c.Col > c1) c1 = c.Col;
            }
            foreach (CellOfSparseMatrix c in op2)
            {
                if (c.Row > r2) r2 = c.Row;
                if (c.Col > c2) c2 = c.Col;
            }
            //общий размер
            int size = r2 > c1 ? c1 : r2;

            //выполняем умножение по правилу строка на столбец
            for (int i = 1; i <= r1; i++)
                for (int j = 1; j <= c2; j++)
                    for (int k = 1; k <= size; k++)
                        res[i, j] += op1[i, k] * op2[k, j];

            return res;
        }

        /// <summary>
        /// копирование матрицы
        /// </summary>
        public SparseMatrix CopyMatrix()
        {
            SparseMatrix res = new SparseMatrix();
            foreach (CellOfSparseMatrix cell in cells)
                res[cell.Row, cell.Col] = cell.val;
            return res;
        }
    }
}

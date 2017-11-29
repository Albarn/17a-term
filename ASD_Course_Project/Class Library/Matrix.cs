using System;
using System.Collections;
using System.Collections.Generic;

namespace Class_Library
{
    /// <summary>
    /// разряженная матрица
    /// </summary>
    public class SparseMatrixOfRealNumbers:IEnumerable<NodeOfSparseMatrix>
    {
        /// <summary>
        /// список узлов
        /// </summary>
        List<NodeOfSparseMatrix> cells;

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
                    foreach (NodeOfSparseMatrix c in this)
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
        public SparseMatrixOfRealNumbers()
        {
            cells = new List<NodeOfSparseMatrix>();
        }

        /// <summary>
        /// создание случайной матрицы
        /// </summary>
        /// <param name="size">ранг</param>
        /// <param name="density">плотность(от 0 до 1)</param>
        public SparseMatrixOfRealNumbers(int size, float density)
        {
            Random f = new Random(DateTime.Now.Millisecond);
            cells = new List<NodeOfSparseMatrix>();

            //заполняем главную диагональ
            for (int i = 1; i <= size; i++)
                this[i, i] = f.Next(100, 10000) / 100f;

            //оставшееся количество узлов, что нужно добавить
            int count = (int)(size * size * density) - size;
            while (count > 0)
            {
                int i = f.Next(1, size + 1);
                int j = f.Next(1, size + 1);
                if (this[i, j] == 0)
                {
                    this[i, j] = f.Next(100, 10000) / 100f;
                    count--;
                }
            }
        }

        //методы поддержки перечисления коллекции cells
        public IEnumerator<NodeOfSparseMatrix> GetEnumerator()
        {
            return ((IEnumerable<NodeOfSparseMatrix>)cells).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<NodeOfSparseMatrix>)cells).GetEnumerator();
        }

        /// <summary>
        /// индексация со взятием ячейки
        /// </summary>
        /// <param name="row">строка</param>
        /// <param name="col">столбец</param>
        /// <param name="t">параметр для перегрузки</param>
        NodeOfSparseMatrix this[int row, int col, int t]
        {
            get {
                foreach (NodeOfSparseMatrix cell in cells)
                    //перебираем до совпадения и возвращаем поле val
                    if (cell.Row == row)
                    {
                        NodeOfSparseMatrix c = cell;
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
                        NodeOfSparseMatrix c = cell;
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
                NodeOfSparseMatrix res = this[row, col, 0];

                if (res != null) return res.val;
                else
                    //если узла в списке нет - это ноль
                    return 0;
            }
            set {
                NodeOfSparseMatrix c = this[row, col, 0];
                if (c == null)
                {
                    //если такого узла нет, его нужно вставить
                    InsertNode(row, col, value);
                }
                else
                {
                    c.val = value;
                    if (c.val == 0) RemoveNode(c.Row, c.Col);
                }
            }
        }
        
        /// <summary>
        /// вставка нового узла в матрицу
        /// </summary>
        /// <param name="i">строка</param>
        /// <param name="j">столбец</param>
        /// <param name="v">значение</param>
        void InsertNode(int i, int j, double v)
        {

            //создаем ячейку и проверяем на ноль
            NodeOfSparseMatrix cell = new NodeOfSparseMatrix(i, j, v);
            if (cell.val == 0) return;

            //если узел в этом месте уже есть,
            //операция неверна
            foreach (NodeOfSparseMatrix c in this)
                if (c.Col == cell.Col && c.Row == cell.Row)
                    return;

            //если в матрице есть узлы
            if (cells.Count > 0)
            {
                //ищем соседнюю ячейку
                foreach (NodeOfSparseMatrix c in this)

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
        void RemoveNode(int i, int j)
        {
            NodeOfSparseMatrix cell = this[i, j, 0];
            foreach (NodeOfSparseMatrix c in this)
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
            NodeOfSparseMatrix centre = this[i, j, 0];
            if (centre.val == 0) return false;

            //Шаг 2.[Найти новый столбец]
            //начинаем обработку узлов, что не принадлежат
            //осевой строке/столбцу
            //сдвигаемся от осевого элемента
            NodeOfSparseMatrix hor = centre.Left;
            NodeOfSparseMatrix ver;

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
                    NodeOfSparseMatrix cell = this[row, col,0];
                    if (cell == null)
                        InsertNode(row, col, -((ver.val * hor.val) / centre.val));
                    else
                    {
                        cell.val -= (ver.val * hor.val) / centre.val;
                        if (cell.val == 0)
                            RemoveNode(row, col);
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
            foreach (NodeOfSparseMatrix c in this)
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
        static public SparseMatrixOfRealNumbers MultiplyMatrixes(SparseMatrixOfRealNumbers op1, SparseMatrixOfRealNumbers op2)
        {
            //правая часть выражения
            SparseMatrixOfRealNumbers res = new SparseMatrixOfRealNumbers();

            //размеры матриц
            int r1, r2, c1, c2;
            r1 = r2 = c1 = c2 = 0;
            foreach (NodeOfSparseMatrix c in op1)
            {
                if (c.Row > r1) r1 = c.Row;
                if (c.Col > c1) c1 = c.Col;
            }
            foreach (NodeOfSparseMatrix c in op2)
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
        public SparseMatrixOfRealNumbers CopyMatrix()
        {
            SparseMatrixOfRealNumbers res = new SparseMatrixOfRealNumbers();
            foreach (NodeOfSparseMatrix cell in cells)
                res[cell.Row, cell.Col] = cell.val;
            return res;
        }
    }
}

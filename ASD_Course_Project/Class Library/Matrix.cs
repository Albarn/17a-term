using System.Collections;
using System.Collections.Generic;

namespace Class_Library
{
    public class Matrix:IEnumerable<Cell>
    {
        List<Cell> cells;

        public int Count { get
            {
                return cells.Count;
            } }

        public void Add(Cell c)
        {
            cells.Add(c);
        }

        public void Remove(Cell c)
        {
            cells.Remove(c);
        }

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

        public Cell this[int row, int col]
        {
            get {
                foreach (Cell cell in cells)
                    if (cell.Row == row && cell.Col == col)
                        return cell;
                return null;
            }
        }
    }
}

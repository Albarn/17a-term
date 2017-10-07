using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_Library;

namespace Console_App
{
    class Program
    {
        static void Main()
        {
            Matrix m = new Matrix();
            do
            {
                int r, c, v;
                Console.Write("r:");
                int.TryParse(Console.ReadLine(), out r);
                Console.Write("c:");
                int.TryParse(Console.ReadLine(), out c);
                Console.Write("v:");
                int.TryParse(Console.ReadLine(), out v);
                Cell.Insert(new Cell(r, c, v), m);
                Console.Write("stop?");
            } while (Console.ReadLine() != "Y");
            Cell.InvertMatrix(m);
            Matrix m2 = new Matrix();
            do
            {
                int r, c, v;
                Console.Write("r:");
                int.TryParse(Console.ReadLine(), out r);
                Console.Write("c:");
                int.TryParse(Console.ReadLine(), out c);
                Console.Write("v:");
                int.TryParse(Console.ReadLine(), out v);
                Cell.Insert(new Cell(r, c, v), m2);
                Console.Write("stop?");
            } while (Console.ReadLine() != "Y");
            Matrix er = Cell.MultiplyMatrixes(m, m2);
            return;
        }
    }
}

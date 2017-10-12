using Class_Library;
using System;

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
                m.Insert(new Cell(r, c, v));
                Console.Write("stop?");
            } while (Console.ReadLine() != "Y");
            m.Invert();
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
                m2.Insert(new Cell(r, c, v));
                Console.Write("stop?");
            } while (Console.ReadLine() != "Y");
            Matrix er = Matrix.MultiplyMatrixes(m, m2);
            return;
        }
    }
}

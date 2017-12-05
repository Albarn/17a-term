using Class_Library;
using System;

namespace Console_App
{
    class Program
    {
        static void Main()
        {
            SparseMatrix m = new SparseMatrix();
            do
            {
                int r, c, v;
                Console.Write("r:");
                int.TryParse(Console.ReadLine(), out r);
                Console.Write("c:");
                int.TryParse(Console.ReadLine(), out c);
                Console.Write("v:");
                int.TryParse(Console.ReadLine(), out v);
                m[r, c] = v;
                Console.Write("stop?");
            } while (Console.ReadLine() != "Y");
            m.InvertMatrix();
            SparseMatrix m2 = new SparseMatrix();
            do
            {
                int r, c, v;
                Console.Write("r:");
                int.TryParse(Console.ReadLine(), out r);
                Console.Write("c:");
                int.TryParse(Console.ReadLine(), out c);
                Console.Write("v:");
                int.TryParse(Console.ReadLine(), out v);
                m2[r, c] = v;
                Console.Write("stop?");
            } while (Console.ReadLine() != "Y");
            SparseMatrix er = SparseMatrix.MultiplyMatrixes(m, m2);
            return;
        }
    }
}

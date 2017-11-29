using Class_Library;
using System;

namespace Console_App
{
    class Program
    {
        static void Main()
        {
            SparseMatrixOfRealNumbers m = new SparseMatrixOfRealNumbers();
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
            SparseMatrixOfRealNumbers m2 = new SparseMatrixOfRealNumbers();
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
            SparseMatrixOfRealNumbers er = SparseMatrixOfRealNumbers.MultiplyMatrixes(m, m2);
            return;
        }
    }
}

using L4;
using System;
using System.Collections.Generic;

namespace Hw
{
    class Program
    {
        static void Main()
        {
            //чтение выражения
            string str = Console.ReadLine();

            List<Vector> res;
            bool b = _List(str, 0, str.Length - 1, out res);
            //вызов процедуры начального терминала на всей строке
            Console.WriteLine(b);
            Console.WriteLine("Свертка:");
            _ConList(res);

            //выход по нажатию кнопки
            Console.Read();
        }

        /// <summary>
        /// свертка списка векторов
        /// </summary>
        /// <param name="list">список</param>
        static void _ConList(List<Vector> list)
        {
            //если список не пуст
            if (list.Count != 0)
            {

                //выводим вектор
                _ConVector(list[0]);
                for(int i = 1; i < list.Count; i++)
                {

                    //следующие вектора выводятся после запятой
                    Console.Write(", 2, ");
                    _ConVector(list[i]);
                }
            }
        }

        /// <summary>
        /// процедура свертки вектора
        /// </summary>
        /// <param name="vector">вектор</param>
        static void _ConVector(Vector vector)
        {
            Console.Write("3, ");                   //открывающая скобка
            Console.Write("1." + vector.x + ", ");  //первое целое
            Console.Write("2, ");                   //запятая
            Console.Write("1." + vector.y + ", ");  //второе целое
            Console.Write("2, ");                   //запятая
            Console.Write("1." + vector.z + ", ");  //третье целое
            Console.Write("4");                     //закрывающаяся скобка
        }

        /// <summary>
        /// распознавание списка векторов
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <param name="e">индекс последнего элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool _List(string s, int b, int e, out List<Vector> res)
        {
            res = new List<Vector>();
            //если пустая цепочка возвращаем истину
            if (b > e)
                return true;

            //иначе есть нетерминал Вектор
            int i = b;
            for (; i <= e && i < s.Length; i++)

                //А - вектор, его конец - 
                //закрывающаяся скобка
                if (s[i] == ')') break;

            Vector a;
            //если это не вектор возвращаем ложь
            if (!_Vector(s, b, i, out a)) return false;

            res.Add(a);

            //если следующих векторов нет, возвращаем истину
            if (i == s.Length - 1)
                return true;

            //переходим к следующему вектору
            i++;

            //пока не дойдем до конца строки
            //читаем вектора
            while (i < s.Length)
            {
                //после вектора должна быть запятая
                if (s[i] != ',')
                {
                    Console.WriteLine("На позиции " + (i + 1) + " ожидался символ ','");
                    return false;
                }

                //находим границы вектора и читаем его
                b = i + 1;
                for (; i < s.Length && s[i] != ')'; i++) ;
                if (!_Vector(s, b, i, out a)) return false;
                res.Add(a);

                //переходим к следующему вектору
                i++;
            }

            //мы закончили работу и возвращаем истину
            return true;
        }

        /// <summary>
        /// распознавание вектора
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <param name="e">индекс последнего элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool _Vector(string s, int b, int e, out Vector res)
        {
            res = null;
            //вектор не может быть пустой цепочкой,
            //первый символ должен быть открывающей скобкой
            if (b > e || b > s.Length - 1 || s[b] != '(')
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидался символ '('");
                return false;
            }

            int x, y, z;
            //от скобки до запятой первое целое число
            int i = b + 1;
            try
            {
                for (; i < s.Length && i <= e && s[i] != ','; i++) ;
                x = int.Parse(s.Substring(b + 1, i - 1 - b));

                //от запятой до запятой второе число
                i++;
                b = i;
                for (; i < s.Length && i <= e && s[i] != ','; i++) ;
                y = int.Parse(s.Substring(b, i - b));

                //от запятой до скобки последнее число
                i++;
                b = i;
                for (; i < s.Length && i <= e && s[i] != ')'; i++) ;
                z = int.Parse(s.Substring(b, i - b));
            }
            catch
            {
                Console.WriteLine("На позиции " + (i + 1) + " ожидалось целое число");
                return false;
            }
            //если конец вектора правилен возвращаем истину
            if (i < s.Length && i == e && s[i] == ')')
            {
                res = new Vector(x, y, z);
                return true;
            }
            //иначе сообщаем об ошибке в выражении
            else
            {
                Console.WriteLine("На позиции " + (i + 1) + " ожидался символ ')'");
                return false;
            }
        }
    }
}

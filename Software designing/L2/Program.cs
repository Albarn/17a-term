using System;

namespace L2
{
    class Program
    {
        static void Main()
        {
            //чтение выражения
            string str = Console.ReadLine();

            //вызов процедуры начального терминала на всей строке
            int b = 0;
            Console.Write(S(str, 0));

            //выход по нажатию кнопки
            Console.Read();
        }

        /// <summary>
        /// распознавание списка векторов
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool S(string s, int b)
        {
            s22:
            if (s.Length == 0)
                return true;
            s23:
            if (b < s.Length && s[b] == '(')
            {
                if (!A(s, ref b))
                    return false;
                else b++;
            }
            else
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидался символ '('");
                return false;
            }
            s24:
            if (!B(s, ref b))
                return false;
            s25:
            if (b < s.Length)
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидался символ ','");
                return false;
            }
            return true;
        }

        /// <summary>
        /// распознавание следующего вектора
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool B(string s, ref int b)
        {
            s18:
            if (b < s.Length && s[b] == ',')
            {
                b++;
                goto s20;
            }
            s19:
            return true;
            s20:
            if (b < s.Length && s[b] == '(')
            {
                if (!A(s, ref b))
                    return false;
                else b++;
            }
            else
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидался символ '('");
                return false;
            }
            s21:
            return B(s, ref b);
        }

        /// <summary>
        /// распознавание вектора
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool A(string s, ref int b)
        {
            s10:
            if (s[b] == '(')
                b++;
            else
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидался символ '('");
                return false;
            }
            s11:
            if (b < s.Length && s[b] == '-' || (s[b] >= '0' && s[b] <= '9'))
            {
                if (!Z(s, ref b))
                    return false;
            }
            else
            {
                Console.WriteLine("На позиции " + (b + 1) +
                    " ожидалась цифра или символ '-'");
                return false;
            }
            s12:
            if (b < s.Length && s[b] == ',')
                b++;
            else
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидался символ ','");
                return false;
            }
            s13:
            if (b < s.Length && (s[b] == '-' || (s[b] >= '0' && s[b] <= '9')))
            {
                if (!Z(s, ref b))
                    return false;
            }
            else
            {
                Console.WriteLine("На позиции " + (b + 1) +
                    " ожидалась цифра или символ '-'");
                return false;
            }
            s14:
            if (b < s.Length && s[b] == ',')
                b++;
            else
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидался символ ','");
                return false;
            }
            s15:
            if (b < s.Length && (s[b] == '-' || (s[b] >= '0' && s[b] <= '9')))
            {
                if (!Z(s, ref b))
                    return false;
            }
            else
            {
                Console.WriteLine("На позиции " + (b + 1) +
                    " ожидалась цифра или символ '-'");
                return false;
            }
            s16:
            if (!(b < s.Length) || s[b] != ')')
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидался символ ')'");
                return false;
            }
            s17:
            return true;
        }

        /// <summary>
        /// распознавание целого числа
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool Z(string s, ref int b)
        {
            s6:
            if (s[b] == '0')
            {
                b++;
                goto s9;
            }
            s7:
            if (b < s.Length && s[b] == '-')
                b++;
            s8:
            if (b < s.Length && s[b] >= '1' && s[b] <= '9')
                return N(s, ref b);
            else
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидалась цифра от 1 до 9");
                return false;
            }
            s9:
            return true;
        }

        /// <summary>
        /// распознавание натурального числа
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool N(string s, ref int b)
        {
            s3:
            if (s[b] >= '1' && s[b] <= '9')
                b++;
            else
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидалась цифра от 1 до 9");
                return false;
            }
            s4:
            if (b < s.Length && s[b] >= '0' && s[b] <= '9')
                return D(s, ref b);

            s5:
            return true;
        }

        /// <summary>
        /// распознавание следующей цифры
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool D(string s, ref int b)
        {
            s1:
            if (b < s.Length && s[b] >= '0' && s[b] <= '9')
            {
                b++;
                goto s1;
            }
            s2:
            return true;
        }
    }
}
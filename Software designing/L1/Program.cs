using System;

namespace L1
{
    class Program
    {
        static void Main()
        {
            //чтение выражения
            string str = Console.ReadLine();

            //вызов процедуры начального терминала на всей строке
            Console.Write(S(str, 0, str.Length - 1));

            //выход по нажатию кнопки
            Console.Read();
        }

        /// <summary>
        /// распознавание списка векторов
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <param name="e">индекс последнего элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool S(string s, int b, int e)
        {

            //если пустая цепочка возвращаем истину
            if (b > e)
                return true;

            //иначе есть терминал А и В, ищем конец А
            int i = b;
            for (; i <= e && i < s.Length; i++)

                //А - вектор, его конец - 
                //закрывающаяся скобка
                if (s[i] == ')') break;

            //если это не вектор возвращаем ложь
            if (!A(s, b, i)) return false;

            //иначе возвращаем р-т проверки остальной части
            return B(s, i + 1, s.Length - 1);
        }

        /// <summary>
        /// распознавание следующего вектора
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <param name="e">индекс последнего элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool B(string s, int b, int e)
        {
            //если предыдущий вектор был последним
            //возвращаем истину
            if (b > e)
                return true;

            //первый знак должен быть запятой
            if (b > s.Length - 1 || s[b] != ',')
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидался символ ','");
                return false;
            }

            //ищем конец вектора
            int i = b + 1;
            for (; i <= e && i < s.Length; i++)
                if (s[i] == ')') break;

            //если это не вектор возвращаем ложь
            if (!A(s, b + 1, i)) return false;

            //иначе возвращаем р-т проверки остальной части
            return B(s, i + 1, s.Length - 1);
        }

        /// <summary>
        /// распознавание вектора
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <param name="e">индекс последнего элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool A(string s, int b, int e)
        {
            //вектор не может быть пустой цепочкой,
            //первый символ должен быть открывающей скобкой
            if (b > e || b > s.Length - 1 || s[b] != '(')
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидался символ '('");
                return false;
            }

            //от скобки до запятой первое целое число
            int i = b + 1;
            for (; i < s.Length && i <= e && s[i] != ','; i++) ;
            if (!Z(s, b + 1, i - 1)) return false;

            //от запятой до запятой второе число
            i++;
            b = i;
            for (; i < s.Length && i <= e && s[i] != ','; i++) ;
            if (!Z(s, b, i - 1)) return false;

            //от запятой до скобки последнее число
            i++;
            b = i;
            for (; i < s.Length && i <= e && s[i] != ')'; i++) ;
            if (!Z(s, b, i - 1)) return false;

            //если конец вектора правилен возвращаем истину
            if (i < s.Length && i == e && s[i] == ')')
                return true;
            //иначе сообщаем об ошибке в выражении
            else
            {
                Console.WriteLine("На позиции " + (i + 1) + " ожидался символ ')'");
                return false;
            }
        }

        /// <summary>
        /// распознавание целого числа
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <param name="e">индекс последнего элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool Z(string s, int b, int e)
        {
            //целое число не может быть пустой цепочкой
            if (b > e || b > s.Length - 1)
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидалась цифра от 0 до 9 или символ '-'");
                return false;
            }

            //если выражение состоит только из нуля
            //возвращаем истину
            if (s[b] == '0' && b == e)
                return true;
            //если первый знак минус
            //не включаем его в проверку N
            if (s[b] == '-')
                return N(s, b + 1, e);
            //иначе проверяем все выражение на N
            else
                return N(s, b, e);
        }

        /// <summary>
        /// распознавание натурального числа
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <param name="e">индекс последнего элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool N(string s, int b, int e)
        {
            //натуральное число не может быть пустой цепочкой
            if (b > e)
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидалась цифра от 1 до 9");
                return false;
            }
            //первый знак не должен быть нулем
            else if (s[b] >= '1' && s[b] <= '9')
                return D(s, b + 1, e);
            else
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидалась цифра от 1 до 9");
                return false;
            }
        }

        /// <summary>
        /// распознавание следующей цифры
        /// </summary>
        /// <param name="s">строка</param>
        /// <param name="b">индекс первого элемента выражения</param>
        /// <param name="e">индекс последнего элемента выражения</param>
        /// <returns>результат анализа</returns>
        static bool D(string s, int b, int e)
        {
            //если предыдущая цифра была последней
            //возвращаем истину
            if (b > e)
                return true;

            //следующий знак должен быть цифрой
            else if (s[b] >= '0' && s[b] <= '9')
                return D(s, b + 1, e);
            else
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидалась цифра от 0 до 9");
                return false;
            }
        }
    }
}

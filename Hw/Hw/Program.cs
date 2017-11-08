using System;
using System.Collections.Generic;
using System.Text;

namespace Hw
{
    class Program
    {
        static readonly string[] lexemes =
        {
            "NAME",
            "STRING",
            "INT",
            "bool",
            "string",
            "int",
            "static",
            "ref",
            "if",
            "else",
            "return",
            "Console.WriteLine",
            "true",
            "false",
            "{",
            "}",
            "(",
            ")",
            "[",
            "]",
            "\"",
            "\"",
            ".",
            ";",
            "=",
            ",",
            "&&",
            ">",
            ">=",
            "<=",
            "+",
            "*"
        };

        static void Main()
        {
            //чтение выражения

            string code = Console.ReadLine();
            while (true)
            {
                string str = Console.ReadLine();
                if (str == "STOP") break;
                code += "\n" + str;
            }

            StringBuilder con = new StringBuilder();
            bool b = Parametr(code, 0, code.Length - 1, con);
            Console.WriteLine(b);
            if (b)
                Console.WriteLine(con);

            //выход по нажатию кнопки
            Console.Read();
        }




        private static bool Value(string code, int b, int v, StringBuilder con)
        {
            return true;
        }

        /// <summary>
        /// проверка вызов
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool Call(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            //пропускаем пробелы в начале
            int i = b;
            for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

            //идем до разделителя/оператора, проверяем есть ли значение

            for (; i <= e &&
                !char.IsSeparator(code[i]) &&
                code[i] != '+' && code[i] != '*'; i++) ;


            //добавляем разделители в конце
            i++;
            for (; i <= end; i++)
                if (!char.IsSeparator(code[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// проверка параметра
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool Parametr(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            //пропускаем пробелы в начале
            int i = b;
            for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

            //проверяем две альтернативы
            StringBuilder sb1 = new StringBuilder(con.ToString());//арифметическое выражение

            bool b1 = false, b2 = false;
            b1 = ArithmeticExpression(code, i, e, sb1);
            if (b1)
            {
                con.Clear();
                con.Append(sb1);
                return true;
            }


            StringBuilder sb2 = new StringBuilder(con.ToString());//[ref] ИМЯ
            if (code.Substring(i, 3) != lexemes[7])
            {
                if (NAME(code, i, e, sb2))
                    b2 = true;
                else
                    b2 = false;
            }
            else
            {
                sb2.Append(", 8, ");
                i += 3;
                if (code[i] != ' ')
                    b2 = false;
                else
                {
                    b2 = NAME(code, i, e, sb2);
                }
            }
            if (b2)
            {
                con.Clear();
                con.Append(sb2);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// проверка арифметического выражения
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool ArithmeticExpression(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            //пропускаем пробелы в начале
            int i = b;
            for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

            //идем до разделителя/оператора, проверяем есть ли значение

            for (; i <= e &&
                !char.IsSeparator(code[i]) &&
                code[i] != '+' && code[i] != '*'; i++) ;

            if (!Value(code, b, i - 1, con))
                return false;

            //проверяем остальную часть выражения
            while (i <= e)
            {
                //опускаем пробелы
                for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;
                if (i > e) break;

                //два значения должен связывать оператор
                if (code[i] == '+')
                    con.Append(", 31");
                else if (code[i] == '*')
                    con.Append(", 32");
                else
                    return false;

                i++;
                //опускаем пробелы
                for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

                //ищем пределы значения
                b = i;
                for (; i <= e &&
                !char.IsSeparator(code[i]) &&
                code[i] != '+' && code[i] != '*'; i++) ;

                //проверяем значение
                if (!Value(code, b, i - 1, con))
                    return false;
            }

            //добавляем разделители в конце
            i++;
            for (; i <= end; i++)
                if (!char.IsSeparator(code[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// проверка возврата
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool Return(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            //пропускаем пробелы в начале
            int i = b;
            for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

            //идем до разделителя, проверяем есть ли "return"
            b = i;
            for (; i <= e &&
                !char.IsSeparator(code[i]) && code[i] != '\n'; i++) ;
            if (code.Substring(b, i - b) != lexemes[10])
                return false;
            con.Append("11");

            //идем до конца оператора
            b = i;
            for (; i <= e && code[i] != lexemes[23][0]; i++) ;
            //проверяем значение
            if (!Value(code, b, i - 1, con)) return false;
            con.Append(", 24");

            i++;
            for (; i <= end; i++)
                if (!char.IsSeparator(code[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// проверка вывода
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool Output(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            //пропускаем разделители в начале
            int i = b;
            for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

            //идем до круглых скобок или разделителя
            b = i;
            for (; i <= e && code[i] != lexemes[16][0] &&
                !char.IsSeparator(code[i]); i++) ;

            //проверяем равенство лексеме
            if (code.Substring(b, i - b) != lexemes[11])
                return false;
            con.Append("12");

            //если был разделитель(...Line     (...
            //идем до скобок
            for (; i <= e && code[i] != lexemes[16][0]; i++) ;

            con.Append(", 17");

            //идем до закрывающизхся скобок
            b = i + 1;
            for (i = b; i <= e && code[i] != lexemes[17][0]; i++) ;

            //проверяем выражение
            if (!ArithmeticExpression(code, b, i - 1, con))
                return false;

            //добавляем скобку
            con.Append(", 18");

            //пропускаем разделители
            i++;
            for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

            //следующий символ - конец оператора
            if (code[i] != lexemes[23][0])
                return false;
            con.Append(", 24");

            i++;
            for (; i <= end; i++)
                if (!char.IsSeparator(code[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// проверка присвоения
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool Assignment(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            //пропускаем пробелы в начале
            int i = b;
            for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

            //ищем границы имени(от начала до знака равно)
            for (; i <= e && code[i] != lexemes[24][0]; i++) ;

            //свертываем имя
            if (!NAME(code, b, i - 1, con))
                return false;

            //добавляем знак равно
            con.Append(", 25");

            //границы арифметического выражения(от равно до точки с запятой)
            b = i + 1;
            for (i = b; i <= e && code[i] != lexemes[23][0]; i++) ;

            //проверяем и добавляем точку с запятой
            if (!ArithmeticExpression(code, b, i - 1, con))
                return false;
            con.Append(", 24");

            //добавляем разделители в конце
            i++;
            for (; i <= end; i++)
                if (!char.IsSeparator(code[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// проверка имени
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool NAME(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            //опускаем пробелы слева и справа
            for (; b <= e &&
                char.IsSeparator(code[b]); b++)
                for (; e > 0 &&
                    char.IsSeparator(code[e]); e--) ;

            //первый знак
            if (!(code[b] == '_' || (code[b] >= 'a' && code[b] <= 'z') ||
                (code[b] >= 'A' && code[b] <= 'Z')))
                return false;
            b++;

            //следующие символы
            for (int i = b; i <= e; i++)
                if (!((code[i] >= 'a' && code[i] <= 'z') ||
                (code[i] >= 'A' && code[i] <= 'Z') || char.IsDigit(code[i])))
                    return false;

            //свертка
            con.Append("1." + code.Substring(b - 1, e - b + 2));
            for (int i = e + 1; i <= end; i++)
                if (!char.IsSeparator(code[i]))
                    return false;
            return true;
        }
    }
}

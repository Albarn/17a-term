using L4;
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

        /*
        static bool D(string s, int b, int e, ref int res)
        {
            //если предыдущая цифра была последней
            //возвращаем истину
            if (b > e)
                return true;

            //следующий знак должен быть цифрой
            else 
            if (s[b] >= '0' && s[b] <= '9')
            {
                res = res * 10 + int.Parse(s[b].ToString());
                return D(s, b + 1, e, ref res);
            }
            else
            {
                Console.WriteLine("На позиции " + (b + 1) + " ожидалась цифра от 0 до 9");
                return false;
            }
        }
        }*/

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
            bool b = If(code, 0, code.Length - 1, con);
            Console.WriteLine(b);
            if (b)
                Console.WriteLine(con);

            //выход по нажатию кнопки
            Console.Read();
        }

        /// <summary>
        /// проверка оператора
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="sb">строка для свертки</param>
        /// <returns></returns>
        static bool Operator(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            StringBuilder sb = new StringBuilder();
            if (If(code, b, e, sb))
            {
                con.Append(sb);
                return true;
            }
            sb.Clear();
            if (Return(code, b, e, sb))
            {
                con.Append(sb);
                return true;
            }
            sb.Clear();
            if (Output(code, b, e, sb))
            {
                con.Append(sb);
                return true;
            }
            sb.Clear();
            if (Assignment(code, b, e, sb))
            {
                con.Append(sb);
                return true;
            }
            return false;
        }

        static List<string> GetListOfOps(string[] ops)
        {
            List<string> lops = new List<string>();
            for (int k = 0; k < ops.Length; k++)
            {
                int count;
                if (!ops[k].StartsWith(lexemes[8]))//if
                {
                    //это обычный оператор, добавляем как есть
                    lops.Add(ops[k]);
                }
                else
                {
                    //это оператор если
                    //добавляем заголовок
                    lops.Add(ops[k]);

                    k++;
                    //это блок, добавляем все операторы до закрытия блока
                    int t = k;
                    count = -1;
                    while (t < ops.Length)
                    {
                        if (ops[t].StartsWith(lexemes[14]))  //{
                            count++;
                        if (ops[t].StartsWith(lexemes[15]))  //}
                            if (count != 0)
                                count--;
                            else
                                break;
                        t++;
                    }
                    if (t == ops.Length)
                        throw new InvalidOperationException();
                    for (; k <= t; k++)   //}
                        lops[lops.Count - 1] += "\n" + ops[k];
                    k--;

                    //проверяем, есть ли блок иначе
                    if (k + 1 < ops.Length && ops[k + 1].StartsWith(lexemes[9])) //else
                    {
                        k++;
                        //делаем то же самое, что и для если
                        //это оператор иначе
                        //добавляем заголовок
                        lops[lops.Count - 1] += "\n" + ops[k];

                        k++;
                        //это блок, добавляем все операторы до закрытия блока
                        t = k;
                        count = -1;
                        while (t < ops.Length)
                        {
                            if (ops[t].StartsWith(lexemes[14]))  //{
                                count++;
                            if (ops[t].StartsWith(lexemes[15]))  //}
                                if (count != 0)
                                    count--;
                                else
                                    break;
                            t++;
                        }
                        if (t == ops.Length)
                            throw new InvalidOperationException();
                        for (; k <= t; k++)   //}
                            lops[lops.Count - 1] += "\n" + ops[k];
                        k--;

                    }
                    lops[lops.Count - 1] += "\n";
                }
            }
            return lops;
        }

        /// <summary>
        /// проверка если
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool If(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            //пропускаем пробелы в начале
            int i = b;
            for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

            if (code.Length < i + 3 || code.Substring(i, 2) != lexemes[8]) //if
                return false;
            i += 2;
            con.Append("9, ");

            while (i <= e &&
                char.IsSeparator(code[i]))
                i++;

            if (code[i] != lexemes[16][0])    //(
                return false;
            i++;
            con.Append("17, ");

            b = i;
            while (i <= e &&
                code[i] != lexemes[17][0])    //)
                i++;
            if (code[i] != lexemes[17][0])
                return false;

            //проверяем логическое выражение между скобками
            if (!LogicalExpression(code, b, i - 1, con))
                return false;
            con.Append(", 18\n");

            i++;
            while (i <= e &&
                char.IsSeparator(code[i]))
                i++;

            if (code[i] != '\n')
                return false;
            i++;
            b = i;
            while (i <= e &&
                code[i] != '\n')
                i++;

            if (i <= e && code[i] != '\n')
                return false;
            i++;

            //проверяем часть if
            StringBuilder sb = new StringBuilder(con.ToString());
            int j = b;

            //ищем начало и конец блока
            if (code[j] != lexemes[14][0])  //{
                return false;
            con.Append("15\n");
            j++;
            while (j <= e &&
            char.IsSeparator(code[j]))
                j++;

            if (code[j] != '\n')
                return false;
            j++;

            int h = j;

            //счетчик внутренних скобок
            //т.е. если найдена еще одна открывающая скобка
            //счетчик увеличивается и одна закрывающая скобка
            //будет пропущена
            int count = 0;
            j = h;
            while (j <= e)  //}
            {
                j++;
                if (code[j] == lexemes[14][0])  //{
                    count++;
                if (code[j] == lexemes[15][0])  //}
                    if (count != 0)
                        count--;
                    else
                        break;
            }
            if (j > e || code[j] != lexemes[15][0] ||
                code[j - 1] != '\n')
                return false;
            j -= 2;

            //делим блок на операторы, если есть блоки if, 
            //объединяем их
            string[] ops = code.Substring(h, j + 1 - h).Split('\n');
            List<string> lops = new List<string>();
            try
            {
                lops = GetListOfOps(ops);
            }
            catch { return false; }

            foreach (string op in lops)
                if (!Operator(op, 0, op.Length - 1, con))
                    return false;

            con.Append("16\n");
            i = j + 3;



            while (i <= e &&
                char.IsSeparator(code[i]))
                i++;

            if (i <= e && code[i] != '\n')
                return false;
            i++;

            if (i <= e)
            {
                if (code.Length < i + 5 || code.Substring(i, 4) != lexemes[9]) //else
                    return false;
                i += 4;
                con.Append("10\n");
                while (i <= e &&
                char.IsSeparator(code[i]))
                    i++;

                if (code[i] != '\n')
                    return false;
                i++;
                b = i;
                while (i <= e &&
                    code[i] != '\n')
                    i++;

                if (code[i] != '\n')
                    return false;
                i++;

                sb = new StringBuilder(con.ToString());
                j = b;
                if (code[j] != lexemes[14][0])  //{
                    return false;
                con.Append("15\n");
                j++;
                while (j <= e &&
                char.IsSeparator(code[j]))
                    j++;

                if (code[j] != '\n')
                    return false;
                j++;

                h = j;
                count = 0;
                j = h;
                while (j <= e)  //}
                {
                    j++;
                    if (code[j] == lexemes[14][0])  //{
                        count++;
                    if (code[j] == lexemes[15][0])  //}
                        if (count != 0)
                            count--;
                        else
                            break;
                }
                if (j > e || code[j] != lexemes[15][0] ||
                    code[j - 1] != '\n')
                    return false;
                j -= 2;

                ops = code.Substring(h, j + 1 - h).Split('\n');
                try
                {
                    lops = GetListOfOps(ops);
                }
                catch { return false; }
                foreach (string op in lops)
                    if (!Operator(op, 0, op.Length - 1, con))
                        return false;

                con.Append("16\n");

                i = j + 3;
            }

            //добавляем разделители в конце
            //i++;
            //for (; i <= end; i++)
            //    if (!char.IsSeparator(code[i]) && code[i] != '\n')
            //        return false;

            return true;
        }

        /// <summary>
        /// проверка логического выражения
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool LogicalExpression(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            //пропускаем пробелы в начале
            int i = b;
            for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

            b = i;

            while (i <= e &&
                !char.IsSeparator(code[i]))
                i++;

            bool comp = true;
            int j = i;
            for (; j <= e; j++)
                if (code[j] == lexemes[26][0]) //&&
                {
                    comp = false;
                    break;
                }

            if (comp)
                if (!Comparison(code, b, j - 1, con))
                    return false;
                else { }
            else
            {
                if (!Comparison(code, b, j - 1, con))
                    return false;
                j++;
                if (code[j] != lexemes[26][1])
                    return false;

                con.Append(", 27, ");

                j++;
                while (j <= e &&
                    char.IsSeparator(code[j]))
                    j++;
                if (j != e)
                {
                    if (!LogicalExpression(code, j, e, con))
                        return false;
                }
            }

            //добавляем разделители в конце
            i = e + 1;
            for (; i <= end; i++)
                if (!char.IsSeparator(code[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// проверка сравнение
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool Comparison(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            //пропускаем пробелы в начале
            int i = b;
            for (; i <= e &&
                char.IsSeparator(code[i]); i++) ;

            b = i;
            while (i <= e &&
                code[i] != lexemes[27][0] &&    //>
                code[i] != lexemes[29][0])      //<=
                i++;

            //проверяем первое значение
            if (!Value(code, b, i - 1, con))
                return false;

            //пропускаем знак сравнения
            if(code[i]==lexemes[29][0])
            {
                i++;
                if (code[i] != lexemes[29][1])
                    return false;
                i++;
                con.Append(", 30, ");
            }
            else
            {
                i++;
                if (code[i] == lexemes[28][1])  //>=
                {
                    i++;
                    con.Append(", 29, ");
                }
                else
                    con.Append(", 28, ");
            }

            //проверяем второе значение
            if (!Value(code, i, e, con))
                return false;

            //добавляем разделители в конце
            i = e+1;
            for (; i <= end; i++)
                if (!char.IsSeparator(code[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// проверка значения
        /// </summary>
        /// <param name="code">код</param>
        /// <param name="b">начало</param>
        /// <param name="e">конец</param>
        /// <param name="con">строка для свертки</param>
        /// <returns></returns>
        static bool Value(string code, int b, int e, StringBuilder con)
        {
            int end = e;
            //проверка параметров
            if (e >= code.Length || b >= code.Length || b > e)
                return false;

            StringBuilder sb = new StringBuilder(con.ToString());

            //проверка ЦЕЛОЕ
            int h = b;
            int j = e;
            try
            {
                while (h <= e &&
                    char.IsSeparator(code[h]))
                    h++;
                while (j >= h &&
                    char.IsSeparator(code[j]))
                    j--;
                int a = int.Parse(code.Substring(h, j + 1 - h));
                sb.Append("3." + a);
                con.Clear();
                con.Append(sb);
                return true;
            }
            catch { }



            //проверка true | false
            sb.Clear();
            sb.Append(con.ToString());
            h = b;
            j = e;
            while (h <= e &&
                char.IsSeparator(code[h]))
                h++;
            while (j >= h &&
                char.IsSeparator(code[j]))
                j--;
            if (code.Substring(h, j + 1 - h) == lexemes[12])    //true
            {
                sb.Append("13");
                con.Clear();
                con.Append(sb);
                return true;
            }
            if (code.Substring(h, j + 1 - h) == lexemes[13])    //false
            {
                sb.Append("14");
                con.Clear();
                con.Append(sb);
                return true;
            }

            //проверка ИМЯ
            sb.Clear();
            sb.Append(con.ToString());
            if (NAME(code, b, e, sb))
            {
                con.Clear();
                con.Append(sb);
                return true;
            }

            //проверка ИМЯ “[” ЦЕЛОЕ “]”
            sb.Clear();
            sb.Append(con.ToString());
            h = b;
            j = h;
            while (j <= e &&
                code[j] != lexemes[18][0]) //[
                j++;
            if (NAME(code, h, j - 1, sb) && code[j] == lexemes[18][0])
            {
                sb.Append(", 19, ");
                h = j + 1;
                j = h;
                while (j <= e &&
                    code[j] != lexemes[19][0])  //]
                    j++;
                if (code[j] == lexemes[19][0])
                {
                    j--;
                    try
                    {
                        while (h <= e &&
                            char.IsSeparator(code[h]))
                            h++;
                        while (j >= h &&
                            char.IsSeparator(code[j]))
                            j--;
                        int a = int.Parse(code.Substring(h, j + 1 - h));
                        sb.Append("3." + a);
                        sb.Append(", 20");
                        con.Clear();
                        con.Append(sb);
                        return true;
                    }
                    catch { }
                }
            }

            //проверка "СТРОКА"
            sb.Clear();
            sb.Append(con.ToString());
            h = b;
            j = e;
            while (h <= e &&
                char.IsSeparator(code[h]))    
                h++;
            while (j >= b &&
                char.IsSeparator(code[j]))
                j--;
            if (j > h && code[h] == lexemes[20][0] &&   //"
                code[j] == lexemes[21][0])              //"
            {
                sb.Append("21, ");
                h++;j--;
                sb.Append("2." + code.Substring(h, j + 1 - h));
                sb.Append(", 22");
                con.Clear();
                con.Append(sb);
                return true;
            }

            //проверка вызов
            return Call(code, b, e, con);
        }

        /// <summary>
        /// проверка вызова
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

            //идем до разделителя/точки, проверяем есть ли значение

            bool single = false;
            for (; i <= e && code[i] != lexemes[22][0]      //.
                && code[i] != lexemes[16][0]; i++)          //(

                //между ИМЯ.ИМЯ не должно быть пробелов
                if (char.IsSeparator(code[i]))
                    single = true;

            //если указано имя класса
            if (!single)
            {
                //имя класса
                if (!NAME(code, b, i - 1, con))
                    return false;

                //добавляем точку и идем до открывающееся скобки
                con.Append(", 23, ");
                i++;
                b = i;
                if (char.IsSeparator(code[i]) || code[i] == lexemes[16][0])     //(
                    return false;

                while (!char.IsSeparator(code[i]) && code[i] != lexemes[16][0]) //(
                    i++;
            }

            //имя метода
            if (!NAME(code, b, i - 1, con))
                return false;

            while (char.IsSeparator(code[i]))
                i++;

            if (code[i] != lexemes[16][0])  //(
                return false;

            con.Append(", 17");

            i++;
            b = i;
            while (char.IsSeparator(code[i]))
                i++;

            //если в скобках есть параметры
            if (code[i] != lexemes[17][0])  //)
            {

                con.Append(", ");
                //добавляем первый
                while (code[i] != lexemes[25][0] && //,
                    code[i] != lexemes[17][0])      //)
                    i++;
                if (!Parametr(code, b, i - 1, con))
                    return false;

                if (code[i] == lexemes[25][0])      //,
                    con.Append(", 26");

                //добавляем остальные
                int c = 0;
                while (true)
                {
                    con.Append(", ");
                    c++;
                    if (c > 10000)
                        break;
                    while (char.IsSeparator(code[i]))
                        i++;
                    if (code[i] == lexemes[17][0])  //)
                        break;
                    else if (code[i] != lexemes[25][0])
                        return false;

                    i++;
                    b = i;
                    while (code[i] != lexemes[17][0] && //)
                        code[i] != lexemes[25][0])      //,
                        i++;

                    if (!Parametr(code, b, i - 1, con))
                        return false;

                    if (code[i] == lexemes[25][0])      //,
                        con.Append(", 26");
                    else
                        break;
                    i++;
                }
            }

            //закрывающаяся скобка
            con.Append(", 18");
            i++;

            //добавляем разделители в конце
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
                sb2.Append("8, ");
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
                    con.Append(", 31, ");
                else if (code[i] == '*')
                    con.Append(", 32, ");
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
            con.Append("11, ");

            //идем до конца оператора
            b = i;
            for (; i <= e && code[i] != lexemes[23][0]; i++) ;
            //проверяем значение
            if (!Value(code, b, i - 1, con)) return false;
            con.Append(", 24\n");

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
            while (b <= e &&
                char.IsSeparator(code[b]))
                b++;
            while (e >= b &&
                char.IsSeparator(code[e]))
                e--;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(S(Console.ReadLine()));
            Console.Read();
        }

        static bool S(string s)
        {
            if (s.Length == 0)
                return true;

            string l = "";
            int i = 0;
            for(;i<s.Length;i++)
            {
                l += s[i];
                if (s[i] == ')') break;
            }
            if (!A(l)) return false;

            l = "";
            i++;
            for (; i < s.Length; i++)
                l += s[i];

            return B(l);
        }

        static bool B(string s)
        {
            if (s.Length == 0)
                return true;
            if (s[0] != ',')
                return false;

            string l = "";
            int i = 1;
            for(;i<s.Length;i++)
            {
                l += s[i];
                if (s[i] == ')') break;
            }
            if (!A(l)) return false;

            l = "";
            i++;
            for (; i < s.Length; i++)
                l += s[i];

            return B(l);
        }

        static bool A(string s)
        {
            if (s.Length == 0)
                return false;
            if (s[0] != '(')
                return false;

            string l = "";
            int i = 1;
            for (; i < s.Length && s[i] != ','; i++)
                l += s[i];
            if (!Z(l)) return false;

            l = "";
            i++;
            for (; i < s.Length && s[i] != ','; i++)
                l += s[i];
            if (!Z(l)) return false;

            l = "";
            i++;
            for (; i < s.Length && s[i] != ')'; i++)
                l += s[i];
            if (!Z(l)) return false;

            if (i == s.Length - 1 && s[i] == ')')
                return true;
            else
                return false;
        }

        static bool Z(string s)
        {
            if (s.Length == 0)
                return false;
            if (s == "0")
                return true;
            if (s[0] == '-')
            {
                string l = "";
                for (int i = 1; i < s.Length; i++)
                    l += s[i];
                return N(l);
            }
            else
                return N(s);
        }

        static bool N(string s)
        {
            if (s.Length == 0)
                return false;
            else if (s[0] >= '1' && s[0] <= '9')
            {
                string l = "";
                for (int i = 1; i < s.Length; i++)
                    l += s[i];
                return D(l);
            }
            else
                return false;
        }

        static bool D(string s)
        {
            if (s.Length == 0)
                return true;
            else if (s[0] >= '0' && s[0] <= '9')
            {
                string l = "";
                for (int i = 1; i < s.Length; i++)
                    l += s[i];
                return D(l);
            }
            else
                return false;
        }
    }
}

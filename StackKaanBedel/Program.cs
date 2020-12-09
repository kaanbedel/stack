using System;
using System.Collections.Generic;
using System.Text;

namespace StackKaanBedel
{
    class Program
    {
        public static string expressionString = "";

        public static void Main(string[] args)
        {
            Console.WriteLine("Hesaplanacak işlemi yazınız.");
            expressionString = Console.ReadLine().Trim().Replace("\"", string.Empty);
            Console.WriteLine("İşlemin sonucu: " + Evaluate(expressionString));
            Console.Read();
        }

        public static int Evaluate(string expression)
        {
            char[] inputs = expression.ToCharArray();

            // Sayılar için stack
            Stack<int> numbers = new Stack<int>();
            // Operasyonlar için stack
            Stack<char> ops = new Stack<char>();

            for (int i = 0; i < inputs.Length; i++)
            {
                // Sayılar için Stack'i doldur
                if (inputs[i] >= '0' && inputs[i] <= '9')
                {
                    StringBuilder sbuf = new StringBuilder();
                    while (i < inputs.Length && inputs[i] >= '0' && inputs[i] <= '9')
                    {
                        sbuf.Append(inputs[i++]);
                    }
                    numbers.Push(int.Parse(sbuf.ToString()));
                    i--;
                }
                else if (inputs[i] == '(')
                {
                    ops.Push(inputs[i]);
                }
                else if (inputs[i] == ')')
                {
                    while (ops.Peek() != '(')
                    {
                        numbers.Push(Operate(ops.Pop(), numbers.Pop(), numbers.Pop()));
                    }
                    ops.Pop();
                }
                else if (inputs[i] == '+' || inputs[i] == '-' || inputs[i] == '*' || inputs[i] == '/' || inputs[i] == '%')
                {
                    while (ops.Count > 0 && HasPrecedence(inputs[i], ops.Peek()))
                    {
                        numbers.Push(Operate(ops.Pop(), numbers.Pop(), numbers.Pop()));
                    }
                    ops.Push(inputs[i]);
                }
            }
            while (ops.Count > 0)
            {
                numbers.Push(Operate(ops.Pop(), numbers.Pop(), numbers.Pop()));
            }
            return numbers.Pop();
        }
        //Öncelik belirle
        public static bool HasPrecedence(char op1, char op2)
        {
            if (op2 == '(' || op2 == ')')
            {
                return false;
            }
            if ((op1 == '*' || op1 == '/' || op1 == '%') && (op2 == '+' || op2 == '-'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //Dört işlemi gerçekleştir
        public static int Operate(char op, int b, int a)
        {
            switch (op)
            {
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '%': return a % b;
                case '/':
                    if (b == 0)
                    {
                        throw new NotSupportedException("Sıfıra Bölünemez");
                    }
                    return a / b;

            }
            return 0;
        }

    }
}

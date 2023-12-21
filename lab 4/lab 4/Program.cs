using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите выражение: ");
            string str = Console.ReadLine().Replace(" ",string.Empty);
            var prn = PRN(str);
            Console.Write("ОПЗ: ");
            Console.WriteLine(string.Join(" ", prn));
            Console.Write("Значение: ");
            Console.WriteLine(string.Join(" ", Result(prn)));
        }

        public static List<object> PRN(string str)
        {
            Dictionary<object, int> prioretyDictionary = new Dictionary<object, int>
            {
                {'+', 1}, {'-', 1}, {'*', 2}, {'/', 2}, {'(', 0}, {')', 5},
            };
            List<object> prn = new List<object>();
            Stack<object> stack = new Stack<object>();
            string num = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                if (prioretyDictionary.ContainsKey(str[i]))
                {
                    if (num != string.Empty) { prn.Add(num); num = string.Empty; }
                    if (str[i] == ')')
                    {
                        while ((Char)stack.Peek() != '(') { prn.Add(stack.Pop()); }
                        stack.Pop();
                    }
                    else if (stack.Count == 0 || str[i] == '(' || prioretyDictionary[str[i]] > prioretyDictionary[stack.Peek()]) { stack.Push(str[i]); }
                    else if (prioretyDictionary[str[i]] <= prioretyDictionary[stack.Peek()])
                    {
                        while (stack.Count > 0 && (Char)stack.Peek() != '(') { prn.Add(stack.Pop()); }
                        stack.Push(str[i]);
                    }
                }
                else { num += str[i]; }
            }
            prn.Add(num);
            while (stack.Count > 0) { prn.Add(stack.Pop()); }
            return prn;
        }

        public static Stack<double> Result(List<object> expression)
        {
            Stack<double> stack = new Stack<double>();
            for (var i =0; i < expression.Count; i++)
            {
                if (expression[i] is string) { stack.Push(Convert.ToDouble(expression[i])); }
                else { stack.Push(Calculate((Char)expression[i], stack.Pop(), stack.Pop())); }
            }
            return stack;
        }

        public static double Calculate(Char op, double first, double second)
        {
            switch (op)
            {
                case '*': return first * second;
                case '/': return first / second;
                case '+': return first + second;
                case '-': return first - second;
                default: return double.NaN;
            }
        }
    }
}
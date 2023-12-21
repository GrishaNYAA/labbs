using System;
using System.Collections.Generic;

namespace lab4
{
    class Token
    {
    }

    class Number : Token
    {
        public double Symbol;

        public Number(double num)
        {
            Symbol = num;
        }
    }

    class Operation : Token
    {
        public char Symbol;
        public int Priorety;

        public Operation(char symbol)
        {
            Symbol = symbol;
            Priorety = new Dictionary<char, int>
                { { '+', 1 }, { '-', 1 }, { '*', 2 }, { '/', 2 }, { '(', 0 }, { ')', 5 }, }[symbol];
        }
    }

    class Parenthesis : Token
    {
        public char Symbol;
        public bool IsClosing;

        public Parenthesis(char symbol)
        {
            if (symbol != '(' && symbol != ')') throw new ArgumentException("This is not valid bracket");
            IsClosing = symbol == ')';
            Symbol = symbol;
        }
    }

    class RPN
    {
        static void Main(string[] args)
        {
            string str = Console.ReadLine().Replace(" ", string.Empty);
            Console.WriteLine(string.Join(" ", Result(PRN(GetToken(str)))));
        }

        public static List<Token> GetToken(string str)
        {
            List<Token> tokens = new List<Token>();
            string num = string.Empty;
            foreach (char c in str)
            {
                if (char.IsDigit(c) || c == ',') num += c;
                else
                {
                    if (num != string.Empty)
                    {
                        tokens.Add(new Number(double.Parse(num)));
                        num = string.Empty;
                    }

                    tokens.Add(c == '+' || c == '-' || c == '*' || c == '/' ? new Operation(c) : new Parenthesis(c));
                }
            }

            if (num != string.Empty) tokens.Add(new Number(double.Parse(num)));
            return tokens;
        }

        public static List<Token> PRN(List<Token> tokens)
        {
            List<Token> prn = new List<Token>();
            Stack<Token> stack = new Stack<Token>();
            foreach (Token token in tokens)
            {
                if (token is Operation)
                {
                    while (stack.Count > 0 && !(token is Parenthesis) &&
                           ((Operation)token).Priorety <= ((Operation)stack.Peek()).Priorety) prn.Add(stack.Pop());
                    stack.Push(token);
                }
                else if (token is Parenthesis)
                {
                    if (((Parenthesis)token).IsClosing)
                    {
                        while (!(stack.Peek() is Parenthesis)) prn.Add(stack.Pop());
                        stack.Pop();
                    }
                    else stack.Push(token);
                }
                else if (token is Number) prn.Add(token);
            }

            while (stack.Count > 0) prn.Add(stack.Pop());
            return prn;
        }

        public static Stack<double> Result(List<Token> expression)
        {
            Stack<double> stack = new Stack<double>();
            foreach (Token token in expression)
            {
                if (token is Number number) stack.Push(number.Symbol);
                else stack.Push(Calculate(token, stack.Pop(), stack.Pop()));
            }
            return stack;
        }

        public static double Calculate(Token token, double operand2, double operand1)
        {
            if (token is Operation operation)
            {
                switch (operation.Symbol)
                {
                    case '+':
                        return operand1 + operand2;
                    case '-':
                        return operand1 - operand2;
                    case '*':
                        return operand1 * operand2;
                    case '/':
                        return operand1 / operand2;
                    default:
                        throw new ArgumentException("Invalid operation");
                }
            }
            else
            {
                throw new ArgumentException("Token is not an operation");
            }
        }
    }
}          
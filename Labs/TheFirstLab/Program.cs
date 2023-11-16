using System;
using System.Collections.Generic;

namespace  TheFirstLab
{
    class Program
    {
        static string Saving(List<int> numbers, string symbols)
        {
            if (!string.IsNullOrEmpty(symbols))
            {
                numbers.Add(item: int.Parse(symbols));
            }
            return string.Empty;
        }

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            input = input.Replace(" ", String.Empty);
            char[] suitableOps = new[] { '+', '-', '*', '/' };
            List<int> numbers = new List<int>();
            List<char> operators = new List<char>();
            string symbol = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    symbol += input[i];
                }
                else
                {
                    operators.Add(input[i]);
                    symbol = Saving(numbers, symbol);
                }
            }

            Saving(numbers, symbol);
            foreach (int number in numbers)
            {
                Console.Write($"{number},");
            }
            Console.WriteLine();

            foreach (char op in operators)
            {
                Console.Write($"{op},");
            }
            Console.WriteLine(); 
            int Conclusiion = MathematicalOperations(numbers, operators);
            Console.WriteLine(Conclusiion);
        }
        static int MathematicalOperations(List<int> numbers, List<char> operators)
        {
            for (int i = 0; i < operators.Count;)
            {
                if (operators[i] == '*' || operators[i] == '/')
                {
                    int leftIndex = numbers[i];
                    int rightIndex = numbers[i + 1];
                    int result = operators[i] == '*' ? leftIndex * rightIndex : leftIndex / rightIndex;
                    numbers[i] = result;
                    numbers.RemoveAt(i + 1);
                    operators.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            
            }
            
            for (int i = 0; i < operators.Count;)
            {
                if (operators[i] == '+' || operators[i] == '-')
                {
                    int leftIndex = numbers[i];
                    int rightIndex = numbers[i + 1];
                    int result = operators[i] == '+' ? leftIndex + rightIndex : leftIndex - rightIndex;
                    numbers[i] = result;
                    numbers.RemoveAt(i + 1);
                    operators.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            int res = numbers.Sum();
            return res;
        }
    }
}   
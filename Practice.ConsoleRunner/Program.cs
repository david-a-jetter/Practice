using Practice.FizzBuzz;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Practice.ConsoleRunner
{
    public static class Program
    {
        private static Random _Random { get; }

        public static async Task Main(string[] args)
        {
            var input = Console.ReadLine();

            while(! string.IsNullOrWhiteSpace(input))
            {
                var o = 0;
                var isPalindrome = true;
                var checks = Convert.ToInt32(input.Length / 2);

                for (int i = input.Length - 1; o < checks; i--)
                {
                    var reversedChar = input[i];
                    var inputChar = input[o];

                    if (reversedChar != inputChar)
                    {
                        isPalindrome = false;
                        break;
                    }

                    o++;
                }

                Console.WriteLine($"Input was: {input}");

                if (isPalindrome)
                {
                    Console.WriteLine("These are palindromes!");
                }
                else
                {
                    Console.WriteLine($"This is not a palindrome. Completed {o + 1} checks");
                }

                Console.WriteLine();
                Console.Write("Enter next word: ");
                input = Console.ReadLine();
            }
        }
    }
}

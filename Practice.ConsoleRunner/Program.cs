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
        public static async Task Main(string[] args)
        {
            var input = Console.ReadLine();

            while(! string.IsNullOrWhiteSpace(input))
            {
                var isPalindrome = true;
                var length = input.Length;
                var checks = length / 2;

                var lastIndex = 0;

                for (var i = 0; i < checks; i++)
                {
                    lastIndex = i;

                    var x = (length - i) - 1;
                    var frontChar = input[i];
                    var backChar = input[x];

                    if (frontChar != backChar)
                    {
                        isPalindrome = false;
                        break;
                    }
                }

                int removedCharIndex = 0;

                if (! isPalindrome)
                {
                    for (var t = lastIndex; t < checks; t++)
                    {
                        var newAttempt = input.Remove(t, 1);

                        lastIndex = t;

                        var x = (length - t) - 2;
                        var frontChar = input[t];
                        var backChar = input[x];

                        if (frontChar != backChar)
                        {
                            break;
                        }

                        removedCharIndex = t;

                        isPalindrome = true;
                    }
                }

                Console.WriteLine($"Input was: {input}");

                if (isPalindrome)
                {
                    Console.WriteLine($"These are palindromes! Completed {lastIndex + 1} checks");
                    
                    if (removedCharIndex > 0)
                    {
                        var removedChar = input[removedCharIndex];
                        Console.WriteLine($"Had to remove character '{removedChar}' at index '{removedCharIndex}'");
                    }
                }
                else
                {
                    Console.WriteLine($"This is not a palindrome. Completed {lastIndex + 1} checks");
                }

                Console.WriteLine();
                Console.Write("Enter next word: ");
                input = Console.ReadLine();
            }
        }
    }
}

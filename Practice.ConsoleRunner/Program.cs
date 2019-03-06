using Practice.FizzBuzz;
using System;

namespace Practice.ConsoleRunner
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var runner = new FizzBuzzRunner(
                new FizzBuzzEvaluator(),
                Console.WriteLine);

            runner.Run(1, 100);

            Console.ReadKey();
        }
    }
}

using System;

namespace Practice.FizzBuzz
{
    public class FizzBuzzOutput
    {
        int    Input  { get; }
        string Output { get; }

        public FizzBuzzOutput(int input, string output)
        {
            Input  = input;
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }
    }
}

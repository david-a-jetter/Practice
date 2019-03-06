using System;

namespace Practice.FizzBuzz
{
    public class OutputRule
    {
        public string          Output { get; }
        public Func<int, bool> Rule   { get; }

        public OutputRule(string output, Func<int, bool> ruleFunc)
        {
            Output = output   ?? throw new ArgumentNullException(nameof(output));
            Rule   = ruleFunc ?? throw new ArgumentNullException(nameof(ruleFunc));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.FizzBuzz
{
    public class FizzBuzzEvaluator : IFizzBuzzEvaluator
    {
        private IEnumerable<OutputRule> _Rules { get; }

        private string _Suffix { get; }

        public FizzBuzzEvaluator() : this(
            rules: new OutputRule[]
            {
                new OutputRule("Fizz", new Func<int, bool>(input => input % 3 == 0)),
                new OutputRule("Buzz", new Func<int, bool>(input => input % 5 == 0))
            },
            suffix: "!") { }

        public FizzBuzzEvaluator(IEnumerable<OutputRule> rules, string suffix)
        {
            _Rules  = rules  ?? throw new ArgumentNullException(nameof(rules));
            _Suffix = suffix ?? throw new ArgumentNullException(nameof(suffix));
        }

        public FizzBuzzOutput Evaluate(int input)
        {
            var anyMatches = false;

            var outputSb = new StringBuilder();

            foreach(var rule in _Rules)
            {
                if (rule.Rule(input))
                {
                    anyMatches = true;
                    outputSb.Append(rule.Output);
                }
            }

            if (! anyMatches)
            {
                outputSb.Append(input);
            }
            else
            {
                outputSb.Append(_Suffix);
            }

            var output = new FizzBuzzOutput(input, outputSb.ToString());

            return output;
        }
    }
}

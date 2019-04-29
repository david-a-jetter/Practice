using System;
using System.Collections.Generic;

namespace Practice.FizzBuzz
{
    public class FizzBuzzRunner
    {
        private IFizzBuzzEvaluator _Evaluator    { get; }

        public FizzBuzzRunner(IFizzBuzzEvaluator evaluator)
        {
            _Evaluator    = evaluator    ?? throw new ArgumentNullException(nameof(evaluator));
        }

        public IEnumerable<FizzBuzzOutput> Run(int start, int end)
        {
            if (end < start) throw new ArgumentException($"{nameof(end)} must be greater than or equal to {nameof(start)}");

            var fizzBuzzOutputs = new LinkedList<FizzBuzzOutput>();

            for(int i = start; i <= end; i++)
            {
                var evalResult = _Evaluator.Evaluate(i);

                fizzBuzzOutputs.AddLast(evalResult);
            }

            return fizzBuzzOutputs;
        }
    }
}

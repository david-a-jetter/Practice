using System;

namespace Practice.FizzBuzz
{
    public class FizzBuzzRunner
    {
        private IFizzBuzzEvaluator _Evaluator    { get; }
        private Action<string>     _OutputAction { get; }

        public FizzBuzzRunner(IFizzBuzzEvaluator evaluator, Action<string> outputAction)
        {
            _Evaluator    = evaluator    ?? throw new ArgumentNullException(nameof(evaluator));
            _OutputAction = outputAction ?? throw new ArgumentNullException(nameof(outputAction));
        }

        public void Run(int start, int end)
        {
            if (end < start) throw new ArgumentException($"{nameof(end)} must be greater than or equal to {nameof(start)}");

            for(int i = start; i <= end; i++)
            {
                var evalResult = _Evaluator.Evaluate(i);
                _OutputAction($"{i}: {evalResult}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.FizzBuzz
{
    public interface IFizzBuzzEvaluator
    {
        FizzBuzzOutput Evaluate(int input);
    }
}

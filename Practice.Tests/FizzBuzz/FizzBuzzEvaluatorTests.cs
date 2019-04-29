using System;
using Xunit;
using Practice.FizzBuzz;
using FluentAssertions;
using System.Collections.Generic;

namespace Practice.Tests.FizzBuzz
{
    public class FizzBuzzEvaluatorTests
    {
        [Fact]
        public void WhenRulesIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => new FizzBuzzEvaluator(null, "!"));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*rules*");
        }

        [Fact]
        public void WhenDefaultCtorAndInputIs3_ThenFizzIsReturned()
        {
            var fizzBuzz = new FizzBuzzEvaluator();

            var response = fizzBuzz.Evaluate(3);

            response.Should().Be("Fizz!");
        }

        [Fact]
        public void WhenDefaultCtorAndInputIs5_ThenBuzzIsReturned()
        {
            var fizzBuzz = new FizzBuzzEvaluator();

            var response = fizzBuzz.Evaluate(5);

            response.Should().Be("Buzz!");
        }

        [Fact]
        public void WhenDefaultCtorAndInputIs2_Then2IsReturned()
        {
            var fizzBuzz = new FizzBuzzEvaluator();

            var response = fizzBuzz.Evaluate(2);

            response.Should().Be("2");

        }

        [Fact]
        public void WhenDefaultCtorAndInputIsDivisibleBy3And5_ThenFizzBuzzIsReturned()
        {
            var fizzBuzz = new FizzBuzzEvaluator();

            var response = fizzBuzz.Evaluate(15);

            response.Should().Be("FizzBuzz!");
        }

        [Fact]
        public void When1Through100IsInput_ThenExpectedOutputIsReturned()
        {
            var fizzBuzz = new FizzBuzzEvaluator();
            var outputList = new List<string>();

            for (int i = 1; i <= 100; i++)
            {
                var output = fizzBuzz.Evaluate(i);
                outputList.Add(output);
            }

            outputList.Should().BeEquivalentTo(_ExpectedOutput100);
        }

        private IEnumerable<string> _ExpectedOutput100 = new string[]
        {
            "1",
            "2",
            "Fizz!",
            "4",
            "Buzz!",
            "Fizz!",
            "7",
            "8",
            "Fizz!",
            "Buzz!",
            "11",
            "Fizz!",
            "13",
            "14",
            "FizzBuzz!",
            "16",
            "17",
            "Fizz!",
            "19",
            "Buzz!",
            "Fizz!",
            "22",
            "23",
            "Fizz!",
            "Buzz!",
            "26",
            "Fizz!",
            "28",
            "29",
            "FizzBuzz!",
            "31",
            "32",
            "Fizz!",
            "34",
            "Buzz!",
            "Fizz!",
            "37",
            "38",
            "Fizz!",
            "Buzz!",
            "41",
            "Fizz!",
            "43",
            "44",
            "FizzBuzz!",
            "46",
            "47",
            "Fizz!",
            "49",
            "Buzz!",
            "Fizz!",
            "52",
            "53",
            "Fizz!",
            "Buzz!",
            "56",
            "Fizz!",
            "58",
            "59",
            "FizzBuzz!",
            "61",
            "62",
            "Fizz!",
            "64",
            "Buzz!",
            "Fizz!",
            "67",
            "68",
            "Fizz!",
            "Buzz!",
            "71",
            "Fizz!",
            "73",
            "74",
            "FizzBuzz!",
            "76",
            "77",
            "Fizz!",
            "79",
            "Buzz!",
            "Fizz!",
            "82",
            "83",
            "Fizz!",
            "Buzz!",
            "86",
            "Fizz!",
            "88",
            "89",
            "FizzBuzz!",
            "91",
            "92",
            "Fizz!",
            "94",
            "Buzz!",
            "Fizz!",
            "97",
            "98",
            "Fizz!",
            "Buzz!"
        };
    }
}

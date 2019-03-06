using FluentAssertions;
using Practice.FizzBuzz;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Practice.Tests.FizzBuzz
{
    public class OutputRuleTests
    {
        [Fact]
        public void WhenOutputIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() =>
                new OutputRule(null, new Func<int, bool>(_ => true)));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*output*");
        }

        [Fact]
        public void WhenRuleIsNull_ThenCtorThrows()
        {
            var ctorAction = new Action(() => new OutputRule("output", null));

            ctorAction.Should().Throw<ArgumentNullException>().WithMessage("*rule*");
        }

        [Fact]
        public void WhenRuleIsConstructed_ThenPropertiesAreExpected()
        {
            var output = "OUTPUT";
            var rule = new Func<int, bool>(_ => true);

            var outputRule = new OutputRule(output, rule);

            outputRule.Output.Should().Be(output);
            outputRule.Rule.Should().BeSameAs(rule);
        }
    }
}

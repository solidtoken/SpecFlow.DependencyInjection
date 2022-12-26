using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using SolidToken.SpecFlow.DependencyInjection.Tests.Support;
using TechTalk.SpecFlow;

namespace SolidToken.SpecFlow.DependencyInjection.Tests.Steps
{
    [Binding]
    public class CalculatorSteps
    {
        private readonly ICalculator _calculator;

        public CalculatorSteps(ICalculator calculator)
        {
            _calculator = calculator;
        }

        [Given(@"I have entered (.*) into the Calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int operand)
        {
            _calculator.Enter(operand);
        }

        [Then(@"the Result should be (.*)")]
        public void ThenTheResultShouldBe(int expected)
        {
            _calculator.Result.Should().Be(expected);
        }

        [Then(@"the Size should be (.*)")]
        public void ThenTheSizeShouldBe(int expected)
        {
            _calculator.Size.Should().Be(expected);
        }
    }

    [Binding]
    public class CalculatorOperatorSteps
    {
        private readonly ICalculator _calculator;

        public CalculatorOperatorSteps(ICalculator calculator)
        {
            _calculator = calculator;
        }

        [When(@"I press Add")]
        public void WhenIPressAdd()
        {
            _calculator.Add();
        }

        [When(@"I press Multiply")]
        public void WhenIPressMultiply()
        {
            _calculator.Multiply();
        }
    }
}

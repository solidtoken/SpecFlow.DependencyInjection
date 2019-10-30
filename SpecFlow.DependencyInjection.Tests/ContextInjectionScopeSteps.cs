using System;
using System.Diagnostics;
using TechTalk.SpecFlow;
using Xunit;

namespace SolidToken.SpecFlow.DependencyInjection.Tests
{
    public class TestContext
    {
        public int Number { get; set; }
    }

    [Binding]
    public class ContextInjectionScopeSteps
    {
        private readonly TestContext _context;

        public ContextInjectionScopeSteps(TestContext context)
        {
            _context = context;
        }

        [Given(@"I have a test context")]
        public void GivenIHaveATestContext()
        {
            // NOOP
        }

        [Given(@"I have test context with number (.*)")]
        public void GivenIHaveTestContextWithNumber(int number)
        {
            _context.Number = number;
        }

        [Then(@"the test context number should be (.*)")]
        public void ThenTheTestContextNumberShouldBe(int expected)
        {
            Assert.Equal(expected, _context.Number);
        }
    }

    [Binding]
    public class MultiplySteps
    {
        private readonly TestContext _context;

        public MultiplySteps(TestContext context)
        {
            _context = context;
        }

        [When(@"I multiply the test context number by (.*)")]
        public void WhenIMultiplyTheTestContextNumberBy(int multiply)
        {
            _context.Number *= multiply;
        }
    }

    [Binding]
    public class IncreaseSteps
    {
        private readonly TestContext _context;

        public IncreaseSteps(TestContext context)
        {
            _context = context;
        }

        [When(@"I increase the test context number by (.*)")]
        public void WhenIIncreaseTheTestContextNumberBy(int increase)
        {
            _context.Number += increase;
        }
    }
}

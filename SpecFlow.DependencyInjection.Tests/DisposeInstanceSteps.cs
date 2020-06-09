using System;
using TechTalk.SpecFlow;
using Xunit;

namespace SolidToken.SpecFlow.DependencyInjection.Tests
{
    [Binding]
    public class DisposeInstanceSteps : IDisposable
    {
        private static int DisposeCallCount { get; set; }
        private static int InstanciationCount { get; set; }

        public DisposeInstanceSteps()
        {
            InstanciationCount++;
        }

        [Then(@"Dispose should have been called the right number of times")]
        public void ThenDisposeShouldHaveBeenCalledTimes()
        {
            var expected = InstanciationCount - 1;
            Assert.Equal(expected, DisposeCallCount);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeCallCount++;
        }
    }
}

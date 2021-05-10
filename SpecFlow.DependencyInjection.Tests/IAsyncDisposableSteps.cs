using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace SolidToken.SpecFlow.DependencyInjection.Tests
{
    [Binding]
    public class IAsyncDisposableSteps
    {
        private readonly IAsyncDisposable _asyncDisposableService;

        public IAsyncDisposableSteps(IAsyncDisposable asyncDisposableService)
        {
            _asyncDisposableService = asyncDisposableService;
        }

        [Then(@"verify that AsyncDisposableClass is correctly registered")]
        public void ThenVerifyThatAsyncDisposableClassIsCorrectlyRegistered()
        {
            Assert.NotNull(_asyncDisposableService);
        }
    }

    public sealed class AsyncDisposableService : IAsyncDisposable
    {
        public async ValueTask DisposeAsync()
        {
            // NOOP
        }
    }
}

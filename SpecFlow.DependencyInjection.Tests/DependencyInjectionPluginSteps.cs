using TechTalk.SpecFlow;
using Xunit;

namespace SolidToken.SpecFlow.DependencyInjection.Tests
{
    [Binding]
    public class DependencyInjectionPluginSteps
    {
        private readonly ITestService _testService;

        public DependencyInjectionPluginSteps(ITestService testService)
        {
            _testService = testService;
        }

        [Then(@"verify that TestService is correctly injected")]
        public void ThenVerifyThatTestServiceIsCorrectlyInjected()
        {
            Assert.True(_testService.Verify());
        }
    }

    public interface ITestService
    {
        bool Verify();
    }

    public class TestService : ITestService
    {
        public bool Verify()
        {
            return true;
        }
    }
}

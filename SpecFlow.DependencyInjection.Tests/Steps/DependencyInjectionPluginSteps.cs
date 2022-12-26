using TechTalk.SpecFlow;
using Xunit;

namespace SolidToken.SpecFlow.DependencyInjection.Tests.Steps
{
    [Binding]
    public class DependencyInjectionPluginSteps
    {
        private readonly ITestService testService;

        public DependencyInjectionPluginSteps(ITestService testService)
        {
            this.testService = testService;
        }

        [Then(@"verify that TestService is correctly injected")]
        public void ThenVerifyThatTestServiceIsCorrectlyInjected()
        {
            Assert.True(testService.Verify());
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

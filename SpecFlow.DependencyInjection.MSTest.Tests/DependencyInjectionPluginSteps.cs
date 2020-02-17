using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace SpecFlow.DependencyInjection.MSTest.Tests
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
            Assert.IsTrue(testService.Verify());
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

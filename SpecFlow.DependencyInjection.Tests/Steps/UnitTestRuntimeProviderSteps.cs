using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.UnitTestProvider;
using Xunit;

namespace SolidToken.SpecFlow.DependencyInjection.Tests.Steps
{
    [Binding]
    public class UnitTestRuntimeProviderSteps
    {
        private readonly IUnitTestRuntimeProvider _unitTestRuntimeProvider;

        public UnitTestRuntimeProviderSteps(IUnitTestRuntimeProvider unitTestRuntimeProvider)
        {
            _unitTestRuntimeProvider = unitTestRuntimeProvider;
        }

        [Then(@"verify that IUnitTestRuntimeProvider is correctly injected")]
        public void ThenVerifyThatIUnitTestRuntimeProviderIsCorrectlyInjected()
        {
            Assert.NotNull(_unitTestRuntimeProvider);
        }
    }
}

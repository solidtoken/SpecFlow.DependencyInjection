using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;

namespace SolidToken.SpecFlow.DependencyInjection.Tests
{
    [Binding]
    public class ITestOutputHelperSteps
    {
        private readonly ITestOutputHelper _output;

        public ITestOutputHelperSteps(ITestOutputHelper output)
        {
            _output = output;
        }

        [When(@"a message is output using ITestOutputHelper")]
        public void WhenAMessageIsOutputUsingITestOutputHelper()
        {
            _output.WriteLine("This is output from ITestOutputHelper");
        }

        [Then(@"verify that ITestOutputHelper is correctly injected")]
        public void ThenVerifyThatITestOutputHelperIsCorrectlyInjected()
        {
            Assert.NotNull(_output);
        }
    }
}

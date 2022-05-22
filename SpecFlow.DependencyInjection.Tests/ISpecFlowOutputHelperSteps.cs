using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using Xunit;

namespace SolidToken.SpecFlow.DependencyInjection.Tests
{
    [Binding]
    public class ISpecFlowOutputHelperSteps
    {
        private readonly ISpecFlowOutputHelper _output;

        public ISpecFlowOutputHelperSteps(ISpecFlowOutputHelper output)
        {
            _output = output;
        }

        [When(@"a message is output using ISpecFlowOutputHelper")]
        public void WhenAMessageIsOutputUsingISpecFlowOutputHelper()
        {
            _output.WriteLine("This is output from ISpecFlowOutputHelper");
        }

        [Then(@"verify that ISpecFlowOutputHelper is correctly injected")]
        public void ThenVerifyThatISpecFlowOutputHelperIsCorrectlyInjected()
        {
            Assert.NotNull(_output);
        }
    }
}

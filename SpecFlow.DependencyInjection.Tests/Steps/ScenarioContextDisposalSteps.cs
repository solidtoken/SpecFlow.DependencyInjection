using TechTalk.SpecFlow;

namespace SolidToken.SpecFlow.DependencyInjection.Tests.Steps
{
    [Binding]
    public class ScenarioContextDisposalSteps
    {
        private readonly ScenarioContext _context;

        public ScenarioContextDisposalSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Given(@"I have scenario context with number (.*)")]
        public void GivenIHaveScenarioContextWithNumber(int number)
        {
            _context["number"] = number;
            //or
            //_context.Set(number);
        }
    }
}

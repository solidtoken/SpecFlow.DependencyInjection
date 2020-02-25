using System;
using TechTalk.SpecFlow;

namespace SolidToken.SpecFlow.DependencyInjection
{
    [Serializable]
    public class MissingScenarioDependenciesException : SpecFlowException
    {
        public MissingScenarioDependenciesException()
            : base("No method marked with [ScenarioDependencies] attribute found.")
        {
            HelpLink = @"https://github.com/solidtoken/SpecFlow.DependencyInjection#usage";
        }
    }
}

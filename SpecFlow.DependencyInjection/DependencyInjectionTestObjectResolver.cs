using System;
using BoDi;
using TechTalk.SpecFlow.Infrastructure;

namespace SolidToken.SpecFlow.DependencyInjection
{
    public class DependencyInjectionTestObjectResolver : ITestObjectResolver
    {
        public object ResolveBindingInstance(Type bindingType, IObjectContainer scenarioContainer)
        {
            var provider = scenarioContainer.Resolve<IServiceProvider>();
            return provider.GetService(bindingType);
        }
    }
}

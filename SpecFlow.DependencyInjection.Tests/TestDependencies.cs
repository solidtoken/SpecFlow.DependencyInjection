using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace SolidToken.SpecFlow.DependencyInjection.Tests
{
    public static class TestDependencies
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateServices()
        {
            var services = new ServiceCollection();

            // Add test dependencies
            services.AddTransient<ITestService, TestService>();

            // ContextInjectionScope (by using AddScoped instead of AddTransient, the context will be scoped to the Feature across bindings)
            services.AddScoped<TestContext>();

            // NOTE: This line is essential so that Microsoft.Extensions.DependencyInjection knows
            // about the SpecFlow bindings (something normally BoDi does automatically).
            // TODO: Find out if we can make this part of the Plugin
            foreach (var type in typeof(TestDependencies).Assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))))
            {
                services.AddSingleton(type);
            }

            return services;
        }
    }
}

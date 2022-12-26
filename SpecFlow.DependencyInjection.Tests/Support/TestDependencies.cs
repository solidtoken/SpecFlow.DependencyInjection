using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection.Tests.Steps;

namespace SolidToken.SpecFlow.DependencyInjection.Tests.Support
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

            return services;
        }
    }
}

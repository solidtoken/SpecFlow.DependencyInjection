using Microsoft.Extensions.DependencyInjection;

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

            return services;
        }
    }
}

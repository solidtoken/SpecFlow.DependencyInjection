using System;
using BoDi;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(SolidToken.SpecFlow.DependencyInjection.DependencyInjectionPlugin))]

namespace SolidToken.SpecFlow.DependencyInjection
{
    public class DependencyInjectionPlugin : IRuntimePlugin
    {
        private readonly object registrationLock = new object();

        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeGlobalDependencies += (sender, args) =>
            {
                if (!args.ObjectContainer.IsRegistered<IServiceCollectionFinder>())
                {
                    lock (registrationLock)
                    {
                        if (!args.ObjectContainer.IsRegistered<IServiceCollectionFinder>())
                        {
                            args.ObjectContainer.RegisterTypeAs<DependencyInjectionTestObjectResolver, ITestObjectResolver>();
                            args.ObjectContainer.RegisterTypeAs<ServiceCollectionFinder, IServiceCollectionFinder>();
                        }
                    }
                    args.ObjectContainer.Resolve<IServiceCollectionFinder>();
                }
            };

            runtimePluginEvents.CustomizeScenarioDependencies += (sender, args) =>
            {
                args.ObjectContainer.RegisterFactoryAs<IServiceProvider>(() =>
                {
                    var serviceCollectionFinder = args.ObjectContainer.Resolve<IServiceCollectionFinder>();
                    var createScenarioServiceCollection = serviceCollectionFinder.GetCreateScenarioServiceCollection();
                    var services = createScenarioServiceCollection();
                    RegisterSpecFlowDependencies(args.ObjectContainer, services);
                    return services.BuildServiceProvider();
                });
            };
        }

        private void RegisterSpecFlowDependencies(
            IObjectContainer objectContainer,
            IServiceCollection services)
        {
            services.AddTransient<IObjectContainer>(ctx =>
            {
                return objectContainer;
            });
            services.AddTransient<ScenarioContext>(ctx =>
            {
                var specflowContainer = ctx.GetService<IObjectContainer>();
                var scenarioContext = specflowContainer.Resolve<ScenarioContext>();
                return scenarioContext;
            });
            services.AddTransient<FeatureContext>(ctx =>
            {
                var specflowContainer = ctx.GetService<IObjectContainer>();
                var scenarioContext = specflowContainer.Resolve<FeatureContext>();
                return scenarioContext;
            });
            services.AddTransient<TestThreadContext>(ctx =>
            {
                var specflowContainer = ctx.GetService<IObjectContainer>();
                var scenarioContext = specflowContainer.Resolve<TestThreadContext>();
                return scenarioContext;
            });
        }
    }
}

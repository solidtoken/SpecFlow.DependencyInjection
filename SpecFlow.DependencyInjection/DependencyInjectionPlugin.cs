﻿using BoDi;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Concurrent;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;
using TechTalk.SpecFlow.Bindings.Discovery;
using TechTalk.SpecFlow.BindingSkeletons;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.ErrorHandling;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.Tracing;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: RuntimePlugin(typeof(SolidToken.SpecFlow.DependencyInjection.DependencyInjectionPlugin))]

namespace SolidToken.SpecFlow.DependencyInjection
{
    public class DependencyInjectionPlugin : IRuntimePlugin
    {
        private static readonly ConcurrentDictionary<IServiceProvider, IContextManager> BindMappings =
            new ConcurrentDictionary<IServiceProvider, IContextManager>();

        private static readonly ConcurrentDictionary<ISpecFlowContext, IServiceScope> ActiveServiceScopes =
            new ConcurrentDictionary<ISpecFlowContext, IServiceScope>();

        private readonly object _registrationLock = new object();

        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters, UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            runtimePluginEvents.CustomizeGlobalDependencies += CustomizeGlobalDependencies;
            runtimePluginEvents.CustomizeFeatureDependencies += CustomizeFeatureDependenciesEventHandler;
            runtimePluginEvents.CustomizeScenarioDependencies += CustomizeScenarioDependenciesEventHandler;
        }

        private void CustomizeGlobalDependencies(object sender, CustomizeGlobalDependenciesEventArgs args)
        {
            if (!args.ObjectContainer.IsRegistered<IServiceCollectionFinder>())
            {
                lock (_registrationLock)
                {
                    if (!args.ObjectContainer.IsRegistered<IServiceCollectionFinder>())
                    {
                        args.ObjectContainer.RegisterTypeAs<DependencyInjectionTestObjectResolver, ITestObjectResolver>();
                        args.ObjectContainer.RegisterTypeAs<ServiceCollectionFinder, IServiceCollectionFinder>();
                    }

                    // We store the (MS) service provider in the global (BoDi) container, we create it only once.
                    // It must be lazy (hence factory) because at this point we still don't have the bindings mapped.
                    args.ObjectContainer.RegisterFactoryAs<RootServiceProviderContainer>(() =>
                    {
                        var serviceCollectionFinder = args.ObjectContainer.Resolve<IServiceCollectionFinder>();
                        var (services, scoping) = serviceCollectionFinder.GetServiceCollection();

                        // we need the concrete type here, to be able to mark it as readonly later
                        var concreteServices = new ServiceCollection();
                        foreach (var x in services)
                        {
                            concreteServices.Add(x);
                        }
                        RegisterProxyBindings(args.ObjectContainer, concreteServices);
                        args.ObjectContainer.RegisterFactoryAs<IServiceCollection>(_ => concreteServices);

                        return new RootServiceProviderContainer(concreteServices, scoping);
                    });

                    args.ObjectContainer.RegisterFactoryAs<IServiceProvider>(() =>
                    {
                        return args.ObjectContainer.Resolve<RootServiceProviderContainer>().ServiceProvider;
                    });

                    // Will make sure DI scope is disposed.
                    var lcEvents = args.ObjectContainer.Resolve<RuntimePluginTestExecutionLifecycleEvents>();
                    lcEvents.AfterScenario += AfterScenarioPluginLifecycleEventHandler;
                    lcEvents.AfterFeature += AfterFeaturePluginLifecycleEventHandler;
                }
                args.ObjectContainer.Resolve<IServiceCollectionFinder>();
            }
        }

        private static void CustomizeFeatureDependenciesEventHandler(object sender, CustomizeFeatureDependenciesEventArgs args)
        {
            // At this point we have the bindings, we can resolve the service provider, which will build it if it's the first time.
            var spContainer = args.ObjectContainer.Resolve<RootServiceProviderContainer>();

            if (spContainer.Scoping == ScopeLevelType.Feature)
            {
                // Now we can register a new scoped service provider
                args.ObjectContainer.RegisterFactoryAs<IServiceProvider>(() =>
                {
                    // sesrvice provider creation as late as possible
                    var serviceProvider = spContainer.ServiceProvider;
                    var scope = serviceProvider.CreateScope();
                    BindMappings.TryAdd(scope.ServiceProvider, args.ObjectContainer.Resolve<IContextManager>());
                    ActiveServiceScopes.TryAdd(args.ObjectContainer.Resolve<FeatureContext>(), scope);
                    return scope.ServiceProvider;
                });
            }
        }

        private static void AfterFeaturePluginLifecycleEventHandler(object sender, RuntimePluginAfterFeatureEventArgs eventArgs)
        {
            if (ActiveServiceScopes.TryRemove(eventArgs.ObjectContainer.Resolve<FeatureContext>(), out var serviceScope))
            {
                BindMappings.TryRemove(serviceScope.ServiceProvider, out _);
                serviceScope.Dispose();
            }
        }

        private static void CustomizeScenarioDependenciesEventHandler(object sender, CustomizeScenarioDependenciesEventArgs args)
        {
            // At this point we have the bindings, we can resolve the service provider, which will build it if it's the first time.
            var spContainer = args.ObjectContainer.Resolve<RootServiceProviderContainer>();

            if (spContainer.Scoping == ScopeLevelType.Scenario)
            {
                // Now we can register a new scoped service provider
                args.ObjectContainer.RegisterFactoryAs<IServiceProvider>(() =>
                {
                    // sesrvice provider creation as late as possible
                    var serviceProvider = spContainer.ServiceProvider;
                    var scope = serviceProvider.CreateScope();
                    BindMappings.TryAdd(scope.ServiceProvider, args.ObjectContainer.Resolve<IContextManager>());
                    ActiveServiceScopes.TryAdd(args.ObjectContainer.Resolve<ScenarioContext>(), scope);
                    return scope.ServiceProvider;
                });
            }
        }

        private static void AfterScenarioPluginLifecycleEventHandler(object sender, RuntimePluginAfterScenarioEventArgs eventArgs)
        {
            if (ActiveServiceScopes.TryRemove(eventArgs.ObjectContainer.Resolve<ScenarioContext>(), out var serviceScope))
            {
                BindMappings.TryRemove(serviceScope.ServiceProvider, out _);
                serviceScope.Dispose();
            }
        }

        private static void RegisterProxyBindings(IObjectContainer objectContainer, IServiceCollection services)
        {
            // Required for DI of binding classes that want container injections
            // While they can (and should) use the method params for injection, we can support it.
            // Note that in Feature mode, one can't inject "ScenarioContext", this can only be done from method params.

            // Bases on this: https://docs.specflow.org/projects/specflow/en/latest/Extend/Available-Containers-%26-Registrations.html
            // Might need to add more...

            services.AddSingleton<IObjectContainer>(objectContainer);
            services.AddSingleton(sp => objectContainer.Resolve<IRuntimeConfigurationProvider>());
            services.AddSingleton(sp => objectContainer.Resolve<ITestRunnerManager>());
            services.AddSingleton(sp => objectContainer.Resolve<IStepFormatter>());
            services.AddSingleton(sp => objectContainer.Resolve<ITestTracer>());
            services.AddSingleton(sp => objectContainer.Resolve<ITraceListener>());
            services.AddSingleton(sp => objectContainer.Resolve<ITraceListenerQueue>());
            services.AddSingleton(sp => objectContainer.Resolve<IErrorProvider>());
            services.AddSingleton(sp => objectContainer.Resolve<IRuntimeBindingSourceProcessor>());
            services.AddSingleton(sp => objectContainer.Resolve<IBindingRegistry>());
            services.AddSingleton(sp => objectContainer.Resolve<IBindingFactory>());
            services.AddSingleton(sp => objectContainer.Resolve<IStepDefinitionRegexCalculator>());
            services.AddSingleton(sp => objectContainer.Resolve<IBindingInvoker>());
            services.AddSingleton(sp => objectContainer.Resolve<IStepDefinitionSkeletonProvider>());
            services.AddSingleton(sp => objectContainer.Resolve<ISkeletonTemplateProvider>());
            services.AddSingleton(sp => objectContainer.Resolve<IStepTextAnalyzer>());
            services.AddSingleton(sp => objectContainer.Resolve<IRuntimePluginLoader>());
            services.AddSingleton(sp => objectContainer.Resolve<IBindingAssemblyLoader>());
            services.AddSingleton(sp => objectContainer.Resolve<IUnitTestRuntimeProvider>());

            services.AddTransient(sp =>
            {
                var container = BindMappings.TryGetValue(sp, out var ctx)
                    ? ctx.ScenarioContext?.ScenarioContainer ??
                      ctx.FeatureContext?.FeatureContainer ??
                      ctx.TestThreadContext?.TestThreadContainer ??
                      objectContainer
                    : objectContainer;

                return container.Resolve<ISpecFlowOutputHelper>();
            });

            services.AddTransient(sp => BindMappings[sp]);
            services.AddTransient(sp => BindMappings[sp].TestThreadContext);
            services.AddTransient(sp => BindMappings[sp].FeatureContext);
            services.AddTransient(sp => BindMappings[sp].ScenarioContext);
            services.AddTransient(sp => BindMappings[sp].TestThreadContext.TestThreadContainer.Resolve<ITestRunner>());
            services.AddTransient(sp => BindMappings[sp].TestThreadContext.TestThreadContainer.Resolve<ITestExecutionEngine>());
            services.AddTransient(sp => BindMappings[sp].TestThreadContext.TestThreadContainer.Resolve<IStepArgumentTypeConverter>());
            services.AddTransient(sp => BindMappings[sp].TestThreadContext.TestThreadContainer.Resolve<IStepDefinitionMatchService>());
        }

        private class RootServiceProviderContainer
        {
            private IServiceProvider _serviceProvider;
            public IServiceProvider ServiceProvider
            {
                get
                {
                    _serviceProvider = _serviceProvider ?? _services.BuildServiceProvider();
                    // Mark the collection read-only, so that the user of the library knows
                    // that the provider-has been created and further registrations wouldn't take
                    // effect
                    _services.MakeReadOnly();
                    return _serviceProvider;
                }
            }
            private readonly ServiceCollection _services;
            public IServiceCollection Services => _services;
            public ScopeLevelType Scoping { get; }

            public RootServiceProviderContainer(ServiceCollection services, ScopeLevelType scoping)
            {
                _services = services;
                Scoping = scoping;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace SolidToken.SpecFlow.DependencyInjection
{
    public class ServiceCollectionFinder : IServiceCollectionFinder
    {
        private readonly IBindingRegistry bindingRegistry;
        private readonly Lazy<Func<IServiceCollection>> createScenarioServiceCollection;

        public ServiceCollectionFinder(IBindingRegistry bindingRegistry)
        {
            this.bindingRegistry = bindingRegistry;
            createScenarioServiceCollection = new Lazy<Func<IServiceCollection>>(FindCreateScenarioServiceCollection, true);
        }

        public Func<IServiceCollection> GetCreateScenarioServiceCollection()
        {
            var services = createScenarioServiceCollection.Value;
            if (services == null)
            {
                throw new MissingScenarioDependenciesException();
            }
            return services;
        }

        protected virtual Func<IServiceCollection> FindCreateScenarioServiceCollection()
        {
            var assemblies = bindingRegistry.GetBindingAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    foreach (var methodInfo in type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                    {
                        var scenarioDependenciesAttribute = (ScenarioDependenciesAttribute)Attribute.GetCustomAttribute(methodInfo, typeof(ScenarioDependenciesAttribute));

                        if (scenarioDependenciesAttribute != null)
                        {
                            return () =>
                            {
                                var serviceCollection = GetServiceCollection(methodInfo);
                                if (scenarioDependenciesAttribute.AutoRegisterBindings)
                                {
                                    AddBindingAttributes(assemblies, serviceCollection);
                                }
                                return serviceCollection;
                            };
                        }
                    }
                }
            }
            return null;
        }

        private static IServiceCollection GetServiceCollection(MethodBase methodInfo)
        {
            return (IServiceCollection)methodInfo.Invoke(null, null);
        }

        private static void AddBindingAttributes(IEnumerable<Assembly> bindingAssemblies, IServiceCollection serviceCollection)
        {
            foreach(var assembly in bindingAssemblies)
            {
                foreach (var type in assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))))
                {
                    serviceCollection.AddSingleton(type);
                }
            }
        }
    }
}

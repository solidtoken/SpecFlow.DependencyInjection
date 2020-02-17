using System;
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
                throw new Exception("Unable to find scenario dependencies! Mark a static method that returns a IServiceCollection with [ScenarioDependencies]!");
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
                    foreach (var methodInfo in type
                        .GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                        .Where(m => Attribute.IsDefined((MemberInfo)m, typeof(ScenarioDependenciesAttribute))))
                    {
                        return GetServiceCollection(methodInfo, assembly);
                    }
                }
            }
            return null;
        }

        private static Func<IServiceCollection> GetServiceCollection(MethodInfo methodInfo, Assembly assembly)
        {
            return () =>
            {
                var serviceCollection = (IServiceCollection)methodInfo.Invoke(null, null);
                AddBindingAttributes(assembly, serviceCollection);
                return serviceCollection;
            };
        }

        private static void AddBindingAttributes(Assembly assembly, IServiceCollection serviceCollection)
        {
            foreach (var type in assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))))
            {
                serviceCollection.AddSingleton(type);
            }
        }
    }
}

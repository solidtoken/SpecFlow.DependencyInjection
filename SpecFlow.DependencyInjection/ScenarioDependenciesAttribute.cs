using System;

namespace SolidToken.SpecFlow.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ScenarioDependenciesAttribute : Attribute
    {
        /// <summary>
        /// Automatically register all SpecFlow bindings
        /// </summary>
        public bool AutoRegisterBindings { get; set; } = true;
    }
}

using System;

namespace SolidToken.SpecFlow.DependencyInjection
{
    public enum ScopeLevelType
    {
        /// <summary>
        /// Scoping is created for every scenario and it is destroyed once the scenario ends.
        /// </summary>
        Scenario,
        /// <summary>
        /// Scoping is created for Feature scenario and it is destroyed once the Feature ends.
        /// </summary>
        Feature
    }
    
    [AttributeUsage(AttributeTargets.Method)]
    public class ScenarioDependenciesAttribute : Attribute
    {
        /// <summary>
        /// Automatically register all SpecFlow bindings.
        /// </summary>
        public bool AutoRegisterBindings { get; set; } = true;
        /// <summary>
        /// Define when to create and destroy scope. 
        /// </summary>
        public ScopeLevelType ScopeLevel { get; set; } = ScopeLevelType.Scenario;
    }
}

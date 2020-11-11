# SpecFlow.DependencyInjection

[![GitHub License](https://img.shields.io/github/license/solidtoken/SpecFlow.DependencyInjection.svg)](https://github.com/solidtoken/SpecFlow.DependencyInjection/blob/master/LICENSE) 
[![GitHub Issues](https://img.shields.io/github/issues/solidtoken/SpecFlow.DependencyInjection.svg)](https://github.com/solidtoken/SpecFlow.DependencyInjection/issues) 
[![Azure Build](https://img.shields.io/azure-devops/build/solidtoken/GitHub/8.svg)](https://solidtoken.visualstudio.com/GitHub/_build/latest?definitionId=8&branchName=master) 
[![NuGet Package](https://img.shields.io/nuget/v/SolidToken.SpecFlow.DependencyInjection.svg)](https://www.nuget.org/packages/SolidToken.SpecFlow.DependencyInjection)

SpecFlow plugin that enables to use Microsoft.Extensions.DependencyInjection for resolving test dependencies.

Currently supports:
* [SpecFlow v3.3.30 or later](https://www.nuget.org/packages/SpecFlow/3.3.30)
* [Microsoft.Extensions.DependencyInjection v3.1.5 or later](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/3.1.5)

Listed on [Available Plugins for SpecFlow](https://specflow.org/documentation/Available-Plugins/).
Based on https://github.com/gasparnagy/SpecFlow.Autofac (now [part of SpecFlow](https://github.com/SpecFlowOSS/SpecFlow/tree/master/Plugins/SpecFlow.Autofac.SpecFlowPlugin)).

## Usage

Install plugin from NuGet into your SpecFlow project.

```powershell
PM> Install-Package SolidToken.SpecFlow.DependencyInjection
```

Create a static method in your SpecFlow project that returns a `Microsoft.Extensions.DependencyInjection.IServiceCollection` and tag it with the `[ScenarioDependencies]` attribute. 
Configure your test dependencies for the scenario execution within this method. 
Step definition classes (i.e. classes with the SpecFlow `[Binding]` attribute) are automatically added to the service collection.

A typical dependency builder method looks like this:

```csharp
[ScenarioDependencies]
public static IServiceCollection CreateServices()
{
    var services = new ServiceCollection();
    
    // TODO: add your test dependencies here
    // NOTE: since v0.4.0 it's no longer necessary to manually add your [Binding] classes

    return services;
}
```

Refer to `SpecFlow.DependencyInjection.Tests` for an example.

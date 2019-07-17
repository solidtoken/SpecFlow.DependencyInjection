# SpecFlow.DependencyInjection

[![GitHub License](https://img.shields.io/github/license/solidtoken/SpecFlow.DependencyInjection.svg)](https://github.com/solidtoken/SpecFlow.DependencyInjection/blob/master/LICENSE) 
[![GitHub Issues](https://img.shields.io/github/issues/solidtoken/SpecFlow.DependencyInjection.svg)](https://github.com/solidtoken/SpecFlow.DependencyInjection/issues) 
[![Azure Build](https://img.shields.io/azure-devops/build/solidtoken/GitHub/8.svg)](https://solidtoken.visualstudio.com/GitHub/_build/latest?definitionId=8&branchName=master) 
[![NuGet Package](https://img.shields.io/nuget/v/SolidToken.SpecFlow.DependencyInjection.svg)](https://www.nuget.org/packages/SolidToken.SpecFlow.DependencyInjection)

SpecFlow plugin for using Microsoft.Extensions.DependencyInjection as a dependency injection framework.

Based on https://github.com/gasparnagy/SpecFlow.Autofac ([Apache License 2.0](https://github.com/gasparnagy/SpecFlow.Autofac/blob/master/LICENSE))

Currently supports:
* [SpecFlow v3.0](https://www.nuget.org/packages/SpecFlow/3.0)
* [Microsoft.Extensions.DependencyInjection v2.2](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/2.2)

## Usage

Install plugin from NuGet into your SpecFlow project.

```powershell
PM> Install-Package SolidToken.SpecFlow.DependencyInjection
```

Create a static method somewhere in the SpecFlow project (recommended to put it into the ```Support``` folder) that returns an Microsoft.Extensions.DependencyInjection ```IServiceCollection``` and tag it with the `[ScenarioDependencies]` attribute. Configure your dependencies for the scenario execution within the method. You also have to register the step definition classes, that you can do by either registering all classes marked with the ```[Binding]``` attribute:

```csharp
foreach (var type in typeof(TestDependencies).Assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))))
{
    services.AddSingleton(type);
}
```

A typical dependency builder method probably looks like this:

```csharp
[ScenarioDependencies]
public static IServiceCollection CreateServices()
{
    var services = new ServiceCollection();
    
    // TODO: add customizations, stubs required for testing

    foreach (var type in typeof(TestDependencies).Assembly.GetTypes().Where(t => Attribute.IsDefined(t, typeof(BindingAttribute))))
    {
        services.AddSingleton(type);
    }

    return services;
}
```

Refer to ```SpecFlow.DependencyInjection.Tests``` for a typical implementation.

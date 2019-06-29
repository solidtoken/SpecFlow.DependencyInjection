using System;
using Microsoft.Extensions.DependencyInjection;

namespace SolidToken.SpecFlow.DependencyInjection
{
	public interface IServiceCollectionFinder
	{
		Func<IServiceCollection> GetCreateScenarioServiceCollection();
	}
}

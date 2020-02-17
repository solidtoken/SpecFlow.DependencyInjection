Feature: DependencyInjectionPlugin
	As a developer I want to verify
	that the plugin SolidToken.SpecFlow.DependencyInjection
	allows me to use Microsoft.Extensions.DependencyInjection
    inside the MSTest test framework

Scenario: Test service injection
	Then verify that TestService is correctly injected

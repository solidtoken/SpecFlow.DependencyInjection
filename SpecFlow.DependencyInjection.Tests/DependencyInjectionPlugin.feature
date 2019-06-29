Feature: DependencyInjectionPlugin
	As a developer I want to verify
	that the DependencyInjectionPlugin
	allows me to use Microsoft.Extensions.DependencyInjection

Scenario: Test service injection
	Then verify that TestService is correctly injected

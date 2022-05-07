Feature: UnitTestRuntimeProvider
    Issue #73: Unable to resolve IUnitTestRuntimeProvider
    https://github.com/solidtoken/SpecFlow.DependencyInjection/issues/73

Scenario: Assert IUnitTestRuntimeProvider is available
	Then verify that IUnitTestRuntimeProvider is correctly injected

Feature: IAsyncDisposable
	Issue #48: Support IAsyncDisposable
	https://github.com/solidtoken/SpecFlow.DependencyInjection/issues/48

Scenario: Assert class that implements IAsyncDisposable can be registered
	Then verify that AsyncDisposableClass is correctly registered

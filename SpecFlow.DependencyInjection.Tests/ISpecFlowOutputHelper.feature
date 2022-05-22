Feature: ISpecFlowOutputHelper
    Issue #75: ITestOutputHelper unavailable when using SpecFlow.xUnit
    https://github.com/solidtoken/SpecFlow.DependencyInjection/issues/75

Scenario: Assert ISpecFlowOutputHelper Is Available
    The When part here is purely for displaying the message using ISpecFlowOutputHelper
    When a message is output using ISpecFlowOutputHelper
    Then verify that ISpecFlowOutputHelper is correctly injected

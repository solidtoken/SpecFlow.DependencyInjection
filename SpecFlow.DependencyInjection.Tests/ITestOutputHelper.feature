Feature: ITestOutputHelper
    Issue #75: ITestOutputHelper unavailable when using SpecFlow.xUnit
    https://github.com/solidtoken/SpecFlow.DependencyInjection/issues/75

Scenario: Assert ITestOutputHelper Is Available
    The When part here is purely for displaying the message using ITestOutputHelper
    When a message is output using ITestOutputHelper
    Then verify that ITestOutputHelper is correctly injected

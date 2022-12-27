Feature: ContextInjectionScope
    Issue #12: Scoping Context Injection to Scenario Execution Lifetimes
    https://github.com/solidtoken/SpecFlow.DependencyInjection/issues/12

Scenario: Assert Context Is Scoped To Scenario Execution
    The multiply and increase steps are intentionally implemented using different bindings.
    Assert that they can operate on the same context (within the scenario executing).
	Given I have test context with number 5
    When I multiply the test context number by 2
    And I increase the test context number by 3
    Then the test context number should be 13

Scenario: Assert Context Is Scoped To Scenario Execution (No Spill)
    Assert that the test context does not spill over to other scenarios.
    Note that this assumes this scenario will be run after the above one (ie don't use parallel tests).
    Given I have a test context
    Then the test context number should be 0

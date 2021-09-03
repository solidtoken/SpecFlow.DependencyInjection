Feature: ScenarioContextDisposal
    Issue #68: After ScenarioContext class is used in steps, disposal fails
    https://github.com/solidtoken/SpecFlow.DependencyInjection/issues/68

Scenario: Assert context is disposed correctly
    Given I have scenario context with number 7

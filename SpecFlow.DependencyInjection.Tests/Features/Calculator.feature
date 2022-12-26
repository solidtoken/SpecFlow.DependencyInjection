Feature: Calculator
    As a developer I want to test that the plugin behaves correctly using a Calculator class
    
Scenario: Add Two Numbers
	Given I have entered 10 into the Calculator
    And I have entered 20 into the Calculator
    When I press Add
    Then the Result should be 30

Scenario: Multiply Two Numbers
    Given I have entered 30 into the Calculator
    And I have entered 40 into the Calculator
    When I press Multiply
    Then the Result should be 1200

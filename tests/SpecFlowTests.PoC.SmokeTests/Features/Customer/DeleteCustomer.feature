Feature: Delete customer

Background:
    Given Customer API

Scenario: Created customer should be deleted
    When create single customer endpoint is requested with data:
    """
    {
        "first_name": "John",
        "last_name": "smoke-test"
    }
    """
    Then response status code is 201
    And response contains customer id
    When delete single customer endpoint is requested with already created customer id
    Then response status code is 204

Scenario: Should do not fail if customer does not exists
    Given customer id
    When delete single customer endpoint is requested with already created customer id
    Then response status code is 204

Feature: Add customer

Scenario: Customer should be created successfully when valid input data
    Given Customer API
    When create single customer endpoint is requested with data:
    """
    {
        "first_name": "John",
        "last_name": "smoke-test"
    }
    """
    Then response status code is 201
    And response contains customer id

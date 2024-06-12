Feature: Get customers

Background:
    Given Customer API

Scenario: Should return 200 and empty customers list when customer was deleted
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
    When get customers endpoint is requested
    Then response status code is 200
    And customers list is empty

Scenario: Should return 200 with created customers
    When create single customer endpoint is requested with data:
    """
    {
        "first_name": "John",
        "last_name": "smoke-test"
    }
    """
    Then response status code is 201
    And response contains customer id
    When create single customer endpoint is requested with data:
    """
    {
        "first_name": "Harry",
        "last_name": "smoke-test"
    }
    """
    Then response status code is 201
    And response contains customer id
    When get customers endpoint is requested
    Then response status code is 200
    And customers list is valid with customers:
    """
    [
    {
        "FirstName": "John",
        "LastName": "smoke-test"
    },
    {
        "FirstName": "Harry",
        "LastName": "smoke-test"
    }
    ]
    """

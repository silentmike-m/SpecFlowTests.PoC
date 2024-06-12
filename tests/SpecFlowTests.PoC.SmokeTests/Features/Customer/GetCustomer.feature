Feature: Get customer

Background:
    Given Customer API

Scenario: Should return 404 when customer was deleted
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
    When get single customer endpoint is requested with already created customer id
    Then response status code is 404

Scenario: Should return created customer
    When create single customer endpoint is requested with data:
    """
    {
        "first_name": "John",
        "last_name": "smoke-test"
    }
    """
    Then response status code is 201
    And response contains customer id
    When get single customer endpoint is requested with already created customer id
    Then response status code is 200
    And customer is valid with firstName: John and lastName: smoke-test

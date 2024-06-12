namespace SpecFlowTests.PoC.SmokeTests.StepDefinitions;

using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using SpecFlowTests.PoC.SmokeTests.Drivers.Customers;
using SpecFlowTests.PoC.SmokeTests.Drivers.Customers.Models;
using TechTalk.SpecFlow;

[Binding]
internal sealed class CustomerStepDefinitions : IDisposable
{
    private readonly ScenarioContext scenarioContext;

    private CustomerApiDriver customerApiDriver;
    private string? customerId;
    private HttpResponseMessage? requestResonse;

    public CustomerStepDefinitions(ScenarioContext scenarioContext)
        => this.scenarioContext = scenarioContext;

    public void Dispose()
        => this.customerApiDriver.Dispose();

    [Given("Customer API")]
    public void GivenCustomerApi()
    {
        this.customerApiDriver = new CustomerApiDriver(this.scenarioContext.Get<CustomerApiDriverOptions>());
    }

    [Given("customer id")]
    public void GivenCustomerId()
    {
        this.customerId = Guid.NewGuid().ToString();
    }

    [Then("customer is valid with firstName: (.*) and lastName: (.*)")]
    public async Task ThenCustomerIsValidWithData(string firstName, string lastName)
    {
        if (this.requestResonse is null)
        {
            Assert.Fail("THEN step cannot be executed before WHEN step");
        }

        if (this.customerId is null)
        {
            Assert.Fail("THEN step cannot be executed. First create customer");
        }

        var customer = await this.requestResonse.Content.ReadFromJsonAsync<Customer>();

        customer.Should()
            .NotBeNull();

        var expectedCustomer = new Customer
        {
            FirstName = firstName,
            Id = Guid.Parse(this.customerId),
            LastName = lastName,
        };

        customer.Should()
            .BeEquivalentTo(expectedCustomer);
    }

    [Then("customers list is empty")]
    public async Task ThenCustomersListIsEmpty()
    {
        if (this.requestResonse is null)
        {
            Assert.Fail("THEN step cannot be executed before WHEN step");
        }

        var customers = await this.requestResonse.Content.ReadFromJsonAsync<List<Customer>>();

        customers.Should()
            .NotBeNull()
            .And
            .BeEmpty();
    }

    [Then("customers list is valid with customers:")]
    public async Task ThenCustomersListIsValidWithData(string customersPayload)
    {
        if (this.requestResonse is null)
        {
            Assert.Fail("THEN step cannot be executed before WHEN step");
        }

        var customers = await this.requestResonse.Content.ReadFromJsonAsync<List<Customer>>();

        customers.Should()
            .NotBeNull();

        var expectedCustomers = JsonSerializer.Deserialize<List<Customer>>(customersPayload);

        customers.Should()
            .BeEquivalentTo(expectedCustomers, options => options.Excluding(customer => customer.Id));
    }

    [Then("response contains customer id")]
    public async Task ThenResponseBodyContainsCreatedCustomerId()
    {
        if (this.requestResonse is null)
        {
            Assert.Fail("THEN step cannot be executed before WHEN step");
        }

        var responseString = await this.requestResonse.Content.ReadAsStringAsync();

        var responseJson = JObject.Parse(responseString);

        responseJson.Should()
            .NotBeNull();

        var customerIdString = responseJson["customerId"]?.ToString();

        customerIdString.Should()
            .NotBeNullOrWhiteSpace();

        var isGuid = Guid.TryParse(customerIdString, out _);

        isGuid.Should()
            .BeTrue();

        this.customerId = customerIdString;
    }

    [Then("response status code is (.*)")]
    public void ThenResponseStatusCodeIsEqual(int statusCode)
    {
        if (this.requestResonse is null)
        {
            Assert.Fail("THEN step cannot be executed before WHEN step");
        }

        ((int)this.requestResonse.StatusCode).Should()
            .Be(statusCode);
    }

    [When("create single customer endpoint is requested with data:")]
    public async Task WhenCreateSingleCustomerEndpointIsRequested(string postCustomerJson)
    {
        if (this.customerApiDriver is null)
        {
            Assert.Fail("WHEN step cannot be executed. First target api has to be defined");
        }

        this.requestResonse = await this.customerApiDriver.AddCustomerAsync(postCustomerJson);
    }

    [When("delete single customer endpoint is requested with already created customer id")]
    public async Task WhenDeleteSingleCustomerEndpointIsRequested()
    {
        if (this.customerApiDriver is null)
        {
            Assert.Fail("WHEN step cannot be executed. First target api has to be defined");
        }

        if (this.customerId is null)
        {
            Assert.Fail("When step cannot be executed. First create customer");
        }

        this.requestResonse = await this.customerApiDriver.DeleteCustomerAsync(this.customerId);
    }

    [When("get customers endpoint is requested")]
    public async Task WhenGetCustomersEndpointIsRequested()
    {
        if (this.customerApiDriver is null)
        {
            Assert.Fail("WHEN step cannot be executed. First target api has to be defined");
        }

        this.requestResonse = await this.customerApiDriver.GetCustomersAsync();
    }

    [When("get single customer endpoint is requested with already created customer id")]
    public async Task WhenGetSingleCustomerEndpointIsRequested()
    {
        if (this.customerApiDriver is null)
        {
            Assert.Fail("WHEN step cannot be executed. First target api has to be defined");
        }

        if (this.customerId is null)
        {
            Assert.Fail("When step cannot be executed. First create customer");
        }

        this.requestResonse = await this.customerApiDriver.GetCustomerAsync(this.customerId);
    }
}

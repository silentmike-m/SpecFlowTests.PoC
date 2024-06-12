namespace SpecFlowTests.PoC.SmokeTests.Drivers.Customers;

using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using SpecFlowTests.PoC.SmokeTests.Drivers.Customers.Models;

internal sealed class CustomerApiDriver : IDisposable
{
    private const string ADD_CUSTOMER_ENDPOINT = "/Customers/AddCustomer";
    private const string DELETE_ENDPOINT = "/Customers/DeleteCustomer?id=";
    private const string GET_CUSTOMER_ENDPOINT = "/Customers/GetCustomer?id=";
    private const string GET_CUSTOMERS_ENDPOINT = "/Customers/GetCustomers";

    private readonly HttpClient httpClient;

    public CustomerApiDriver(CustomerApiDriverOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.BaseUrl))
        {
            throw new ArgumentException(nameof(options));
        }

        this.httpClient = new HttpClient
        {
            BaseAddress = new Uri(options.BaseUrl),
        };
    }

    public void Dispose()
        => this.httpClient.Dispose();

    public async Task<HttpResponseMessage> AddCustomerAsync(string payload)
    {
        using var content = new StringContent(payload, Encoding.UTF8, MediaTypeNames.Application.Json);

        var uri = new Uri(ADD_CUSTOMER_ENDPOINT, UriKind.Relative);

        return await this.httpClient.PostAsync(uri, content);
    }

    public async Task CleanUpAsync()
    {
        var customersResponse = await this.GetCustomersAsync();
        var customers = await customersResponse.Content.ReadFromJsonAsync<List<Customer>>();

        foreach (var customer in customers)
        {
            if (customer.LastName.Contains("smoke-test", StringComparison.OrdinalIgnoreCase))
            {
                _ = await this.DeleteCustomerAsync(customer.Id.ToString());
            }
        }
    }

    public async Task<HttpResponseMessage> DeleteCustomerAsync(string id)
    {
        var uri = new Uri($"{DELETE_ENDPOINT}{id}", UriKind.Relative);

        return await this.httpClient.DeleteAsync(uri);
    }

    public async Task<HttpResponseMessage> GetCustomerAsync(string id)
    {
        var uri = new Uri($"{GET_CUSTOMER_ENDPOINT}{id}", UriKind.Relative);

        return await this.httpClient.GetAsync(uri);
    }

    public async Task<HttpResponseMessage> GetCustomersAsync()
    {
        var uri = new Uri(GET_CUSTOMERS_ENDPOINT, UriKind.Relative);

        return await this.httpClient.GetAsync(uri);
    }
}

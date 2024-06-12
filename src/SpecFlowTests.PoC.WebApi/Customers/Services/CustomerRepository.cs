namespace SpecFlowTests.PoC.WebApi.Customers.Services;

using SpecFlowTests.PoC.WebApi.Customers.Interfaces;
using SpecFlowTests.PoC.WebApi.Customers.Models;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly Dictionary<Guid, Customer> customers = new();

    public async Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        this.customers[customer.Id] = customer;

        await Task.CompletedTask;
    }

    public async Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        this.customers.Remove(id);

        await Task.CompletedTask;
    }

    public async Task<Customer?> GetCustomerAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = this.customers.TryGetValue(id, out var customer)
            ? customer
            : null;

        return await Task.FromResult(result);
    }

    public async Task<IReadOnlyList<Customer>> GetCustomersAsync(CancellationToken cancellationToken = default)
    {
        var result = this.customers.Values.ToList();

        return await Task.FromResult(result);
    }
}

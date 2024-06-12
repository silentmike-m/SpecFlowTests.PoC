namespace SpecFlowTests.PoC.WebApi.Customers.Interfaces;

using SpecFlowTests.PoC.WebApi.Customers.Models;

public interface ICustomerRepository
{
    Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer?> GetCustomerAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Customer>> GetCustomersAsync(CancellationToken cancellationToken = default);
}

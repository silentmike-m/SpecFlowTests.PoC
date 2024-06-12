namespace SpecFlowTests.PoC.WebApi.Customers.Queries;

using SpecFlowTests.PoC.WebApi.Customers.Models;

public sealed record GetCustomers : IRequest<IReadOnlyList<Customer>>;

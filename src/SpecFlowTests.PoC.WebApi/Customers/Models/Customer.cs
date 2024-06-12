namespace SpecFlowTests.PoC.WebApi.Customers.Models;

public sealed record Customer
{
    public required string FirstName { get; init; }
    public required Guid Id { get; init; }
    public required string LastName { get; init; }
}

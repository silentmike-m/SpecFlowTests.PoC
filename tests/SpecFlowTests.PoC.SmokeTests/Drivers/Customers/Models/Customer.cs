namespace SpecFlowTests.PoC.SmokeTests.Drivers.Customers.Models;

internal sealed record Customer
{
    public string FirstName { get; init; } = string.Empty;
    public Guid Id { get; init; } = Guid.Empty;
    public string LastName { get; init; } = string.Empty;
}

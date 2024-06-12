namespace SpecFlowTests.PoC.WebApi.Customers.Commands;

using System.Text.Json.Serialization;

public sealed record AddCustomer : IRequest<Guid>
{
    [JsonPropertyName("first_name")]
    public required string FirstName { get; init; }

    [JsonPropertyName("last_name")]
    public required string LastName { get; init; }
}

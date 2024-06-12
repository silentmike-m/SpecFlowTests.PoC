namespace SpecFlowTests.PoC.WebApi.Customers.Commands;

using System.Text.Json.Serialization;

public sealed record DeleteCustomer : IRequest
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }
}

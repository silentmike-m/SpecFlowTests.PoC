namespace SpecFlowTests.PoC.WebApi.Customers.Queries;

using System.Text.Json.Serialization;
using SpecFlowTests.PoC.WebApi.Customers.Models;

public sealed record GetCustomer : IRequest<Customer>
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }
}

namespace SpecFlowTests.PoC.WebApi.Customers.Exceptions;

using System.Runtime.Serialization;
using SpecFlowTests.PoC.WebApi.Exceptions;

public sealed class CustomerNotFoundException : EntityNotFoundException
{
    public CustomerNotFoundException(Guid id)
        : base($"Customer with '{id}' has not been found")
        => this.EntityId = id.ToString();

    private CustomerNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}

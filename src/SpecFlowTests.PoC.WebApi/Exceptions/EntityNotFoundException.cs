namespace SpecFlowTests.PoC.WebApi.Exceptions;

using System.Runtime.Serialization;

public abstract class EntityNotFoundException : Exception
{
    public string EntityId { get; protected set; }

    protected EntityNotFoundException(string message, Exception? innerException = null)
        : base(message, innerException)
    {
    }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}

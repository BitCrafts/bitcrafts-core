using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerUpdateEventResponse : BaseEvent, IEventResponse
{
    public bool Success { get; }
    public IEventRequest Request { get; set; }
}
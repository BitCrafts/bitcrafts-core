using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerUpdateEventResponse : BaseEvent, IEventResponse
{
    public IEventRequest Request { get; set; }
    public bool Success { get; }
}
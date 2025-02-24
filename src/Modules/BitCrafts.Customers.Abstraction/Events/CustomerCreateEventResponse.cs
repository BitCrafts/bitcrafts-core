using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerCreateEventResponse : BaseEvent, IEventResponse
{
    public int CustomerId { get; set; }
    public IEventRequest Request { get; set; }
}
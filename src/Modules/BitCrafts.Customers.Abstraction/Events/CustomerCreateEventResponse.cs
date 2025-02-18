using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerCreateEventResponse : BaseEvent, IEventResponse
{
    public IEventRequest Request { get; set; }
    public int CustomerId { get; set; }
}
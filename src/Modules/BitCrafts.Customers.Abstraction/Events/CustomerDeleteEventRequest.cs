using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerDeleteEventRequest : BaseEvent, IEventRequest
{
    public int CustomerId { get; set; }
    public IEventResponse Response { get; set; }
}
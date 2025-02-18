using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerDeleteEventRequest : BaseEvent, IEventRequest
{
    public IEventResponse Response { get; set; }
    public int CustomerId { get; set; }
}
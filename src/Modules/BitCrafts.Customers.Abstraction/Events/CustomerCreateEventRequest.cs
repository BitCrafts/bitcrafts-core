using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerCreateEventRequest : BaseEvent, IEventRequest
{
    public IEventResponse Response { get; set; }
    public ICustomer Customer { get; set; }
}
using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerUpdateEventRequest : BaseEvent, IEventRequest
{
    public ICustomer Customer { get; set; }
    public IEventResponse Response { get; set; }
}
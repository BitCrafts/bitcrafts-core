using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public class CustomerGroupAssignedEvent : BaseEventResponse, IEventResponse
{
    public IEventRequest Request { get; }
    public ICustomer Customer { get; set; }
    public bool Success { get; set; }
}
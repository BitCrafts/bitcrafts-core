using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public class CustomerGroupAssignEvent : BaseEventRequest, IEventRequest
{
    public int CustomerGroupId { get; set; }
    public int CustomerId { get; set; }
    public IEventResponse Response { get; set; }
}
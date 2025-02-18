using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public class CustomerGroupAssignEvent : BaseEventRequest, IEventRequest
{
    public IEventResponse Response { get; set; }
    public int CustomerGroupId { get; set; }
    public int CustomerId { get; set; }
}
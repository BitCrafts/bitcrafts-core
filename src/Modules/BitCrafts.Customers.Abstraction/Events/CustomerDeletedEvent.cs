using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerDeletedEvent : BaseCustomerEvent
{
    public CustomerDeletedEvent(ICustomer customer) : base(customer)
    {
    }
}
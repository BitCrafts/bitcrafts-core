using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerCreatedEvent : BaseCustomerEvent
{
    public CustomerCreatedEvent(ICustomer customer) : base(customer)
    {
    }
}
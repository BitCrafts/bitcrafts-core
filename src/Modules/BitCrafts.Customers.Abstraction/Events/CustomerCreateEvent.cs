using BitCrafts.Customers.Abstraction.Entities;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerCreateEvent : BaseCustomerEvent
{
    public CustomerCreateEvent(ICustomer customer) : base(customer)
    {
    }
}
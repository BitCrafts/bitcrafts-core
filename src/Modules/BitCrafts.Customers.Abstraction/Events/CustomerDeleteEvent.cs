using BitCrafts.Customers.Abstraction.Entities;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerDeleteEvent : BaseCustomerEvent
{
    public CustomerDeleteEvent(ICustomer customer) : base(customer)
    {
    }
}
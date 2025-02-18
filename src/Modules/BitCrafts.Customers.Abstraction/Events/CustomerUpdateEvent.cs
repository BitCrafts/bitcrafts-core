using BitCrafts.Customers.Abstraction.Entities;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerUpdateEvent : BaseCustomerEvent
{
    public ICustomer NewCustomer { get; }

    public CustomerUpdateEvent(ICustomer oldCustomer, ICustomer newCustomer) : base(oldCustomer)
    {
        NewCustomer = newCustomer;
    }
}
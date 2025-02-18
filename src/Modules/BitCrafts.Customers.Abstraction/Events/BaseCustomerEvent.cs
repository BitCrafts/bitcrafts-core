using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public abstract class BaseCustomerEvent : BaseEvent
{
    public ICustomer Customer { get; }

    protected BaseCustomerEvent(ICustomer customer)
    {
        Customer = customer;
    }
}
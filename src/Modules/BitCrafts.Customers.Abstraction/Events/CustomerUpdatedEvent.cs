using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Customers.Abstraction.Events;

public sealed class CustomerUpdatedEvent : BaseCustomerEvent
{
    public ICustomer NewCustomer { get; }

    public CustomerUpdatedEvent(ICustomer oldCustomer, ICustomer newCustomer) : base(oldCustomer)
    {
        NewCustomer = newCustomer;
    }
}
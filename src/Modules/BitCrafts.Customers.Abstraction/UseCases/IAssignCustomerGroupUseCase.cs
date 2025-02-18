using BitCrafts.Customers.Abstraction.Events;
using BitCrafts.UseCases.Abstraction;

namespace BitCrafts.Customers.Abstraction.UseCases;

public interface IAssignCustomerGroupUseCase : IUseCase<CustomerGroupAssignEvent,CustomerGroupAssignedEvent>
{
    
}
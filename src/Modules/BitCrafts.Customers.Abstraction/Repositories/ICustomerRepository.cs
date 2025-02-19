using BitCrafts.Infrastructure.Abstraction.Repositories;

namespace BitCrafts.Customers.Abstraction.Repositories;

public interface ICustomerRepository : IRepository<int>
{
    Task<bool> AssignCustomerToGroup(int customerId, int groupId);
}
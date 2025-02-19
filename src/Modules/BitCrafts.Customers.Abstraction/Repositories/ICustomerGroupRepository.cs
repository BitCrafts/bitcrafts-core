using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Repositories;

namespace BitCrafts.Customers.Abstraction.Repositories;

public interface ICustomerGroupRepository : IRepository<ICustomerGroup,int>
{
}
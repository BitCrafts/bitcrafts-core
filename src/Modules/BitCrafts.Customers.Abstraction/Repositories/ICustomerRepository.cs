using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Repositories;

namespace BitCrafts.Customers.Abstraction.Repositories;

public interface ICustomerRepository : IRepository<ICustomer,int>
{
    
}
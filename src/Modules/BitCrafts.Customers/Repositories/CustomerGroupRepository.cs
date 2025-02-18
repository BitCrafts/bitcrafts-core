using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Databases; 
using BitCrafts.Infrastructure.Repositories;

namespace BitCrafts.Customers.Repositories;

public sealed class CustomerGroupRepository : BaseRepository<ICustomerGroup, int>, ICustomerGroupRepository
{
    public CustomerGroupRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }
}
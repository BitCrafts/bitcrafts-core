using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Repositories;

namespace BitCrafts.Customers.Repositories;

public sealed class CustomerRepository : BaseRepository<ICustomer, int>, ICustomerRepository
{
    public CustomerRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }
}
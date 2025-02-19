using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Repositories;

namespace BitCrafts.Customers.Repositories;

public sealed class CustomerGroupRepository : BaseRepository<int>, ICustomerGroupRepository
{
    public CustomerGroupRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    protected override string GetTableName()
    {
        return "CustomerGroup";
    }
}
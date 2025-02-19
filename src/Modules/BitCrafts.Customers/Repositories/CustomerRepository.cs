using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Customers.Entities;
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Repositories;
using Dapper;

namespace BitCrafts.Customers.Repositories;

public sealed class CustomerRepository : BaseRepository<int>, ICustomerRepository
{
    private const string assignCustomerToGroupSql = $"UPDATE  Customer SET GroupId = @GroupId WHERE Id = @Id";

    public CustomerRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public async Task<bool> AssignCustomerToGroup(int customerId, int groupId)
    {
        using var connection = ConnectionFactory.Create();

        var rowAffected =
            await connection.ExecuteAsync(assignCustomerToGroupSql, new { Id = customerId, GroupId = groupId });
        return rowAffected > 0;
    }

    protected override string GetTableName()
    {
        return "Customer";
    }
}
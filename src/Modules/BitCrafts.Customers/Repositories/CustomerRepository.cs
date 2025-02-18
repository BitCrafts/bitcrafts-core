using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Repositories;
using Dapper;

namespace BitCrafts.Customers.Repositories;

public sealed class CustomerRepository : BaseRepository<ICustomer, int>, ICustomerRepository
{
    private const string assignCustomerToGroupSql = "UPDATE Customers SET GroupId = @GroupId WHERE Id = @Id";
    public CustomerRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }

    public async Task<bool> AssignCustomerToGroup(int customerId, int groupId)
    {
       using var connection = ConnectionFactory.Create();
       
       var rowAffected = await connection.ExecuteAsync(assignCustomerToGroupSql);
       return rowAffected > 0;
    }
}
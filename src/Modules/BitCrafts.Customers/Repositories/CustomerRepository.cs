using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Customers.Entities;
using BitCrafts.Infrastructure.Abstraction.Databases;
using Dapper;

namespace BitCrafts.Customers.Repositories;

public sealed class CustomerRepository : ICustomerRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private const string assignCustomerToGroupSql = $"UPDATE  Customer SET GroupId = @GroupId WHERE Id = @Id";

    public CustomerRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<bool> AssignCustomerToGroup(int customerId, int groupId)
    {
        using var connection = _connectionFactory.Create();

        var rowAffected =
            await connection.ExecuteAsync(assignCustomerToGroupSql, new { Id = customerId, GroupId = groupId });
        return rowAffected > 0;
    }

    private string GetTableName()
    {
        return "Customer";
    }

    public Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.Create();
        var deleteSql = $"DELETE FROM {GetTableName()} WHERE Id = @Id";
        var rowAffected =
            connection.Execute(deleteSql, new { Id = id });
        return Task.FromResult(rowAffected > 0);
    }

    public async Task<bool> UpdateAsync(ICustomer entity)
    {
        using var connection = _connectionFactory.Create();
        var updateSql =
            $"UPDATE {GetTableName()} SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, GroupId = @GroupId WHERE Id = @Id";
        var rowAffected =
            await connection.ExecuteAsync(updateSql, entity);
        return rowAffected > 0;
    }

    public async Task<ICustomer> AddAsync(ICustomer entity)
    {
        using var connection = _connectionFactory.Create();
        var insertSql =
            $"INSERT INTO {GetTableName()} (FirstName, LastName, Email, Phone, GroupId) VALUES (@FirstName, @LastName, @Email, @Phone, @GroupId);";
        var getLastInsertedIdSql = "select last_insert_rowid();";
        var rowAffected = await connection.ExecuteAsync(insertSql, entity);
        var id = connection.QueryFirstOrDefault<int>(getLastInsertedIdSql);
        entity.Id = id;
        return entity;
    }

    public async Task<IEnumerable<ICustomer>> GetAllAsync()
    {
        using var connection = _connectionFactory.Create();
        var selectAllSql = $"SELECT * FROM {GetTableName()}";
        var result = await connection.QueryAsync<Customer>(selectAllSql);
        return result.Cast<ICustomer>();
    }

    public async Task<ICustomer> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.Create();
        var selectByIdSql = $"SELECT * FROM {GetTableName()} WHERE Id = @Id";
        var result = await connection.QueryFirstOrDefaultAsync<Customer>(selectByIdSql, new { Id = id });
        return result as ICustomer;
    }
}
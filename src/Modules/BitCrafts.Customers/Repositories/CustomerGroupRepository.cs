using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Customers.Abstraction.Repositories;
using BitCrafts.Customers.Entities;
using BitCrafts.Infrastructure.Abstraction.Databases;
using Dapper;

namespace BitCrafts.Customers.Repositories;

public sealed class CustomerGroupRepository : ICustomerGroupRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public CustomerGroupRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    private string GetTableName()
    {
        return "CustomerGroup";
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _connectionFactory.Create();
        var deleteSql = "DELETE FROM CustomerGroup WHERE Id = @Id";
        var result = await connection.ExecuteAsync(deleteSql, new { Id = id });
        return result > 0;
    }

    public async Task<bool> UpdateAsync(ICustomerGroup entity)
    {
        using var connection = _connectionFactory.Create();
        var updateSql = "UPDATE CustomerGroup SET Name = @Name WHERE Id = @Id";
        var result = await connection.ExecuteAsync(updateSql, entity);
        return result > 0;
    }

    public async Task<ICustomerGroup> AddAsync(ICustomerGroup entity)
    {
        using var connection = _connectionFactory.Create();
        var insertSql = "INSERT INTO CustomerGroup (Name) VALUES (@Name)";
        var id = await connection.ExecuteScalarAsync<int>(insertSql, entity);
        entity.Id = id;
        return entity;
    }

    public async Task<IEnumerable<ICustomerGroup>> GetAllAsync()
    {
        using var connection = _connectionFactory.Create();
        var selectAllSql = "SELECT * FROM CustomerGroup";
        var result = await connection.QueryAsync<CustomerGroup>(selectAllSql);
        return result.Cast<ICustomerGroup>();
    }

    public async Task<ICustomerGroup> GetByIdAsync(int id)
    {
        using var connection = _connectionFactory.Create();
        var selectByIdSql = "SELECT * FROM CustomerGroup WHERE Id = @Id";
        var result = await connection.QueryFirstOrDefaultAsync<CustomerGroup>(selectByIdSql, new { Id = id });
        return result as ICustomerGroup;
    }
}
using System.Reflection;
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using Dapper;

namespace BitCrafts.Infrastructure.Repositories;

public abstract class BaseRepository<TKey> : IRepository<TKey>
{
    private readonly IDbConnectionFactory _connectionFactory;
    protected IDbConnectionFactory ConnectionFactory => _connectionFactory;

    protected BaseRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    protected abstract string GetTableName();

    public async Task<bool> DeleteAsync(TKey id)
    {
        using var connection = _connectionFactory.Create();
        var tableName = GetTableName();
        var sql = $"DELETE FROM {tableName} WHERE Id = @Id";
        return await connection.ExecuteAsync(sql, new { Id = id }) > 0;
    }

    public async Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : IEntity<TKey>
    {
        using var connection = _connectionFactory.Create();
        var tableName = GetTableName();
        var properties = GetProperties<TEntity>();

        var sql =
            $"UPDATE {tableName} SET {string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"))} WHERE Id = @Id";
        return await connection.ExecuteAsync(sql, entity) > 0;
    }

    public async Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : IEntity<TKey>
    {
        using var connection = _connectionFactory.Create();
        var tableName = GetTableName();
        var properties = GetProperties<TEntity>();
        var sql =
            $"INSERT INTO {tableName} ({string.Join(", ", properties.Select(p => p.Name))}) VALUES ({string.Join(", ", properties.Select(p => $"@{p.Name}"))}); SELECT LAST_INSERT_ROWID()";

        var id = await connection.ExecuteScalarAsync<TKey>(sql, entity);
        entity.Id = id;
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : IEntity<TKey>
    {
        using var connection = _connectionFactory.Create();
        var tableName = GetTableName();
        var sql = $"SELECT * FROM {tableName}";
        return await connection.QueryAsync<TEntity>(sql);
    }

    public async Task<TEntity> GetByIdAsync<TEntity>(TKey id) where TEntity : IEntity<TKey>
    {
        using var connection = _connectionFactory.Create();
        var tableName = GetTableName();
        var sql = $"SELECT * FROM {tableName} WHERE Id = @Id";
        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        return await connection.QueryFirstOrDefaultAsync<TEntity>(sql, parameters);
    }

    private IList<PropertyInfo> GetProperties<TEntity>()
    {
        return typeof(TEntity).GetProperties().Where(p => p.Name != "Id").ToList();
    }
}
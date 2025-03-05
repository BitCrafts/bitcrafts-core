using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Abstraction.Entities;
using Dapper;

namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public abstract class BaseRepository<TEntity> : IRepository<TEntity>
    where TEntity : IEntity<int>
{
    private readonly IDatabaseManager _databaseManager;

    protected BaseRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public abstract string GetTableName();


    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        using var connection = await _databaseManager.OpenNewConnection();
        return await connection.QueryAsync<TEntity>(
            $"SELECT * FROM {GetTableName()}");
    }

    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        using var connection = await _databaseManager.OpenNewConnection();
        return await connection.QuerySingleAsync<TEntity>(
            $"SELECT * FROM {GetTableName()} WHERE Id = @Id",
            new { Id = id });
    }

    public abstract Task<int> AddAsync(TEntity entity);
    public abstract Task<int> UpdateAsync(TEntity entity);

    public virtual async Task<int> DeleteAsync(int id)
    {
        using var connection = await _databaseManager.OpenNewConnection();
        return await connection.ExecuteAsync($"DELETE FROM {GetTableName()} WHERE Id = @Id", new { Id = id });
    }
}
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    Task<bool> DeleteAsync(TKey id, IDatabaseTransaction transaction = null);

    Task<bool> UpdateAsync(TEntity entity, IDatabaseTransaction transaction = null);

    Task<TEntity> AddAsync(TEntity entity, IDatabaseTransaction transaction = null);

    Task<IEnumerable<TEntity>> GetAllAsync(IDatabaseTransaction transaction = null);

    Task<TEntity> GetByIdAsync(TKey id, IDatabaseTransaction transaction = null);
}
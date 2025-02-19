using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    Task<bool> DeleteAsync(TKey id);

    Task<bool> UpdateAsync(TEntity entity);

    Task<TEntity> AddAsync(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> GetByIdAsync(TKey id);
}
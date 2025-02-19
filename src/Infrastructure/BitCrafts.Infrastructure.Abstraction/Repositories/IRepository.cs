using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public interface IRepository<TKey>
{
    Task<bool> DeleteAsync(TKey id);

    Task<bool> UpdateAsync<TEntity>(TEntity entity) where TEntity : IEntity<TKey>;

    Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : IEntity<TKey>;

    Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : IEntity<TKey>;

    Task<TEntity> GetByIdAsync<TEntity>(TKey id) where TEntity : IEntity<TKey>;
}
using BitCrafts.Core.Contracts.Entities;

namespace BitCrafts.Core.Contracts.Repositories;

public interface IRepository<TEntity, TKey> : IDisposable
    where TEntity : class, IEntity<TKey>

{
    Task<IEnumerable<TEntity>> SearchAsync(RepositorySearchParameter parameter);
    Task<bool> DeleteAsync(TKey id);

    Task<bool> UpdateAsync(TEntity entity);

    Task<TEntity> AddAsync(TEntity entity);

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> GetByIdAsync(TKey id);
}
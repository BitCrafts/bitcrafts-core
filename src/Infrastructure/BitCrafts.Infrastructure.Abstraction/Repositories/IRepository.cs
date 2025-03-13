using System.Linq.Expressions;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public interface IRepository<T> where T : class, IEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
}
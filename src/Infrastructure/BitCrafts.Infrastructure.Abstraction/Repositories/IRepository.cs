using System.Linq.Expressions;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public interface IRepository<T> where T : class, IEntity
{
    T GetById(int id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    T Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}

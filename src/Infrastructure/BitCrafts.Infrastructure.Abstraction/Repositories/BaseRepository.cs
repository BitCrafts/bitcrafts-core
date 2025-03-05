using System.Linq.Expressions;
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Abstraction.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public abstract class Repository<TContext, TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public Repository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public TEntity GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate).ToList();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Remove(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }
}
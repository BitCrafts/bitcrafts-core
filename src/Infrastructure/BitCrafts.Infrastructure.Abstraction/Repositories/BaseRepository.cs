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

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var result = await _dbSet.FindAsync(id).ConfigureAwait(false);
        return result;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _dbSet.Where(predicate).ToListAsync().ConfigureAwait(false);
        return result;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity).ConfigureAwait(false);
        return result.Entity;
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities).ConfigureAwait(false);
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
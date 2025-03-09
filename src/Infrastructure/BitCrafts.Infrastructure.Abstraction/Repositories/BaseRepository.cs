using System.Linq.Expressions;
using BitCrafts.Infrastructure.Abstraction.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public abstract class Repository<TContext, TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    where TContext : DbContext
{
    protected readonly TContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public Repository(TContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        var result = await DbSet.FindAsync(id).ConfigureAwait(false);
        return result;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await DbSet.Where(predicate).ToListAsync().ConfigureAwait(false);
        return result;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var result = await DbSet.AddAsync(entity).ConfigureAwait(false);
        return result.Entity;
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities).ConfigureAwait(false);
    }

    public void Update(TEntity entity)
    {
        var existingEntity = DbSet.Find(entity.Id);

        if (existingEntity != null)
        {
            Context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
        else
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }
    }

    public void Remove(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        DbSet.RemoveRange(entities);
    }
}
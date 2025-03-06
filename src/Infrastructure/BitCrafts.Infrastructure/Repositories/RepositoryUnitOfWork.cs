using BitCrafts.Infrastructure.Abstraction.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BitCrafts.Infrastructure.Repositories;

public sealed class RepositoryUnitOfWork : IRepositoryUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;
    private DbContext _dbContext;

    public RepositoryUnitOfWork(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetDbContext(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public T GetRepository<T>()
    {
        return (T)_serviceProvider.GetService(typeof(T));
    }

    public int Commit()
    {
        return _dbContext.SaveChanges();
    }
}
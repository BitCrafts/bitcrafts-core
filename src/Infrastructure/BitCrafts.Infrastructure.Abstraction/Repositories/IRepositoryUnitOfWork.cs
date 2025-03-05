using Microsoft.EntityFrameworkCore;

namespace BitCrafts.Infrastructure.Abstraction.Repositories;

public interface IRepositoryUnitOfWork : IDisposable
{
        void SetDbContext(DbContext dbContext);

        T GetRepository<T>();
        int Commit();
     

}
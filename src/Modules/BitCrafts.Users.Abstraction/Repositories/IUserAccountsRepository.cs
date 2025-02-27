using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Repositories;

public interface IUserAccountsRepository : IRepository<IUserAccount, int>
{
    Task<IUserAccount> GetByUserIdAsync(int userId, IDatabaseTransaction transaction = null);
}
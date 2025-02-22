using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Repositories;

public interface IUsersRepository : IRepository<IUser,int>
{
    
}
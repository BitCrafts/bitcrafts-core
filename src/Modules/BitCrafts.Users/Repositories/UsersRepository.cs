using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Repositories;

namespace BitCrafts.Users.Repositories;

public class UsersRepository : Repository<UsersDbContext, User>, IUsersRepository
{
    public UsersRepository(UsersDbContext context) : base(context)
    {
    }
}
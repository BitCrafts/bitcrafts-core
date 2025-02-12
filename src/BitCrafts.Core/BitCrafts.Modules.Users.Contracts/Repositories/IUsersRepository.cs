using BitCrafts.Modules.Users.Contracts.Models;

namespace BitCrafts.Modules.Users.Contracts.Repositories;

public interface IUsersRepository
{
    IEnumerable<IUserEntity> GetAllUsers();
    void AddUser(IUserEntity user);
    void DeleteUser(int id);

}
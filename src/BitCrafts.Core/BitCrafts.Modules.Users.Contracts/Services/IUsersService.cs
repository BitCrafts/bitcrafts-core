using BitCrafts.Modules.Users.Contracts.Models;

namespace BitCrafts.Modules.Users.Contracts.Services;

public interface IUsersService
{
    List<IUserEntity> GetAllUsers();
    void AddUser(IUserEntity user);

    void DeleteUser(int id);
}
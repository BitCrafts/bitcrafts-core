using BitCrafts.Modules.Users.Contracts.Models;

namespace BitCrafts.Modules.Users.Contracts.Services;

public interface IUsersService
{
    IList<IUserModel> GetAllUsers();
    void AddUser(IUserModel user);

    void DeleteUser(int id);
}
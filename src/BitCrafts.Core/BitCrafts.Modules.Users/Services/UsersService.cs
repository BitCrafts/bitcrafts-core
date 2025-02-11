using BitCrafts.Modules.Users.Contracts.Models;
using BitCrafts.Modules.Users.Contracts.Services;
using Serilog;

namespace BitCrafts.Modules.Users.Services;

public class UsersService : IUsersService
{
    private readonly ILogger _logger;
    private readonly IList<IUserModel> _users;

    public UsersService(ILogger logger)
    {
        _logger = logger;
        _users = new List<IUserModel>();
    }

    public IList<IUserModel> GetAllUsers()
    {
        return _users;
    }

    public void AddUser(IUserModel user)
    {
        user.Id = _users.Count + 1;
        _users.Add(user);
        _logger.Information($"User added ID : {user.Id}");
    }
}
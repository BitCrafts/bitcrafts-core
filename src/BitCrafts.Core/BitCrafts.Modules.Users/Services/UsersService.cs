using BitCrafts.Modules.Users.Contracts.Models;
using BitCrafts.Modules.Users.Contracts.Repositories;
using BitCrafts.Modules.Users.Contracts.Services;
using Serilog;

namespace BitCrafts.Modules.Users.Services;

public class UsersService : IUsersService
{
    private readonly ILogger _logger;
    private readonly IUsersRepository _usersRepository;

    public UsersService(ILogger logger, IUsersRepository usersRepository)
    {
        _logger = logger;
        _usersRepository = usersRepository;
    }

    public List<IUserEntity> GetAllUsers()
    {
        return _usersRepository.GetAllUsers().ToList();
    }

    public void AddUser(IUserEntity user)
    {
        _usersRepository.AddUser(user);
        _logger.Information($"Nouvel utilisateur ajouté avec l’ID : {user.PrimaryKey}");
    }

    public void DeleteUser(int id)
    {
        _usersRepository.DeleteUser(id);
    }
}
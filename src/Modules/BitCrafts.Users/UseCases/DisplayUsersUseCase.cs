using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Entities;

namespace BitCrafts.Users.UseCases;

public sealed class DisplayUsersUseCase : BaseUseCase<DisplayUsersEventRequest, DisplayUsersEventResponse>,
    IDisplayUsersUseCase
{
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public DisplayUsersUseCase(IRepositoryUnitOfWork repositoryUnitOfWork,
        UsersDbContext usersDbContext)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _repositoryUnitOfWork.SetDbContext(usersDbContext);
    }

    private async Task<IEnumerable<User>> GetUsers()
    {
        var repository = _repositoryUnitOfWork.GetRepository<IUsersRepository>();
        var result = await repository.GetAllAsync();
        return result;
    }

    protected override async Task<DisplayUsersEventResponse> ExecuteCore(DisplayUsersEventRequest eventRequest)
    {
        var result = await GetUsers();
        var response = new DisplayUsersEventResponse(result);
        return response;
    }
}
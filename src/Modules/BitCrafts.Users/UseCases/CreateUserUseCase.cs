using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Services;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;

namespace BitCrafts.Users.UseCases;

public sealed class CreateUserUseCase : BaseUseCase<CreateUserEventRequest, CreateUserEventResponse>, ICreateUserUseCase
{
    private readonly IHashingService _hashingService;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public CreateUserUseCase(IHashingService hashingService,
        UsersDbContext dbContext, IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _hashingService = hashingService;
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _repositoryUnitOfWork.SetDbContext(dbContext);
    }

    private async Task<User> CreateUser(CreateUserEventRequest eventRequest)
    {
        var salt = _hashingService.GenerateSalt();
        var hashedPassword = _hashingService.HashPassword(eventRequest.Password);
        var userAccount = new UserAccount
        {
            HashedPassword = hashedPassword,
            PasswordSalt = salt
        };
        var user = eventRequest.User;
        user.UserAccount = userAccount;
        user = await _repositoryUnitOfWork.GetRepository<IUsersRepository>().AddAsync(user).ConfigureAwait(false);
        await _repositoryUnitOfWork.CommitAsync().ConfigureAwait(false);

        return user;
    }

    protected override async Task<CreateUserEventResponse> ExecuteCore(CreateUserEventRequest eventRequest)
    {
        var response = new CreateUserEventResponse();
        response.User = await CreateUser(eventRequest);
        return response;
    }
}
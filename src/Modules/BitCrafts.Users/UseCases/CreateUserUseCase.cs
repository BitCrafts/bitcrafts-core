using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Services;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users.UseCases;

public sealed class CreateUserUseCase : BaseUseCase<UserEventRequest, UserEventResponse>, ICreateUserUseCase
{
    private readonly IHashingService _hashingService;
    private readonly UsersDbContext _dbContext;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public CreateUserUseCase(IServiceProvider serviceProvider, IHashingService hashingService,
        UsersDbContext dbContext, IRepositoryUnitOfWork repositoryUnitOfWork) : base(serviceProvider)
    {
        _hashingService = hashingService;
        _dbContext = dbContext;
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _repositoryUnitOfWork.SetDbContext(dbContext);
    }

    private async Task<User> CreateUser(UserEventRequest eventRequest)
    {
        string salt = _hashingService.GenerateSalt();
        string hashedPassword = _hashingService.HashPassword(eventRequest.Password);
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

    protected override async Task<UserEventResponse> ExecuteCore(UserEventRequest eventRequest)
    {
        var response = new UserEventResponse();
        response.User = await CreateUser(eventRequest);
        return response;
    }
}
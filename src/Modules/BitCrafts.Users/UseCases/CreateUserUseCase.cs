using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Services;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;

namespace BitCrafts.Users.UseCases;

public sealed class CreateUserUseCase : BaseUseCase<CreateUserUseCaseInput>, ICreateUserUseCase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IHashingService _hashingService;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public CreateUserUseCase(IHashingService hashingService, IEventAggregator eventAggregator,
        UsersDbContext dbContext, IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _hashingService = hashingService;
        _eventAggregator = eventAggregator;
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _repositoryUnitOfWork.SetDbContext(dbContext);
    }

    protected override async Task ExecuteCore(CreateUserUseCaseInput input)
    {
        var salt = _hashingService.GenerateSalt();
        var hashedPassword = _hashingService.HashPassword(input.Password);
        var userAccount = new UserAccount
        {
            HashedPassword = hashedPassword,
            PasswordSalt = salt
        };
        User user;
        user = input.User;
        user.UserAccount = userAccount;
        user = await _repositoryUnitOfWork.GetRepository<IUsersRepository>().AddAsync(user).ConfigureAwait(false);
        var result = await _repositoryUnitOfWork.CommitAsync().ConfigureAwait(false);

        _eventAggregator.Publish(new CreateUserEvent(user, result > 0));
    }
}
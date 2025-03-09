using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;

namespace BitCrafts.Users.UseCases;

public sealed class DisplayUsersUseCase : BaseUseCase, IDisplayUsersUseCase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public DisplayUsersUseCase(IRepositoryUnitOfWork repositoryUnitOfWork, IEventAggregator eventAggregator,
        UsersDbContext usersDbContext)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _eventAggregator = eventAggregator;
        _repositoryUnitOfWork.SetDbContext(usersDbContext);
    }

    protected override async Task ExecuteCore()
    {
        var repository = _repositoryUnitOfWork.GetRepository<IUsersRepository>();
        var result = await repository.GetAllAsync();
        _eventAggregator.Publish(new DisplayUsersEvent(result));
    }
}
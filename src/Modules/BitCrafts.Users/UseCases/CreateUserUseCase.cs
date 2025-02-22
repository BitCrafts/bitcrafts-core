using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.UseCases.Abstraction;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Users.UseCases;

public sealed class CreateUserUseCase : BaseUseCase<UserCreateEventRequest, UserCreateEventResponse>, ICreateUserUseCase
{
    private readonly IUsersRepository _usersRepository;

    public CreateUserUseCase(ILogger<BaseUseCase<UserCreateEventRequest, UserCreateEventResponse>> logger,
        IEventAggregator eventAggregator, IUsersRepository usersRepository) : base(logger, eventAggregator)
    {
        _usersRepository = usersRepository;
    }


    protected override async Task ExecuteCoreAsync(UserCreateEventRequest createEvent)
    {
        await _usersRepository.AddAsync(createEvent.User);
    }
}
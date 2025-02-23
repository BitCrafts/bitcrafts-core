using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.UseCases.Abstraction;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Users.UseCases;

public sealed class UpdateUserUseCase : BaseUseCase<UserEventRequest, UserEventResponse>, IUpdateUserUseCase
{
    private readonly IUsersRepository _usersRepository;

    public UpdateUserUseCase(ILogger<BaseUseCase<UserEventRequest, UserEventResponse>> logger,
        IEventAggregator eventAggregator,
        IUsersRepository usersRepository) : base(logger, eventAggregator)
    {
        _usersRepository = usersRepository;
    }

    protected override async Task ExecuteCoreAsync(UserEventRequest request)
    {
        await _usersRepository.UpdateAsync(request.User);
    }
}
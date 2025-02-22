using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Users.UseCases;

public sealed class UpdateUserUseCase : IUpdateUserUseCase<UserUpdateEventRequest, UserUpdateEventResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILogger<UpdateUserUseCase> _logger;

    public UpdateUserUseCase(IUsersRepository usersRepository,
        IEventAggregator eventAggregator,
        ILogger<UpdateUserUseCase> logger)
    {
        _usersRepository = usersRepository;
        _eventAggregator = eventAggregator;
        _logger = logger;
        _eventAggregator.Subscribe<UserUpdateEventRequest>(ExecuteUpdateUser);
    }

    private async Task ExecuteUpdateUser(UserUpdateEventRequest request)
    {
        _logger.LogInformation($"Handling UpdateUser event for user ID: {request.User?.Id}");

        if (request.User == null || request.User.Id <= 0)
        {
            _logger.LogWarning("UpdateUser event received with invalid or null User object.");
            return;
        }

        var result = await _usersRepository.UpdateAsync(request.User);
        if (result)
        {
            _logger.LogInformation($"User with ID {request.User.Id} successfully updated.");
        }
        else
        {
            _logger.LogWarning($"Failed to update User with ID {request.User.Id}.");
        }
    }

    public void Dispose()
    {
        _eventAggregator.Unsubscribe<UserUpdateEventRequest>(ExecuteUpdateUser);
    }

    public Task ExecuteAsync(UserUpdateEventRequest request)
    {
        return ExecuteUpdateUser(request);
    }
}
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Users.UseCases;

public sealed class DeleteUserUseCase : IDeleteUserUseCase<UserDeleteEventRequest, UserDeleteEventResponse>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILogger<DeleteUserUseCase> _logger;

    public DeleteUserUseCase(IUsersRepository usersRepository,
        IEventAggregator eventAggregator,
        ILogger<DeleteUserUseCase> logger)
    {
        _usersRepository = usersRepository;
        _eventAggregator = eventAggregator;
        _logger = logger;
        _eventAggregator.Subscribe<UserDeleteEventRequest>(ExecuteDeleteUser);
    }

    private async Task ExecuteDeleteUser(UserDeleteEventRequest request)
    {
        _logger.LogInformation($"Handling DeleteUser event for user ID: {request.UserId}");

        if (request.UserId <= 0)
        {
            _logger.LogWarning("DeleteUser event received with invalid UserId.");
            return;
        }

        var result = await _usersRepository.DeleteAsync(request.UserId);
        if (result)
        {
            _logger.LogInformation($"User with ID {request.UserId} successfully deleted.");
        }
        else
        {
            _logger.LogWarning($"Failed to delete User with ID {request.UserId}.");
        }
    }

    public void Dispose()
    {
        _eventAggregator.Unsubscribe<UserDeleteEventRequest>(ExecuteDeleteUser);
    }

    public Task ExecuteAsync(UserDeleteEventRequest request)
    {
        return ExecuteDeleteUser(request);
    }
}
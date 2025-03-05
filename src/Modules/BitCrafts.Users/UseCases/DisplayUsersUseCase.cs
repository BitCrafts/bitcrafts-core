using BitCrafts.UseCases.Abstraction;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.UseCases;

namespace BitCrafts.Users.UseCases;

public sealed class DisplayUsersUseCase : BaseUseCase<UserEventRequest, UserEventResponse>, ICreateUserUseCase
{
    public DisplayUsersUseCase(IServiceProvider provider) : base(provider)
    {
    }

    protected override async Task ExecuteCoreAsync(UserEventRequest createEvent)
    {
        await Task.CompletedTask;
    }
}
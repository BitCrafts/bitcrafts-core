using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users.UseCases;

public sealed class DisplayUsersUseCase : BaseUseCase<DisplayUsersEventRequest, DisplayUsersEventResponse>,
    IDisplayUsersUseCase
{
    public DisplayUsersUseCase(IServiceProvider provider) : base(provider)
    {
    }

    protected override Task<DisplayUsersEventResponse> ExecuteCoreAsync(DisplayUsersEventRequest createEvent)
    {
        var users = GetUsers();
        var response = new DisplayUsersEventResponse(users);
        return Task.FromResult(response);
    }

    private IEnumerable<User> GetUsers()
    {
        var uow = ServiceProdiver.GetRequiredService<IRepositoryUnitOfWork>();
        var db = ServiceProdiver.GetRequiredService<UsersDbContext>();
        uow.SetDbContext(db);
        var repository = uow.GetRepository<IUsersRepository>();
        var result = repository.GetAll();
        return result;
    }
}
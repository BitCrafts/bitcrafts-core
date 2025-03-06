using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Services;
using BitCrafts.UseCases.Abstraction;
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
    
    public CreateUserUseCase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _hashingService = ServiceProdiver.GetRequiredService<IHashingService>();
    }


    protected override Task<UserEventResponse> ExecuteCoreAsync(UserEventRequest @event)
    {
        var response = new UserEventResponse();
        response.User = CreateUser(@event);
        return Task.FromResult(response);
    }

    private User CreateUser(UserEventRequest @event)
    {
        string salt = _hashingService.GenerateSalt();
        string hashedPassword = _hashingService.HashPassword(@event.Password);
        var userAccount = new UserAccount
        {
            HashedPassword = hashedPassword,
            PasswordSalt = salt
        };
        var user = @event.User;
        using (var uow = ServiceProdiver.GetRequiredService<IRepositoryUnitOfWork>())
        {
            uow.SetDbContext(ServiceProdiver.GetService<UsersDbContext>());

            user.UserAccount = userAccount;
            user = uow.GetRepository<IUsersRepository>().Add(user);
            uow.Commit();
        }

        return user;
    }
}
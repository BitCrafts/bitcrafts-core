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


    protected override async Task ExecuteCoreAsync(UserEventRequest @event)
    {
        try
        {
            string salt = _hashingService.GenerateSalt();
            string hashedPassword = _hashingService.HashPassword(@event.Password);
            var userAccount = new UserAccount
            {
                HashedPassword = hashedPassword,
                PasswordSalt = salt
            };
            using (var uow = ServiceProdiver.GetRequiredService<IRepositoryUnitOfWork>())
            {
                uow.SetDbContext(ServiceProdiver.GetService<UsersDbContext>());
                var user = @event.User as User;
                user.UserAccount = userAccount;
                uow.GetRepository<IUsersRepository>().Add(user);
                uow.Commit();
            }
        }
        catch
        {
            throw;
        }

        await Task.CompletedTask;
    }
}
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Abstraction.Services;
using BitCrafts.UseCases.Abstraction;
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
            using (var dbContext = ServiceProdiver.GetRequiredService<UsersDbContext>())
            {
                var user = @event.User as User;
                user.UserAccount = userAccount;
                var userEntity = await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
            }
        }
        catch
        {
            throw;
        }
    }
}
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
    private readonly IUsersRepository _usersRepository;
    private readonly IHashingService _hashingService;
    private readonly IUserAccountsRepository _userAccountsRepository;


    public CreateUserUseCase(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _usersRepository = ServiceProdiver.GetRequiredService<IUsersRepository>();
        _hashingService = ServiceProdiver.GetRequiredService<IHashingService>();
        _userAccountsRepository = ServiceProdiver.GetRequiredService<IUserAccountsRepository>();
    }


    protected override async Task ExecuteCoreAsync(UserEventRequest @event)
    {
        using (var transaction =
               await ServiceProdiver.GetRequiredService<IDatabaseManager>()
                   .BeginTransactionAsync())
        {
            try
            {
                var addedUser = await _usersRepository.AddAsync(@event.User, transaction);

                string salt = _hashingService.GenerateSalt();
                string hashedPassword = _hashingService.HashPassword(@event.Password);

                var userAccount = new UserAccount
                {
                    UserId = addedUser.Id,
                    HashedPassword = hashedPassword,
                    PasswordSalt = salt
                };


                await _userAccountsRepository.AddAsync(userAccount, transaction);

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
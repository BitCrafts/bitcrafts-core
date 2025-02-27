using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Abstraction.Services;
using BitCrafts.UseCases.Abstraction;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users.UseCases;

public sealed class UpdateUserUseCase : BaseUseCase<UserEventRequest, UserEventResponse>, IUpdateUserUseCase
{
    private readonly IServiceProvider _provider;
    private readonly IUsersRepository _usersRepository;
    private readonly IUserAccountsRepository _userAccountsRepository;
    private readonly IHashingService _hashingService;

    public UpdateUserUseCase(IServiceProvider provider) : base(provider)
    {
        _provider = provider;
        _usersRepository = provider.GetRequiredService<IUsersRepository>();
        _userAccountsRepository = provider.GetRequiredService<IUserAccountsRepository>();
        _hashingService = provider.GetRequiredService<IHashingService>();
    }

    protected override async Task ExecuteCoreAsync(UserEventRequest request)
    {
        using (var transaction = await _provider.GetRequiredService<IDatabaseManager>().BeginTransactionAsync())
        {
            try
            {
                await _usersRepository.UpdateAsync(request.User, transaction);

                if (!string.IsNullOrEmpty(request.Password))
                {
                    string salt = _hashingService.GenerateSalt();
                    string hashedPassword =
                        _hashingService.HashPassword(request.Password);

                    var userAccount = new UserAccount
                    {
                        UserId = request.User.Id,
                        HashedPassword = hashedPassword,
                        PasswordSalt = salt
                    };

                    await _userAccountsRepository.UpdateAsync(userAccount, transaction);
                }

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
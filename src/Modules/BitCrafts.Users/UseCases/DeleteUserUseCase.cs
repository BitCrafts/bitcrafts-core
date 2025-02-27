using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.UseCases.Abstraction;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users.UseCases;

public sealed class DeleteUserUseCase : BaseUseCase<UserEventRequest, UserEventResponse>, IDeleteUserUseCase
{
    private readonly IServiceProvider _provider;
    private readonly IUsersRepository _usersRepository;
    private readonly IUserAccountsRepository _userAccountsRepository;

    public DeleteUserUseCase(IServiceProvider provider) : base(provider)
    {
        _provider = provider;
        _usersRepository = provider.GetRequiredService<IUsersRepository>();
        _userAccountsRepository = provider.GetRequiredService<IUserAccountsRepository>();
    }

    protected override async Task ExecuteCoreAsync(UserEventRequest request)
    {
        using (var transaction = await _provider.GetRequiredService<IDatabaseManager>().BeginTransactionAsync())
        {
            try
            {
                await _userAccountsRepository.DeleteAsync(request.User.Id, transaction);
                await _usersRepository.DeleteAsync(request.User.Id, transaction);
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
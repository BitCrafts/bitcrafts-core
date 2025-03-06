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


    protected override Task<UserEventResponse> ExecuteCoreAsync(UserEventRequest @eventRequest)
    {
        var response = new UserEventResponse();
        response.User = CreateUser(eventRequest);
        return Task.FromResult(response);
    }

    private User CreateUser(UserEventRequest eventRequest)
    {
        string salt = _hashingService.GenerateSalt();
        string hashedPassword = _hashingService.HashPassword(eventRequest.Password);
        var userAccount = new UserAccount
        {
            HashedPassword = hashedPassword,
            PasswordSalt = salt
        };
        var user = eventRequest.User;
        var dbContext = ServiceProdiver.GetService<UsersDbContext>();
        var uow = ServiceProdiver.GetRequiredService<IRepositoryUnitOfWork>();
        uow.SetDbContext(dbContext);
        user.UserAccount = userAccount;
        user = uow.GetRepository<IUsersRepository>().Add(user);
        uow.Commit();

        return user;
    }
}
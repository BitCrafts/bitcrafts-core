using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Users.Abstraction;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Repositories;
using BitCrafts.Users.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users;

public class UsersModule : IUsersModule
{
    public string Name { get; } = "Users";

    public void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<ICreateUserUseCase,CreateUserUseCase>();
    }
}
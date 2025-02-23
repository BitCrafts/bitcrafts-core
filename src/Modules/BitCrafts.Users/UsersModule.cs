using BitCrafts.Infrastructure.Abstraction.Application.UI;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Users.Abstraction;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using BitCrafts.Users.Repositories;
using BitCrafts.Users.UseCases;
using BitCrafts.Users.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users;

public class UsersModule : IUsersModule
{
    public string Name { get; } = "Users";

    public void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<ICreateUserUseCase, CreateUserUseCase>();
        services.AddTransient<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddTransient<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddTransient<IUsersView, UsersView>();
    }

    public Type GetViewType()
    {
        return typeof(IUsersView);
    }
}
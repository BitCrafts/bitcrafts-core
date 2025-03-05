using BitCrafts.Users.Abstraction;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using BitCrafts.Users.Entities;
using BitCrafts.Users.Presenters;
using BitCrafts.Users.UseCases;
using BitCrafts.Users.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace BitCrafts.Users;

public class UsersModule : IUsersModule
{
    public string Name { get; } = "Users";

    public void RegisterServices(IServiceCollection services)
    {
        services.TryAddTransient<ICreateUserUseCase, CreateUserUseCase>();
        services.TryAddTransient<IDeleteUserUseCase, DeleteUserUseCase>();
        services.TryAddTransient<IUpdateUserUseCase, UpdateUserUseCase>();
        services.TryAddTransient<IUpdateUserUseCase, UpdateUserUseCase>();
        services.TryAddTransient<IUsersView, UsersView>();
        services.TryAddSingleton<IUsersPresenter, UsersPresenter>();
        services.AddDbContext<UsersDbContext>();
    }

    public Type GetViewType()
    {
        return typeof(IUsersView);
    }

    public Type GetPresenterType()
    {
        return typeof(IUsersPresenter);
    }
}
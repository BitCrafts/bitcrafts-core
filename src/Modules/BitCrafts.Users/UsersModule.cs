using BitCrafts.Users.Abstraction;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using BitCrafts.Users.Entities;
using BitCrafts.Users.Presenters;
using BitCrafts.Users.Repositories;
using BitCrafts.Users.UseCases;
using BitCrafts.Users.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BitCrafts.Users;

public class UsersModule : IUsersModule
{
    public string Name { get; } = "Users";

    public void RegisterServices(IServiceCollection services)
    {
        services.TryAddTransient<ICreateUserUseCase, CreateUserUseCase>();
        services.TryAddTransient<IDisplayUsersUseCase, DisplayUsersUseCase>();
        services.TryAddTransient<IUsersView, UsersView>();
        services.TryAddTransient<ICreateUserView,CreateUserView>();
        services.TryAddTransient<ICreateUserPresenter, CreateUserPresenter>();
        services.TryAddTransient<IUsersPresenter, UsersPresenter>();
        services.AddDbContext<UsersDbContext>();
        services.AddScoped<IUsersRepository, UsersRepository>();
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
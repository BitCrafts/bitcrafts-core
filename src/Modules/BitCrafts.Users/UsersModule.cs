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
        services.AddTransient<ICreateUserUseCase, CreateUserUseCase>();
        services.AddTransient<IDisplayUsersUseCase, DisplayUsersUseCase>();
        services.AddTransient<ICreateUserView, CreateUserView>();
        services.AddScoped<ICreateUserPresenter, CreateUserPresenter>();
        services.AddTransient<IUsersView, UsersView>();
        services.AddScoped<IUsersPresenter, UsersPresenter>();

        services.AddScoped<IUsersRepository, UsersRepository>();
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
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

namespace BitCrafts.Users;

public class UsersModule : IUsersModule
{
    private readonly string _name = "Users";

    public string Name => _name;

    public void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<ICreateUserUseCase, CreateUserUseCase>();
        services.AddTransient<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddTransient<IDisplayUsersUseCase, DisplayUsersUseCase>();
        services.AddTransient<ICreateUserView, CreateUserView>();
        services.AddTransient<ICreateUserPresenter, CreateUserPresenter>();
        services.AddTransient<IUsersView, UsersView>();
        services.AddTransient<IUsersPresenter, UsersPresenter>();
        services.AddTransient<IUsersRepository, UsersRepository>();
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
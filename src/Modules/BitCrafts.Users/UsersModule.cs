using BitCrafts.Users.Abstraction;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using BitCrafts.Users.Presenters;
using BitCrafts.Users.Repositories;
using BitCrafts.Users.UseCases;
using BitCrafts.Users.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users;

public sealed class UsersModule : IUsersModule
{
    private readonly string _name = "Users";

    public string Name => _name;

    public void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<ICreateUserUseCase, CreateUserUseCase>();
        services.AddTransient<IDeleteUserUseCase, DeleteUserUseCase>();
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
    public Type GetViewImplementationType()
    {
        return typeof(UsersView);
    }

    public Type GetPresenterType()
    {
        return typeof(IUsersPresenter);
    }
    
    public Type GetPresenterImplementationType()
    {
        return typeof(UsersPresenter);
    }
}
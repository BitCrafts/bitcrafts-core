using BitCrafts.Users.Abstraction;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.Repositories;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
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
        services.TryAddTransient<IUserAccountsRepository, UserAccountsRepository>();
        services.TryAddTransient<IUsersRepository, UsersRepository>();
        services.TryAddTransient<ICreateUserUseCase, CreateUserUseCase>();
        services.TryAddTransient<IDeleteUserUseCase, DeleteUserUseCase>();
        services.TryAddTransient<IUpdateUserUseCase, UpdateUserUseCase>();
        services.TryAddTransient<IUsersView, UsersView>();
        services.TryAddTransient<IUsersPresenter, UsersPresenter>();
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
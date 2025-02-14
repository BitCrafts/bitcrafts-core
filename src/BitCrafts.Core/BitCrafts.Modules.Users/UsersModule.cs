using BitCrafts.Core.Contracts.Modules;
using BitCrafts.Modules.Users.Contracts.Models;
using BitCrafts.Modules.Users.Contracts.Presenters;
using BitCrafts.Modules.Users.Contracts.Repositories;
using BitCrafts.Modules.Users.Contracts.Services;
using BitCrafts.Modules.Users.Contracts.Views;
using BitCrafts.Modules.Users.Models;
using BitCrafts.Modules.Users.Presenters;
using BitCrafts.Modules.Users.Repositories;
using BitCrafts.Modules.Users.Services;
using BitCrafts.Modules.Users.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Modules.Users;

public class UsersModule : IModule
{
    public string Name => "Users";

    public void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IUsersView, UsersView>();
        services.AddTransient<IUsersPresenter, UsersPresenter>();
        services.AddTransient<IUsersPresenterModel, UsersPresenterModel>();
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<IUsersRepository, UsersRepository>();
    }

    public (Type viewContract, Type viewImplementation) GetViewType()
    {
        return (typeof(IUsersView), typeof(UsersView));
    }

    public (Type presenterContract, Type presenterImplementation) GetPresenterType()
    {
        return (typeof(IUsersPresenter), typeof(UsersPresenter));
    }

    public Type GetModelType()
    {
        return typeof(IUsersPresenterModel);
    }
}
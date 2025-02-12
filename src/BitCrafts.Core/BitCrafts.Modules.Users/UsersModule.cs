using BitCrafts.Core.Contracts;
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

    public void RegisterServices(IIoCRegister ioCContainer)
    {
        ioCContainer.Register<IUsersView, UsersView>(ServiceLifetime.Transient);
        ioCContainer.Register<IUsersPresenter, UsersPresenter>(ServiceLifetime.Transient);
        ioCContainer.Register<IUsersPresenterModel, UsersPresenterModel>(ServiceLifetime.Transient);
        ioCContainer.Register<IUsersService, UsersService>(ServiceLifetime.Transient);
        ioCContainer.Register<IUsersRepository, UsersRepository>(ServiceLifetime.Transient);
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
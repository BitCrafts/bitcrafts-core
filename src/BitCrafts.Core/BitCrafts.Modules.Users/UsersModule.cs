using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Modules;
using BitCrafts.Modules.Users.Contracts.Presenters;
using BitCrafts.Modules.Users.Contracts.Views;
using BitCrafts.Modules.Users.Presenters;
using BitCrafts.Modules.Users.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Modules.Users;

public class UsersModule : IModule
{
    private string _name;

    public string Name => "Users";

    public void RegisterServices(IIoCContainer ioCContainer)
    {
        ioCContainer.Register<IUsersView, UsersView>(ServiceLifetime.Transient);

        ioCContainer.Register<IUsersPresenter, UsersPresenter>(ServiceLifetime.Transient);
    }
}
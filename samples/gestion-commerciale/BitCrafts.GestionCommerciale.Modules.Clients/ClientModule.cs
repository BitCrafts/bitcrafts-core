using BitCrafts.Core.Contracts.Modules;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Models;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Presenters;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Repositories;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Services;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Views;
using BitCrafts.GestionCommerciale.Modules.Clients.Models;
using BitCrafts.GestionCommerciale.Modules.Clients.Presenters;
using BitCrafts.GestionCommerciale.Modules.Clients.Repositories;
using BitCrafts.GestionCommerciale.Modules.Clients.Services;
using BitCrafts.GestionCommerciale.Modules.Clients.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.GestionCommerciale.Modules.Clients;

public class ClientModule : IModule
{
    public string Name => "Clients";

    public void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IClientService, ClientService>();
        services.AddTransient<IClientRepository, ClientRepository>();
        services.AddTransient<IGroupeClientRepository, GroupeClientRepository>();
        services.AddTransient<IMainControlPresenterModel, MainControlPresenterModel>();
        services.AddTransient<IMainControlView, MainControlView>();
        services.AddTransient<IMainControlPresenter, MainControlPresenter>();
    }

    public (Type viewContract, Type viewImplementation) GetViewType()
    {
        return (typeof(IMainControlView), typeof(MainControlView));
    }

    public (Type presenterContract, Type presenterImplementation) GetPresenterType()
    {
        return (typeof(IMainControlPresenter), typeof(MainControlPresenter));
    }

    public Type GetModelType()
    {
        return typeof(IMainControlPresenterModel);
    }
}
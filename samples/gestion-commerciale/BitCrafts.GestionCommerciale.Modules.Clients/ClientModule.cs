using BitCrafts.Core.Contracts.Modules;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Repositories;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Services;
using BitCrafts.GestionCommerciale.Modules.Clients.Repositories;
using BitCrafts.GestionCommerciale.Modules.Clients.Services;
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
    }
}
using BitCrafts.Core.Contracts.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.GestionCommerciale.Modules.Clients;

public class ClientModule : IModule
{
    public string Name => "Clients";

    public void RegisterServices(IServiceCollection services)
    {
        throw new NotImplementedException();
    }

    public (Type viewContract, Type viewImplementation) GetViewType()
    {
        throw new NotImplementedException();
    }

    public (Type presenterContract, Type presenterImplementation) GetPresenterType()
    {
        throw new NotImplementedException();
    }

    public Type GetModelType()
    {
        throw new NotImplementedException();
    }
}
using BitCrafts.Core.Contracts.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.GestionCommerciale.Modules.Commandes;

public class CommandeModule : IModule
{
    public string Name => "Commandes";

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
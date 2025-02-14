using BitCrafts.Core.Contracts.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.GestionCommerciale.Modules.Produits;

public class ProduitModule : IModule
{
    public string Name => "Produits";

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
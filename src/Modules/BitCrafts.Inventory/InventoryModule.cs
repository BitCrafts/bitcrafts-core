using BitCrafts.Inventory.Abstraction;
using BitCrafts.Inventory.Abstraction.Repositories;
using BitCrafts.Inventory.Abstraction.UseCases;
using BitCrafts.Inventory.Repositories;
using BitCrafts.Inventory.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Inventory;

public class InventoryModule : IInventoryModule
{
    public string Name { get; } = "Inventory";

    public void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IInventoryRepository, InventoryRepository>();
        services.AddTransient<ICreateInventoryUseCase, CreateInventoryUseCase>();
        services.AddTransient<IDeleteInventoryUseCase, DeleteInventoryUseCase>();
        services.AddTransient<IUpdateInventoryUseCase, UpdateInventoryUseCase>();
    }
}
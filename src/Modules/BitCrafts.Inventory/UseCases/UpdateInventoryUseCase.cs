using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Inventory.Abstraction.Events;
using BitCrafts.Inventory.Abstraction.Repositories;
using BitCrafts.Inventory.Abstraction.UseCases;
using BitCrafts.UseCases.Abstraction;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Inventory.UseCases;

public sealed class UpdateInventoryUseCase : BaseUseCase<InventoryEventRequest, InventoryEventResponse>,
    IUpdateInventoryUseCase
{
    private readonly IInventoryRepository _inventoryRepository;

    public UpdateInventoryUseCase(ILogger<BaseUseCase<InventoryEventRequest, InventoryEventResponse>> logger,
        IEventAggregator eventAggregator, IInventoryRepository inventoryRepository) : base(logger, eventAggregator)
    {
        _inventoryRepository = inventoryRepository;
    }

    protected override Task ExecuteCoreAsync(InventoryEventRequest request)
    {
        return ExecuteUpdateInventory(request);
    }

    private async Task ExecuteUpdateInventory(InventoryEventRequest request)
    {
        if (request.Inventory == null || request.Inventory.Id <= 0)
        {
            Logger.LogWarning("UpdateInventory event received with invalid or null Inventory object.");
            return;
        } 
        
        Logger.LogInformation($"Handling UpdateInventory event for ID: {request.Inventory.Id}");

        var result = await _inventoryRepository.UpdateAsync(request.Inventory);
        if (result)
        {
            Logger.LogInformation($"Successfully updated inventory with ID: {request.Inventory.Id}");
        }
        else
        {
            Logger.LogWarning($"Failed to update inventory with ID: {request.Inventory.Id}");
        }
    }
}
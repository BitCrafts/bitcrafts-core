using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Inventory.Abstraction.Events;
using BitCrafts.Inventory.Abstraction.Repositories;
using BitCrafts.Inventory.Abstraction.UseCases;
using BitCrafts.UseCases.Abstraction;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Inventory.UseCases;

public sealed class DeleteInventoryUseCase : BaseUseCase<InventoryEventRequest, InventoryEventResponse>,
    IDeleteInventoryUseCase
{
    private readonly IInventoryRepository _inventoryRepository;

    public DeleteInventoryUseCase(ILogger<BaseUseCase<InventoryEventRequest, InventoryEventResponse>> logger,
        IEventAggregator eventAggregator, IInventoryRepository inventoryRepository) : base(logger, eventAggregator)
    {
        _inventoryRepository = inventoryRepository;
    }

    private async Task ExecuteDeleteInventory(InventoryEventRequest request)
    {
        if (request.Inventory == null || request.Inventory.Id <= 0)
        {
            Logger.LogWarning("DeleteInventory event received with invalid InventoryId.");
            return;
        }

        Logger.LogInformation($"Handling DeleteInventory event for ID: {request.Inventory.Id}");

        var result = await _inventoryRepository.DeleteAsync(request.Inventory.Id);
        if (result)
        {
            Logger.LogInformation($"Successfully deleted inventory with ID: {request.Inventory.Id}");
        }
        else
        {
            Logger.LogWarning($"Failed to delete inventory with ID: {request.Inventory.Id}");
        }
    }

    protected override Task ExecuteCoreAsync(InventoryEventRequest createEvent)
    {
        return ExecuteDeleteInventory(createEvent);
    }
}
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Inventory.Abstraction.Events;
using BitCrafts.Inventory.Abstraction.Repositories;
using BitCrafts.Inventory.Abstraction.UseCases;
using BitCrafts.UseCases.Abstraction;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Inventory.UseCases;

public sealed class CreateInventoryUseCase :
    BaseUseCase<InventoryEventRequest, InventoryEventResponse>, ICreateInventoryUseCase
{
    private readonly IInventoryRepository _inventoryRepository;

    public CreateInventoryUseCase(
        ILogger<BaseUseCase<InventoryEventRequest, InventoryEventResponse>> logger,
        IEventAggregator eventAggregator, IInventoryRepository inventoryRepository) : base(logger, eventAggregator)
    {
        _inventoryRepository = inventoryRepository;
    }

    private async Task ExecuteCreateInventory(InventoryEventRequest request)
    {
        Logger.LogInformation($"Handling CreateInventory event for inventory: {request.Inventory.Name}");

        if (request.Inventory == null)
        {
            Logger.LogWarning("CreateInventory event received with null Inventory object.");
            return;
        }

        await _inventoryRepository.AddAsync(request.Inventory);
    }

    protected override Task ExecuteCoreAsync(InventoryEventRequest request)
    {
        return ExecuteCreateInventory(request);
    }
}
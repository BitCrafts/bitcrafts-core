using BitCrafts.Inventory.Abstraction.Events;
using BitCrafts.UseCases.Abstraction;

namespace BitCrafts.Inventory.Abstraction.UseCases;

public interface IDeleteInventoryUseCase : IUseCase<InventoryEventRequest, InventoryEventResponse>
{
}
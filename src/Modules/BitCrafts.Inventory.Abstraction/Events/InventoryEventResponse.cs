using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Inventory.Abstraction.Entities;

namespace BitCrafts.Inventory.Abstraction.Events;

public class InventoryEventResponse : BaseEventResponse,IEventResponse
{
    public IInventory Inventory { get; set; }
}
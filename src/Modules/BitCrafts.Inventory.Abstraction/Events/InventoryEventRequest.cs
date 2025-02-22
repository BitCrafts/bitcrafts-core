using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Inventory.Abstraction.Entities;

namespace BitCrafts.Inventory.Abstraction.Events;

public class InventoryEventRequest : BaseEventRequest,IEventRequest
{
    public IInventory Inventory { get; set; }
}
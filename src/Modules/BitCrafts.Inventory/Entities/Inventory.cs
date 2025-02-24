using BitCrafts.Infrastructure.Abstraction.Entities;
using BitCrafts.Inventory.Abstraction.Entities;

namespace BitCrafts.Inventory.Entities;

public class Inventory : BaseEntity<int>, IInventory
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
}
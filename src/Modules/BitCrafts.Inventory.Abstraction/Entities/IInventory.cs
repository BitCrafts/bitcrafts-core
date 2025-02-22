using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Inventory.Abstraction.Entities;

public interface IInventory : IEntity<int>
{
    string Name { get; set; }
    string Description { get; set; }
    string Location { get; set; } //fridge,pantry,freezer
    
}
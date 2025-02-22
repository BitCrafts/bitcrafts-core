using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Inventory.Abstraction.Entities;

namespace BitCrafts.Inventory.Abstraction.Repositories;

public interface IInventoryRepository : IRepository<IInventory,int>
{
    
}
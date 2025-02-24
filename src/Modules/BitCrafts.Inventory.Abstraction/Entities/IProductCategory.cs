using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Inventory.Abstraction.Entities;

public interface IProductCategory : IEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
}
using BitCrafts.Infrastructure.Abstraction.Entities;
using BitCrafts.Inventory.Abstraction.Entities;

namespace BitCrafts.Inventory.Entities;

public class ProductCategory : BaseEntity<int>, IProductCategory
{
    public string Name { get; set; }
    public string Description { get; set; }
}
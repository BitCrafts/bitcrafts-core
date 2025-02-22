using BitCrafts.Infrastructure.Abstraction.Entities;
using BitCrafts.Inventory.Abstraction.Entities;

namespace BitCrafts.Inventory.Entities;

public class Product : BaseEntity<int>, IProduct
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime ExpirationDate { get; set; }
}
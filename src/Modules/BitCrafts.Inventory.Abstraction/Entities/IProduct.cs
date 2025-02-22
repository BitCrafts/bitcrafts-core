using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Inventory.Abstraction.Entities;

public interface IProduct : IEntity<int>
{
    int CategoryId { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
    DateTime ExpirationDate { get; set; }
}
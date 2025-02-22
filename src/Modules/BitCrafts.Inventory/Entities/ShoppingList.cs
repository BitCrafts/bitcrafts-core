using BitCrafts.Infrastructure.Abstraction.Entities;
using BitCrafts.Inventory.Abstraction.Entities;

namespace BitCrafts.Inventory.Entities;

public class ShoppingList : BaseEntity<int>,IShoppingList
{
    public List<IProduct> Products { get; set; }
    public DateTime CreatedDate { get; init; }
    public string Notes { get; set; }
    public decimal EstimatedTotalCost { get; set; }

    public ShoppingList()
    {
        Products = new List<IProduct>();
    }
}
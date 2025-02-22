namespace BitCrafts.Inventory.Abstraction.Entities;

public interface IShoppingList
{
    public List<IProduct> Products { get; set; }
    public DateTime CreatedDate { get; init; }
    public string Notes { get; set; }  
    public decimal EstimatedTotalCost { get; set; } 

}
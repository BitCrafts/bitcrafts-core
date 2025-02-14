namespace BitCrafts.GestionCommerciale.Modules.Produits.Entities;

public class Produit
{
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Description { get; set; }
    public double Prix { get; set; }
    public int QuantiteEnStock { get; set; }
    public int CategorieId { get; set; } 
}
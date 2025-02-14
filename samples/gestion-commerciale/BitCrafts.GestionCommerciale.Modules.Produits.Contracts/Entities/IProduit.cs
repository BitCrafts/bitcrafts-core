namespace BitCrafts.GestionCommerciale.Modules.Produits.Contracts.Entities;

public interface IProduit
{
    int Id { get; set; }
    string Nom { get; set; }
    string Description { get; set; }
    double Prix { get; set; }
    int QuantiteEnStock { get; set; }
    int CategorieId { get; set; }
}
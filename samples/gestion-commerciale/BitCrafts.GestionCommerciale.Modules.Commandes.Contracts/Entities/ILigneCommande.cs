namespace BitCrafts.GestionCommerciale.Modules.Commandes.Contracts.Entities;

public interface ILigneCommande
{
    public int Id { get; set; }
    public int CommandeId { get; set; }
    public int ProduitId { get; set; }
    public int Quantite { get; set; }
    public double PrixUnitaire { get; set; }
}
namespace BitCrafts.GestionCommerciale.Modules.Commandes.Entities;

public class LigneCommande
{
    public int Id { get; set; }
    public int CommandeId { get; set; }
    public int ProduitId { get; set; }
    public int Quantite { get; set; }
    public double PrixUnitaire { get; set; }
}
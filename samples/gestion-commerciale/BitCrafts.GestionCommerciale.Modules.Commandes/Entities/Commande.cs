namespace BitCrafts.GestionCommerciale.Modules.Commandes.Entities;

public class Commande
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public DateTime DateCommande { get; set; }
    public DateTime? DateLivraison { get; set; }
    public string Etat { get; set; }
}
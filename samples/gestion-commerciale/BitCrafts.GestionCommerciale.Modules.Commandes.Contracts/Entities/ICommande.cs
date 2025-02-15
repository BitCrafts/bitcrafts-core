namespace BitCrafts.GestionCommerciale.Modules.Commandes.Contracts.Entities;

public interface ICommande
{
    int Id { get; set; }
    int ClientId { get; set; }
    DateTime DateCommande { get; set; }
    DateTime? DateLivraison { get; set; }
    string Etat { get; set; }
}
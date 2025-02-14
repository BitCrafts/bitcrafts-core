namespace BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Entities;

public interface IClient
{
    int Id { get; set; }
    string Nom { get; set; }
    string Adresse { get; set; }
    string Telephone { get; set; }
    string Email { get; set; }
    int GroupeId { get; set; }
}
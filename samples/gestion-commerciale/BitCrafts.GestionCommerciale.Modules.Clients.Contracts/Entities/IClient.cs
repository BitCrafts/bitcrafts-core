using BitCrafts.Core.Contracts.Entities;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Entities;

public interface IClient : IEntity<int>
{
    string Nom { get; set; }
    string Adresse { get; set; }
    string Telephone { get; set; }
    string Email { get; set; }
    int GroupeId { get; set; }
}
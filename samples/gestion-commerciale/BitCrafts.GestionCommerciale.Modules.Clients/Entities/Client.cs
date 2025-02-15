using BitCrafts.Core.Contracts.Entities;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Entities;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Entities;

public class Client : BaseEntity<int>, IClient
{
    public string Nom { get; set; }
    public string Adresse { get; set; }
    public string Telephone { get; set; }
    public string Email { get; set; }
    public int GroupeId { get; set; }
}
using BitCrafts.Core.Contracts.Entities;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Entities;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Entities;

public class GroupeClient : BaseEntity<int>, IGroupeClient
{
    public string Nom { get; set; }
}
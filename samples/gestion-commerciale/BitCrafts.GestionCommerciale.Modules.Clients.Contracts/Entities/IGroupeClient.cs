using BitCrafts.Core.Contracts.Entities;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Entities;

public interface IGroupeClient : IEntity<int>
{
    string Nom { get; set; }
}
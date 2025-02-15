using BitCrafts.Core.Contracts.Repositories;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Entities;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Repositories;

public interface IClientRepository : IRepository<IClient, int>
{
}
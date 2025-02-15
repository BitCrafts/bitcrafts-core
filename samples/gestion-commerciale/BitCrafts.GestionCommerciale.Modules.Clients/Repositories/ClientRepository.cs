using BitCrafts.Core.Contracts.Database;
using BitCrafts.Core.Contracts.Repositories;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Entities;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Repositories;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Repositories;

public sealed class ClientRepository : BaseRepository<IClient, int>, IClientRepository
{
    public ClientRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }
}
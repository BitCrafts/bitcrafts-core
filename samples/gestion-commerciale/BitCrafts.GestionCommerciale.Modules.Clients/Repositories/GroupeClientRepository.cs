using BitCrafts.Core.Contracts.Database;
using BitCrafts.Core.Contracts.Repositories;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Entities;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Repositories;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Repositories;

public class GroupeClientRepository : BaseRepository<IGroupeClient, int>, IGroupeClientRepository
{
    public GroupeClientRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
    {
    }
}
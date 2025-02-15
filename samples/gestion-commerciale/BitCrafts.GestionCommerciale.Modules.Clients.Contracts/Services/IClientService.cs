using BitCrafts.Core.Contracts.Repositories;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Entities;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Services;

public interface IClientService
{
    Task<IClient> CreateClientAsync(IClient client);
    Task<IGroupeClient> CreateClientGroupAsync(IGroupeClient group);
    Task<IEnumerable<IClient>> SearchClientsAsync(RepositorySearchParameter searchParameter);
    Task<IEnumerable<IClient>> GetAllClientsAsync();
    Task<IClient> UpdateClientAsync(IClient client);
    Task DeleteClientAsync(int clientId);
    Task<bool> AssignClientToGroupAsync(int clientId, int groupId);
}
using BitCrafts.Core.Contracts.Repositories;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Entities;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Repositories;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Services;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Services;

public sealed class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IGroupeClientRepository _groupeClientRepository;

    public ClientService(IClientRepository clientRepository, IGroupeClientRepository groupeClientRepository)
    {
        _clientRepository = clientRepository;
        _groupeClientRepository = groupeClientRepository;
    }

    public async Task<IClient> CreateClientAsync(IClient client)
    {
        var groupExists = await _groupeClientRepository.GetByIdAsync(client.GroupeId);
        if (groupExists == null) throw new ArgumentException($"Group with ID {client.GroupeId} does not exist.");

        await _clientRepository.AddAsync(client);

        return client;
    }

    public async Task<IGroupeClient> CreateClientGroupAsync(IGroupeClient group)
    {
        // Validation: Check if the group name is valid
        if (string.IsNullOrWhiteSpace(group.Nom)) throw new ArgumentException("The group name cannot be empty.");

        await _groupeClientRepository.AddAsync(group);

        return group;
    }

    public async Task<IEnumerable<IClient>> SearchClientsAsync(RepositorySearchParameter searchParameter)
    {
        searchParameter.Validate();

        return await _clientRepository.SearchAsync(searchParameter);
    }

    public async Task<IEnumerable<IClient>> GetAllClientsAsync()
    {
        return await _clientRepository.GetAllAsync();
    }

    public async Task<IClient> UpdateClientAsync(IClient client)
    {
        var existingClient = await _clientRepository.GetByIdAsync(client.Id);
        if (existingClient == null) throw new KeyNotFoundException($"No client found with ID {client.Id}.");

        var groupExists = await _groupeClientRepository.GetByIdAsync(client.GroupeId);
        if (groupExists == null) throw new ArgumentException($"Group with ID {client.GroupeId} does not exist.");

        await _clientRepository.UpdateAsync(client);

        return client;
    }

    public async Task DeleteClientAsync(int clientId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null) throw new KeyNotFoundException($"No client found with ID {clientId}.");

        await _clientRepository.DeleteAsync(clientId);
    }

    public async Task<bool> AssignClientToGroupAsync(int clientId, int groupId)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null) throw new KeyNotFoundException($"No client found with ID {clientId}.");

        var group = await _groupeClientRepository.GetByIdAsync(groupId);
        if (group == null) throw new ArgumentException($"Group with ID {groupId} does not exist.");

        client.GroupeId = groupId;

        var result = await _clientRepository.UpdateAsync(client).ConfigureAwait(false);

        return result;
    }
}
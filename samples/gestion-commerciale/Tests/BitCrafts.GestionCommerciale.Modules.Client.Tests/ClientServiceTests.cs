namespace BitCrafts.GestionCommerciale.Modules.Client.Tests;

[TestClass]
public class ClientServiceTests
{
    private IGroupeClientRepository _clientGroupRepositoryMock;
    private IClientRepository _clientRepositoryMock;
    private IClientService _clientService;

    [TestInitialize]
    public void Setup()
    {
        _clientRepositoryMock = Substitute.For<IClientRepository>();
        _clientGroupRepositoryMock = Substitute.For<IGroupeClientRepository>();
        _clientService = new ClientService(_clientRepositoryMock, _clientGroupRepositoryMock);
    }


    [TestMethod]
    public async Task CreateClientAsync_ShouldAddClient_WhenGroupExists()
    {
        // Arrange
        var client = Substitute.For<IClient>();
        client.GroupeId.Returns(1);

        var group = Substitute.For<IGroupeClient>();
        _clientGroupRepositoryMock.GetByIdAsync(1).Returns(group);

        // Act
        var result = await _clientService.CreateClientAsync(client);

        // Assert
        await _clientRepositoryMock.Received(1).AddAsync(client);
        Assert.AreEqual(client, result);
    }

    [TestMethod]
    public async Task CreateClientAsync_ShouldThrowException_WhenGroupDoesNotExist()
    {
        // Arrange
        var client = Substitute.For<IClient>();
        client.GroupeId.Returns(1);

        _clientGroupRepositoryMock.GetByIdAsync(1).Returns((IGroupeClient)null);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
            await _clientService.CreateClientAsync(client));
    }

    [TestMethod]
    public async Task SearchClientsAsync_ShouldReturnClients_WhenCalled()
    {
        // Arrange
        var searchParameter = new RepositorySearchParameter();
        var clients = new List<IClient> { Substitute.For<IClient>() };
        _clientRepositoryMock.SearchAsync(searchParameter).Returns(clients);

        // Act
        var result = await _clientService.SearchClientsAsync(searchParameter);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Count());
    }

    [TestMethod]
    public async Task GetAllClientsAsync_ShouldReturnAllClients()
    {
        // Arrange
        var clients = new List<IClient> { Substitute.For<IClient>(), Substitute.For<IClient>() };
        _clientRepositoryMock.GetAllAsync().Returns(clients);

        // Act
        var result = await _clientService.GetAllClientsAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public async Task UpdateClientAsync_ShouldUpdateClient_WhenClientAndGroupExist()
    {
        // Arrange
        var client = Substitute.For<IClient>();
        client.Id.Returns(1);
        client.GroupeId.Returns(2);

        var group = Substitute.For<IGroupeClient>();
        _clientRepositoryMock.GetByIdAsync(1).Returns(client);
        _clientGroupRepositoryMock.GetByIdAsync(2).Returns(group);

        // Act
        var result = await _clientService.UpdateClientAsync(client);

        // Assert
        await _clientRepositoryMock.Received(1).UpdateAsync(client);
        Assert.AreEqual(client, result);
    }

    [TestMethod]
    public async Task UpdateClientAsync_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange
        var client = Substitute.For<IClient>();
        client.Id.Returns(1);

        _clientRepositoryMock.GetByIdAsync(1).Returns((IClient)null);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () =>
            await _clientService.UpdateClientAsync(client));
    }

    [TestMethod]
    public async Task DeleteClientAsync_ShouldDeleteClient_WhenClientExists()
    {
        // Arrange
        var client = Substitute.For<IClient>();
        _clientRepositoryMock.GetByIdAsync(1).Returns(client);

        // Act
        await _clientService.DeleteClientAsync(1);

        // Assert
        await _clientRepositoryMock.Received(1).DeleteAsync(1);
    }

    [TestMethod]
    public async Task DeleteClientAsync_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange
        _clientRepositoryMock.GetByIdAsync(1).Returns((IClient)null);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () =>
            await _clientService.DeleteClientAsync(1));
    }

    [TestMethod]
    public async Task AssignClientToGroupAsync_ShouldAssignGroup_WhenClientAndGroupExist()
    {
        // Arrange
        var client = Substitute.For<IClient>();
        client.GroupeId.Returns(1);

        var group = Substitute.For<IGroupeClient>();
        _clientRepositoryMock.GetByIdAsync(1).Returns(client);
        _clientGroupRepositoryMock.GetByIdAsync(2).Returns(group);

        // Act
        var result = await _clientService.AssignClientToGroupAsync(1, 2);

        // Assert
        await _clientRepositoryMock.Received(1).UpdateAsync(client);
        Assert.AreEqual(2, client.GroupeId);
    }

    [TestMethod]
    public async Task AssignClientToGroupAsync_ShouldThrowException_WhenClientDoesNotExist()
    {
        // Arrange
        _clientRepositoryMock.GetByIdAsync(1).Returns((IClient)null);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<KeyNotFoundException>(async () =>
            await _clientService.AssignClientToGroupAsync(1, 2));
    }
}
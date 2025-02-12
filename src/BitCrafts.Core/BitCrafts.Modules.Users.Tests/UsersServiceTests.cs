using BitCrafts.Modules.Users.Contracts.Repositories;
using BitCrafts.Modules.Users.Models;
using BitCrafts.Modules.Users.Services;
using NSubstitute;
using Serilog;

namespace BitCrafts.Modules.Users.Tests;

[TestClass]
public class UsersServiceTests
{
    private ILogger _mockLogger;
    private IUsersRepository _mockRepository;
    private UsersService _service;

    [TestInitialize]
    public void Setup()
    {
        _mockLogger = Substitute.For<ILogger>();
        _mockRepository = Substitute.For<IUsersRepository>();
        _service = new UsersService(_mockLogger, _mockRepository);
    }

    [TestMethod]
    public void GetAllUsers_ShouldReturnEmptyListInitially()
    {
        // Act
        var users = _service.GetAllUsers();

        // Assert
        Assert.IsNotNull(users);
        Assert.AreEqual(0, users.Count); // Verify that the list is initially empty
    }

    [TestMethod]
    public void AddUser_ShouldAddUserToTheListAndLog()
    {
        // Arrange
        var user = new UserEntity() { FirstName = "Jean", LastName = "Dupont" };

        // Act
        _service.AddUser(user);

        // Assert
        Assert.AreEqual(1, _service.GetAllUsers().Count); // Verify that a user was added
        Assert.AreEqual(1, user.PrimaryKey); // Verify that user ID was set
        _mockLogger.Received(1).Information(Arg.Is<string>(s => s.Contains("User added ID"))); // Verify logging
    }
}
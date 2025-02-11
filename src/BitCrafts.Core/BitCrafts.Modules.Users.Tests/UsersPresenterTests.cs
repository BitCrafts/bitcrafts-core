using BitCrafts.Modules.Users.Contracts.Models;
using BitCrafts.Modules.Users.Contracts.Services;
using BitCrafts.Modules.Users.Contracts.Views;
using BitCrafts.Modules.Users.Models;
using BitCrafts.Modules.Users.Presenters;
using NSubstitute;

namespace BitCrafts.Modules.Users.Tests;

[TestClass]
public class UsersPresenterTests
{
    private IUsersService _mockService;
    private IUsersView _mockView;
    private UsersPresenter _presenter;

    [TestInitialize] // Exécuté avant chaque test
    public void Setup()
    {
        _mockService = Substitute.For<IUsersService>();
        _mockView = Substitute.For<IUsersView>();
        _presenter = new UsersPresenter(_mockService);
        _presenter.SetView(_mockView);
    }

    [TestMethod]
    public void LoadUsers_ShouldCallServiceAndUpdateView()
    {
        // Arrange
        var users = new List<IUserModel>
        {
            new UserModel { FirstName = "Jean", LastName = "Dupont", Email = "jean.dupont@example.com" }
        };
        _mockService.GetAllUsers().Returns(users);

        // Act
        _presenter.LoadUsers();

        // Assert
        _mockService.Received(1).GetAllUsers(); // Verify that the service was called
        _mockView.Received(1).DisplayUsers(users); // Verify that the view was updated
    }

    [TestMethod]
    public void AddUser_ShouldAddUserAndReloadUsers()
    {
        // Arrange 
        var users = new List<IUserModel> { new UserModel { FirstName = "Jean" } };
        _mockService.GetAllUsers().Returns(users);

        // Act
        _presenter.AddUser();

        // Assert
        _mockService.Received(1).AddUser(users[0]); // Verify that a user was added
        _mockService.Received(1).GetAllUsers(); // Verify that users list was reloaded
        _mockView.Received(1).DisplayUsers(users); // Verify that the view was updated
    }
}
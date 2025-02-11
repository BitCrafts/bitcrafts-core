using BitCrafts.Core.Contracts.Applications.Views;
using BitCrafts.Modules.Users.Contracts.Presenters;
using BitCrafts.Modules.Users.Contracts.Services;
using BitCrafts.Modules.Users.Contracts.Views;
using BitCrafts.Modules.Users.Models;

namespace BitCrafts.Modules.Users.Presenters;

public class UsersPresenter : IUsersPresenter
{
    private readonly IUsersService _usersService;
    private IUsersView _view;
    private int _selectedUserIndex;

    public UsersPresenter(IUsersService usersService)
    {
        _usersService = usersService;
    }

    public IUsersView View => _view;

    public void Initialize()
    {
        _view.OnLoaded += (sender, e) => LoadUsers();
        _view.UserAdded += (sender, e) => AddUser();
        _view.UserRemoved += (sender, e) => { DeleteUser(_selectedUserIndex); };
        _view.UserSelected += (sender, e) => { _selectedUserIndex = e; };
    }

    private void DeleteUser(int selectedUserIndex)
    {
        _usersService.DeleteUser(selectedUserIndex);
        LoadUsers();
    }

    public void SetView(IUsersView view)
    {
        _view = view;
        Initialize();
    }

    public void LoadUsers()
    {
        var users = _usersService.GetAllUsers();
        _view.DisplayUsers(users);
    }


    public void AddUser()
    {
        var newUser = new UserModel
        {
            FirstName = "Jean",
            LastName = "Dupont",
            Email = "jean.dupont@example.com",
            Phone = "0123456789"
        };

        _usersService.AddUser(newUser);
        LoadUsers();
    }
}
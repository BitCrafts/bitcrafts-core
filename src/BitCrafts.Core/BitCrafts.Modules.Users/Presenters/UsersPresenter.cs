using BitCrafts.Modules.Users.Contracts.Presenters;
using BitCrafts.Modules.Users.Contracts.Services;
using BitCrafts.Modules.Users.Contracts.Views;
using BitCrafts.Modules.Users.Models;

namespace BitCrafts.Modules.Users.Presenters;

public class UsersPresenter : IUsersPresenter
{
    private readonly IUsersService _usersService;
    private int _selectedUserIndex;

    public UsersPresenter(IUsersService usersService)
    {
        _usersService = usersService;
    }

    public IUsersView View { get; private set; }

    public void Initialize()
    {
        View.OnLoaded += (sender, e) => LoadUsers();
        View.UserAdded += (sender, e) => AddUser();
        View.UserRemoved += (sender, e) => { DeleteUser(_selectedUserIndex); };
        View.UserSelected += (sender, e) => { _selectedUserIndex = e; };
    }

    public void SetView(IUsersView view)
    {
        View = view;
        Initialize();
    }

    public void LoadUsers()
    {
        var users = _usersService.GetAllUsers();
        View.DisplayUsers(users);
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

    private void DeleteUser(int selectedUserIndex)
    {
        _usersService.DeleteUser(selectedUserIndex);
        LoadUsers();
    }
}
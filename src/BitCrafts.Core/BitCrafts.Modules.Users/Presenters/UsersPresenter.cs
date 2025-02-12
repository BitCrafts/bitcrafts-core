using BitCrafts.Modules.Users.Contracts.Models;
using BitCrafts.Modules.Users.Contracts.Presenters;
using BitCrafts.Modules.Users.Contracts.Services;
using BitCrafts.Modules.Users.Contracts.Views;
using BitCrafts.Modules.Users.Models;

namespace BitCrafts.Modules.Users.Presenters;

public sealed class UsersPresenter : IUsersPresenter
{
    private readonly IUsersService _usersService;
    public IUsersView View { get; private set; }
    public IUsersPresenterModel Model { get; private set; }

    public UsersPresenter(IUsersService usersService)
    {
        _usersService = usersService;
        Model = new UsersPresenterModel();
    }


    public void Initialize()
    {
        View.OnLoaded += ViewOnOnLoaded;
        View.UserAdded += ViewOnUserAdded;
        View.UserRemoved += ViewOnUserRemoved;
        View.UserSelected += ViewOnUserSelected;
    }

    private void ViewOnUserSelected(object sender, int e)
    {
        SelectUser(e);
    }

    private void ViewOnUserRemoved(object sender, int e)
    {
        DeleteUser(e);
    }

    private void ViewOnUserAdded(object sender, EventArgs e)
    {
        AddUser();
    }

    private void ViewOnOnLoaded(object sender, EventArgs e)
    {
        LoadUsers();
    }

    private void SelectUser(int selectedUserIndex)
    {
        if (selectedUserIndex >= 0 && selectedUserIndex < Model.Users.Count)
        {
            Model.SelectedUser = Model.Users[selectedUserIndex]; 
        }
    }

    public void SetView(IUsersView view)
    {
        View = view;
        Initialize();
    }


    public void LoadUsers()
    {
        Model.Users.Clear();
        Model.Users.AddRange(_usersService.GetAllUsers());
        View.DisplayUsers(Model.Users);
    }


    public void AddUser()
    {
        var newUser = new UserEntity
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

    public void Dispose()
    {
        if (View != null)
        {
            View.OnLoaded -= ViewOnOnLoaded;
            View.UserAdded -= ViewOnUserAdded;
            View.UserRemoved -= ViewOnUserRemoved;
            View.UserSelected -= ViewOnUserSelected;
        }
    }
}
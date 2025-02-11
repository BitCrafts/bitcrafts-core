using BitCrafts.Modules.Users.Contracts.Models;
using BitCrafts.Modules.Users.Contracts.Presenters;
using BitCrafts.Modules.Users.Contracts.Views;
using Gtk;

namespace BitCrafts.Modules.Users.Views;

public class UsersView : Box, IUsersView
{
    private IUsersPresenter _presenter;
    private Button _addButton;
    private ListBox _listUsers;
    public event EventHandler OnAddUser;
    public event EventHandler OnViewLoaded;

    public UsersView(IUsersPresenter presenter)
    {
        _presenter = presenter; 
        _presenter.SetView(this);
    }

    public void DisplayUsers(IList<IUserModel> users)
    {
        _listUsers.RemoveAll();

        foreach (var user in users)
        {
            var row = new ListBoxRow();
            var label = new Label();
            label.Label_ = $"[{user.Id}] {user.FirstName} {user.LastName} - {user.Email}";
            row.Child = label;
            _listUsers.Append(row);
        }

        _listUsers.Show();
        Show();
    }


    public void InitializeView()
    {
        OnShow += OnOnShow;
        SetOrientation(Orientation.Vertical);
        SetSpacing(5);
        _addButton = new Button();
        _addButton.Label = "Ajouter Utilisateur";
        _addButton.OnClicked += AddButtonOnOnClicked;

        _listUsers = new ListBox();
        Append(_addButton);
        Append(_listUsers);

        Show();
    }

    private void OnOnShow(Widget sender, EventArgs args)
    {
        OnViewLoaded?.Invoke(this, args);
    }

    private void AddButtonOnOnClicked(Button sender, EventArgs args)
    {
        OnAddUser?.Invoke(this, args);
    }

    public void ShowView()
    {
        Show();
        _presenter.LoadUsers();
    }

    public void CloseView()
    {
        Hide();
    }
}
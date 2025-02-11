using BitCrafts.Modules.Users.Contracts.Models;
using BitCrafts.Modules.Users.Contracts.Presenters;
using BitCrafts.Modules.Users.Contracts.Views;

namespace BitCrafts.Modules.Users.Views;

public class UsersView : Gtk.Box, IUsersView
{
    private readonly IUsersPresenter _presenter;
    [Gtk.Connect] private readonly Gtk.ListBox usersListbox;
    [Gtk.Connect] private readonly Gtk.Button addUserButton;
    [Gtk.Connect] private readonly Gtk.Button deleteUserButton;

    private UsersView(Gtk.Builder builder) :
        base(new Gtk.Internal.BoxHandle(builder.GetPointer("mainView"), false))
    {
        builder.Connect(this);
        usersListbox.OnShow += (sender, args) => OnLoaded?.Invoke(this, EventArgs.Empty);
        addUserButton.OnClicked += (sender, args) => UserAdded?.Invoke(this, EventArgs.Empty);
    }

    public UsersView(IUsersPresenter presenter) :
        this(new Gtk.Builder("UsersView.glade"))
    {
        _presenter = presenter;
        _presenter.SetView(this);
    }

    public void DisplayUsers(IList<IUserModel> users)
    {
        usersListbox.RemoveAll();
        foreach (var user in users)
        {
            var row = new Gtk.ListBoxRow();
            var label = new Gtk.Label();
            label.Label_ = $"[{user.Id}] {user.FirstName} {user.LastName} - {user.Email}";
            row.Child = label;
            usersListbox.Append(row);
        }
    }

    public void InitializeView()
    {
        _presenter.LoadUsers();

        ShowView();
    }

    public void ShowView()
    {
        Show();
    }

    public void CloseView()
    {
        Hide();
    }

    public event EventHandler UserAdded;
    public event EventHandler UserRemoved;
    public event EventHandler OnLoaded;
}
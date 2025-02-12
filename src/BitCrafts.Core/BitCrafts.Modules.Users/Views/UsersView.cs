using BitCrafts.Modules.Users.Contracts.Models;
using BitCrafts.Modules.Users.Contracts.Presenters;
using BitCrafts.Modules.Users.Contracts.Views;
using Gtk;
using Gtk.Internal;
using Box = Gtk.Box;
using Builder = Gtk.Builder;
using Button = Gtk.Button;
using Label = Gtk.Label;
using ListBox = Gtk.ListBox;
using ListBoxRow = Gtk.ListBoxRow;

namespace BitCrafts.Modules.Users.Views;

public class UsersView : Box, IUsersView
{
    private readonly IUsersPresenter _presenter;
    [Connect] private readonly Button addUserButton;
    [Connect] private readonly Button deleteUserButton;
    [Connect] private readonly ListBox usersListbox;
    private int _selectedRowIndex;

    private UsersView(Builder builder) :
        base(new BoxHandle(builder.GetPointer("mainView"), false))
    {
        builder.Connect(this);
        usersListbox.OnShow += (sender, args) => OnLoaded?.Invoke(this, EventArgs.Empty);
        addUserButton.OnClicked += (sender, args) => UserAdded?.Invoke(this, EventArgs.Empty);
        deleteUserButton.OnClicked += (sender, args) => UserRemoved?.Invoke(this, _selectedRowIndex);
        usersListbox.OnSelectedRowsChanged += UsersListboxOnOnSelectedRowsChanged;
    }

    public UsersView(IUsersPresenter presenter) :
        this(new Builder("UsersView.glade"))
    {
        _presenter = presenter;
        _presenter.SetView(this);
    }

    public void DisplayUsers(IEnumerable<IUserEntity> users)
    {
        usersListbox.RemoveAll();
        foreach (var user in users)
        {
            var row = new ListBoxRow();
            var label = new Label();
            label.Label_ = $"[{user.PrimaryKey}] {user.FirstName} {user.LastName} - {user.Email}";
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
    public event EventHandler<int> UserRemoved;
    public event EventHandler<int> UserSelected;
    public event EventHandler OnLoaded;

    private void UsersListboxOnOnSelectedRowsChanged(ListBox sender, EventArgs args)
    {
        var selectedRow = sender.GetSelectedRow();
        if (selectedRow != null)
        {
            _selectedRowIndex = selectedRow.GetIndex();
            if (_selectedRowIndex >= 0) UserSelected?.Invoke(this, _selectedRowIndex);
        }
    }
}
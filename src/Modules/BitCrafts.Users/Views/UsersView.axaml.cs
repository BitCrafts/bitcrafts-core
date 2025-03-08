using System.Collections.ObjectModel;
using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Abstraction.Avalonia.Views;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Views;
using BitCrafts.Users.Entities;

namespace BitCrafts.Users.Views;

public partial class UsersView : BaseView, IUsersView
{
    public event EventHandler SaveClicked;
    public event EventHandler CloseClicked;
    private ObservableCollection<User> _users = new ObservableCollection<User>();

    public void RefreshUsers(IEnumerable<User> users)
    {
        _users.Clear();
        foreach (var user in users)
        {
            _users.Add(user);
        }

        UsersDataGrid.ItemsSource = _users;
    }

    public UsersView()
    {
        InitializeComponent();
        UsersDataGrid.ItemsSource = _users;
    }

    public void AppendUser(User user)
    {
        _users.Add(user);
        UsersDataGrid.ItemsSource = _users;
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        SaveClicked?.Invoke(this, EventArgs.Empty);
    }

    private void Closebutton_OnClick(object sender, RoutedEventArgs e)
    {
        CloseClicked?.Invoke(this, EventArgs.Empty);
    }

    public override void SetBusy(string message)
    {
        LoadingOverlay.IsVisible = true;
        base.SetBusy(message);
    }

    public override void UnsetBusy()
    {
        LoadingOverlay.IsVisible = false;
        base.UnsetBusy();
    }
}
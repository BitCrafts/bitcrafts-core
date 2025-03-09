using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Abstraction.Avalonia.Views;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Views;

namespace BitCrafts.Users.Views;

public partial class UsersView : BaseView, IUsersView
{
    private readonly IEventAggregator _eventAggregator;
    private readonly ObservableCollection<User> _users;

    public UsersView(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
        _users = new ObservableCollection<User>();
        InitializeComponent();
        UsersDataGrid.ItemsSource = _users;
    }

    public event EventHandler SaveClicked;
    public event EventHandler CloseClicked;

    public void RefreshUsers(IEnumerable<User> users)
    {
        _users.Clear();
        foreach (var user in users) _users.Add(user);

        UsersDataGrid.ItemsSource = _users;
    }

    public void AppendUser(User user)
    {
        _users.Add(user);
        UsersDataGrid.ItemsSource = _users;
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

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        SaveClicked?.Invoke(this, EventArgs.Empty);
    }

    private void Closebutton_OnClick(object sender, RoutedEventArgs e)
    {
        CloseClicked?.Invoke(this, EventArgs.Empty);
    }

    private void UsersDataGrid_OnRowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
    {
        if (e.EditAction == DataGridEditAction.Commit)
        {
            var user = e.Row.DataContext as User;
            if (user != null)
                _eventAggregator.Publish<UpdateUserEvent>(new UpdateUserEvent(user));
        }
    }
}
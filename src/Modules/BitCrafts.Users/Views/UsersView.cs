using Avalonia.Controls;
using BitCrafts.Users.Abstraction.Views;

namespace BitCrafts.Users.Views;

public sealed class UsersView : IUsersView
{
    private readonly UserControl _usersView;

    public UsersView()
    {
        _usersView = new UsersNativeView();
    }

    public void Show()
    {
        _usersView.IsVisible = true;
    }

    public void Hide()
    {
        _usersView.IsVisible = false;
    }

    public T GetNativeView<T>() where T : class
    {
        return _usersView as T;
    }
}
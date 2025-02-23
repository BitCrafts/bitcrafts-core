using Avalonia.Controls;
using BitCrafts.Infrastructure.Abstraction.Application.UI;

namespace BitCrafts.Infrastructure.Application.Avalonia.Windows;

public class MainWindow : IWindow
{
    private readonly Window _window;

    public MainWindow()
    {
        _window = new MainNativeWindow();
    }

    public void Show()
    {
        _window.Show();
    }

    public void Hide()
    {
        _window.Hide();
    }

    public void Close()
    {
        _window.Close();
    }

    public T GetNativeWindow<T>() where T : class
    {
        return _window as T;
    }
}
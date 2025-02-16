using BitCrafts.Core.Presentation.Abstraction;
using BitCrafts.Core.Presentation.Abstraction.Views;

namespace BitCrafts.Core.ConsoleApplication.Windows;

public class MainWindow : IWindow
{
    private IView _currentView;

    public void Show()
    {
        Console.Clear();
        _currentView?.Render();
    }

    public void Close()
    {
        Environment.Exit(0);
    }

    public void SetContent(IView content)
    {
        _currentView = content;
        Show();
    }
}
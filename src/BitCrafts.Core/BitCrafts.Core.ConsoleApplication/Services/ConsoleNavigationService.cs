using BitCrafts.Core.Presentation.Abstraction;
using BitCrafts.Core.Presentation.Abstraction.Services;
using BitCrafts.Core.Presentation.Abstraction.Views;

namespace BitCrafts.Core.ConsoleApplication.Services;

public class ConsoleNavigationService : INavigationService
{
    private readonly IWindow _window;
    private readonly Stack<IView> _history = new Stack<IView>();

    public ConsoleNavigationService(IWindow window)
    {
        _window = window;
    }

    public void NavigateTo(IView view)
    {
        if (_window != null)
        {
            _history.Push(view);
            _window.SetContent(view);
        }
    }

    public void GoBack()
    {
        if (_history.Count > 1)
        {
            _history.Pop();
            var previousView = _history.Peek();
            _window.SetContent(previousView);
        }
    }
}
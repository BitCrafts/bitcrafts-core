using BitCrafts.Infrastructure.Abstraction.Application.UI;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public interface IUiManager : IDisposable
{
    Task StartAsync();
    Task ShowWindowAsync(IWindow window);
    Task CloseWindowAsync(IWindow window);
    Task LoadViewAsync(IView view);
    Task UnloadViewAsync(IView view);
}
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public interface IUiManager : IDisposable
{
    Task StartAsync(); 
    void SetMainWindow(IWindow window);
}
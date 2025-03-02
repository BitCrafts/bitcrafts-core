using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWindowingManager : IDisposable
{
    void ShowWindow(IView window,bool setAsMainWindow = false);
    void CloseWindow(IView window);
    void HideWindow(IView window);
    T GetWindow<T>() where T : IView;
    IReadOnlyCollection<IView> GetAllWindows(); 
}
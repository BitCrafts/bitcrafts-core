using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWindowingManager
{
    
    void ShowWindow(IWindow window); 
    void CloseWindow(IWindow window);
    void HideWindow(IWindow window); 
    T GetWindow<T>() where T : IWindow;
    IReadOnlyCollection<IWindow> GetAllWindows();
    void SetRootWindow(IWindow rootWindow);
}
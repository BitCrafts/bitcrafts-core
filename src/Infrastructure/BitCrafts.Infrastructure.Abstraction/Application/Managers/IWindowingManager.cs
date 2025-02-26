using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWindowingManager
{
    T CreateWindow<T>() where T : IWindow, new();
    void ShowWindow<T>() where T : IWindow, new();
    void ShowWindow(IWindow window); 
    void CloseWindow(IWindow window);
    void HideWindow<T>() where T : IWindow;
    void CloseWindow<T>() where T : IWindow;
    T GetWindow<T>() where T : IWindow;
    IReadOnlyCollection<IWindow> GetAllWindows();
    void SetRootWindow(IWindow rootWindow);
}
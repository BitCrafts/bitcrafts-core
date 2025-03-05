using BitCrafts.Infrastructure.Abstraction.Application.Presenters;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWindowManager : IDisposable
{
    /*void ShowWindow(IView window,bool setAsMainWindow = false);
    void CloseWindow(IView window);
    void HideWindow(IView window);
    T GetWindow<T>() where T : IView;
    IReadOnlyCollection<IView> GetAllWindows(); */

    void ShowWindow<TPresenter>() where TPresenter : IPresenter;
    void CloseWindow<TPresenter>() where TPresenter : IPresenter;
    void HideWindow<TPresenter>() where TPresenter : IPresenter;
}
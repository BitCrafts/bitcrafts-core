using BitCrafts.Infrastructure.Abstraction.Application.Presenters;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWorkspaceManager : IDisposable
{
    void ShowPresenter<TPresenter>() where TPresenter : class, IPresenter;
    void ShowPresenter(Type presenterType);
    void ClosePresenter(Type presenterType);
    void ClosePresenter<TPresenter>() where TPresenter : class, IPresenter;
}
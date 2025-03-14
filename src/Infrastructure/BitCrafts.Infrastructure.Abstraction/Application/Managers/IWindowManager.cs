using BitCrafts.Infrastructure.Abstraction.Application.Presenters;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWindowManager : IDisposable
{
    Task ShowPresenterAsync<TPresenter>() where TPresenter : class, IPresenter;
    void ClosePresenter<TPresenter>() where TPresenter : class, IPresenter;

    /* void ShowWindow<TPresenter>() where TPresenter : class, IPresenter;
     Task ShowDialogWindowAsync<TPresenter>() where TPresenter : class, IPresenter;
    void CloseWindow<TPresenter>() where TPresenter : class, IPresenter;
    void HideWindow<TPresenter>() where TPresenter : class, IPresenter;*/
}
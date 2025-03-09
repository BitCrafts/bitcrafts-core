using BitCrafts.Infrastructure.Abstraction.Application.Presenters;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWindowManager : IDisposable
{
    void ShowWindow<TPresenter>() where TPresenter : class, IPresenter;
    Task ShowDialogWindowAsync<TPresenter>() where TPresenter : class, IPresenter;
    void CloseWindow<TPresenter>() where TPresenter : class, IPresenter;
    void HideWindow<TPresenter>() where TPresenter : class, IPresenter;
}
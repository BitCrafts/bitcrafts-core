using BitCrafts.Infrastructure.Abstraction.Application.Presenters;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWindowManager : IDisposable
{
    void ShowWindow<TPresenter>() where TPresenter : IPresenter;
    Task ShowDialogWindowAsync<TPresenter>() where TPresenter : IPresenter;
    void CloseWindow<TPresenter>() where TPresenter : IPresenter;
    void HideWindow<TPresenter>() where TPresenter : IPresenter;
}
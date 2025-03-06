using BitCrafts.Infrastructure.Abstraction.Application.Presenters;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWindowManager : IDisposable
{
    void ShowWindow<TPresenter>(bool isModal = false) where TPresenter : IPresenter;
    void CloseWindow<TPresenter>() where TPresenter : IPresenter;
    void HideWindow<TPresenter>() where TPresenter : IPresenter;
}
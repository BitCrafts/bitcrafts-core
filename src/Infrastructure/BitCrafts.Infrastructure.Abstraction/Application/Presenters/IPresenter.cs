using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Abstraction.Application.Presenters;

public interface IPresenter<TView> : IDisposable
    where TView : IView
{
    TView View { get; }
    void Initialize();
    void Show();
    void Hide();
}
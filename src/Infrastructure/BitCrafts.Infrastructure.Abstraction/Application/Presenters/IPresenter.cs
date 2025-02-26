using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Abstraction.Application.Presenters;

public interface IPresenter<TView> : IDisposable
    where TView : IView
{
    TView View { get; }
    Task InitializeAsync();
    void Show();
    void Hide();
}
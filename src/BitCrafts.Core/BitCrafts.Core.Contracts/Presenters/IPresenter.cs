using BitCrafts.Core.Contracts.Views;

namespace BitCrafts.Core.Contracts.Presenters;

public interface IPresenter<TView> : IDisposable
    where TView : IView

{
    TView View { get; }
    void Initialize();
}
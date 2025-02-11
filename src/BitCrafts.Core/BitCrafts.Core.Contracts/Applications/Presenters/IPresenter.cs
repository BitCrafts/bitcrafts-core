using BitCrafts.Core.Contracts.Applications.Views;

namespace BitCrafts.Core.Contracts.Applications.Presenters;

public interface IPresenter<TView>
    where TView : class, IView
{
    TView View { get; }
    void Initialize();
    void SetView(TView view);
}
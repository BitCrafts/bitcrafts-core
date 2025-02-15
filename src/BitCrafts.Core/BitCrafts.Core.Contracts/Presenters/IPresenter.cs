namespace BitCrafts.Core.Contracts.Presenters;

public interface IPresenter<TView> : IDisposable
{
    TView View { get; }
    T GetNativeWidget<T>() where T : class;
    void ShowView();
}
namespace BitCrafts.Core.Presentation.Abstraction.Presenters;

public interface IPresenter<TView,TModel>
{
    TView View { get; }
    TModel Model { get; }
    void Initialize();

}
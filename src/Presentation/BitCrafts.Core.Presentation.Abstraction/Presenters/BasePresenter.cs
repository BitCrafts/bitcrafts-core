using BitCrafts.Core.Presentation.Abstraction.Views;

namespace BitCrafts.Core.Presentation.Abstraction.Presenters;

public abstract class BasePresenter<TView, TModel> : IPresenter<TView, TModel>
    where TView : class, IView
    where TModel : class
{
    public TView View { get; private set; }
    public TModel Model { get; private set; }

    protected BasePresenter(TView view, TModel model)
    {
        View = view;
        Model = model;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #region Abstract methods

    public abstract void Initialize();

    #endregion

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }
}
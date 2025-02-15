using BitCrafts.Core.Contracts.Views;

namespace BitCrafts.Core.Contracts.Presenters;

public abstract class BasePresenter<TView, TModel> : IPresenter<TView>
    where TView : class, IView<TModel>
{
    protected BasePresenter(TView view, TModel model)
    {
        View = view;
        View.Model = model;
        View.OnViewLoaded += ViewOnLoaded;
        View.OnViewClosing += ViewOnUnloaded;
    }

    public TView View { get; protected set; }

    public T GetNativeWidget<T>() where T : class
    {
        return View as T;
    }

    public void ShowView()
    {
        View.ShowView();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void ViewOnUnloaded(object sender, EventArgs e)
    {
        OnViewUnloaded();
    }

    private void ViewOnLoaded(object sender, EventArgs e)
    {
        OnViewLoaded();
        View.ShowView();
    }

    public abstract void OnViewLoaded();
    public abstract void OnViewUnloaded();

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            View.OnViewLoaded -= ViewOnLoaded;
            View.OnViewClosing -= ViewOnUnloaded;
            View.Dispose();
        }
    }
}
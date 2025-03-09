using BitCrafts.Infrastructure.Abstraction.Application.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application.Presenters;

public abstract class BasePresenter<TView> : IPresenter
    where TView : IView
{
    public BasePresenter(string title, TView view, ILogger<BasePresenter<TView>> logger)
    {
        Logger = logger;
        Title = title;
        View = view;
        View.SetTitle(Title);
        View.ViewLoadedEvent += OnViewLoaded;
        View.ViewClosedEvent += OnViewClosed;
        Logger.LogInformation($"Initializing {GetType().Name}");
    }

    public TView View { get; }
    public string Title { get; protected set; }
    protected ILogger<BasePresenter<TView>> Logger { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IView GetView()
    {
        return View;
    }

    protected virtual void OnViewClosed(object sender, EventArgs e)
    {
        Logger.LogInformation($"Closed {GetType().Name}");
    }

    protected virtual void OnViewLoaded(object sender, EventArgs e)
    {
        Logger.LogInformation($"Loaded {GetType().Name}");
        OnInitialize();
    }

    protected abstract void OnInitialize();

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            View.ViewLoadedEvent -= OnViewLoaded;
            View.ViewClosedEvent -= OnViewClosed;
            View.Dispose();
            Logger.LogInformation($"Disposed {GetType().Name}");
        }
    }
}
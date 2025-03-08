using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application.Presenters;

public abstract class BasePresenter<TView> : IPresenter
    where TView : IView
{
    protected IServiceProvider ServiceProvider { get; }
    public TView View { get; private set; }
    public string Title { get; protected set; }
    protected IWindowManager WindowManager => ServiceProvider.GetRequiredService<IWindowManager>();
    protected IWorkspaceManager WorkspaceManager => ServiceProvider.GetRequiredService<IWorkspaceManager>();
    protected IEventAggregator EventAggregator => ServiceProvider.GetRequiredService<IEventAggregator>();

    protected IBackgroundThreadDispatcher BackgroundThreadDispatcher =>
        ServiceProvider.GetRequiredService<IBackgroundThreadDispatcher>();

    protected ILogger<BasePresenter<TView>> Logger =>
        ServiceProvider.GetRequiredService<ILogger<BasePresenter<TView>>>();

    public BasePresenter(string title, IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Title = title;
        View = serviceProvider.GetRequiredService<TView>();
        View.SetTitle(Title);
        View.ViewLoadedEvent += OnViewLoaded;
        View.ViewClosedEvent += OnViewClosed;
        Logger.LogInformation($"Initializing {this.GetType().Name}");
        InitializeCore();
        
    }

    protected virtual void OnViewClosed(object sender, EventArgs e)
    {
        Logger.LogInformation($"Closed {this.GetType().Name}");
    }

    protected virtual void OnViewLoaded(object sender, EventArgs e)
    {
        Logger.LogInformation($"Loaded {this.GetType().Name}");
    }

    private void InitializeCore()
    {
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
            Logger.LogInformation($"Disposed {this.GetType().Name}");
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IView GetView()
    {
        return View;
    }
}
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application.Presenters;

public abstract class BasePresenter<TView> : IPresenter
    where TView : IView
{
    protected IServiceProvider ServiceProvider { get; }
    public IView View { get; private set; }
    public string Title { get; protected set; }
    public IWindowManager WindowManager => ServiceProvider.GetRequiredService<IWindowManager>();
    public IWorkspaceManager WorkspaceManager => ServiceProvider.GetRequiredService<IWorkspaceManager>();
    public ILogger<BasePresenter<TView>> Logger => ServiceProvider.GetRequiredService<ILogger<BasePresenter<TView>>>();

    public BasePresenter(string title, IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        Title = title;
        View = serviceProvider.GetRequiredService<TView>();
        View.SetTitle(Title);
        View.ViewLoadedEvent += OnViewLoaded;
        View.ViewClosedEvent += OnViewClosed;
        Logger.LogInformation($"Initializing {this.GetType().Name}");
    }

    protected virtual void OnViewClosed(object sender, EventArgs e)
    {
        Logger.LogInformation($"Closed {this.GetType().Name}");
    }

    protected virtual void OnViewLoaded(object sender, EventArgs e)
    {
        Logger.LogInformation($"Loaded {this.GetType().Name}");
    }

    public T GetView<T>() where T : IView
    {
        return (T)View;
    }

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
}
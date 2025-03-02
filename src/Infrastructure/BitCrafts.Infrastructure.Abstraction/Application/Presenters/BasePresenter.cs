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
    public IWindowingManager WindowingManager => ServiceProvider.GetRequiredService<IWindowingManager>();
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

    public async Task CloseAndOpenPresenterAsync<T>(bool isMainWindow = false)
    {
        await OpenPresenterAsync<T>(isMainWindow);
        await CloseAsync();
    }

    public async Task OpenPresenterAsync<T>(bool isMainWindow = false)
    {
        dynamic presenter = ServiceProvider.GetRequiredService<T>();
        await presenter.ShowAsync(isMainWindow);
    }

    public T GetView<T>() where T : IView
    {
        return (T)View;
    }

    public void SetView(IView view)
    {
        View = view;
    }

    public async Task ShowAsync(bool isMainWindow = false)
    {
        if (View.IsWindow)
        {
            WindowingManager.ShowWindow(View, isMainWindow);
        }
        else if (View is { } view)
        {
            await ServiceProvider.GetService<IWorkspaceManager>().ShowPresenterAsync(this);
        }
    }

    public async Task CloseAsync()
    {
        if (View.IsWindow)
        {
            WindowingManager.CloseWindow(View);
        }
        else if (View is { } view)
        {
            await ServiceProvider.GetService<IWorkspaceManager>().ClosePresenterAsync(this);
        }
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
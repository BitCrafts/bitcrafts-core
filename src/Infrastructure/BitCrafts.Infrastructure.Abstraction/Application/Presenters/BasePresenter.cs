using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application.Presenters;

public abstract class BasePresenter<TView> : IPresenter<TView>, IDisposable
    where TView : IView
{
    protected IServiceProvider ServiceProvider { get; }
    public TView View { get; }
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

    public async Task CloseAndOpenPresenterAsync<T>()
    {
        await OpenPresenterAsync<T>();
        await CloseAsync();
    }

    public async Task OpenPresenterAsync<T>()
    {
        dynamic presenter = ServiceProvider.GetRequiredService<T>();
        await presenter.ShowAsync();
    }

    public async Task ShowAsync()
    {
        Logger.LogInformation($"Showing View {View.GetType().Name}");
        if (View.IsWindow)
        {
            WindowingManager.ShowWindow(View);
        }
        else if (View is IView view)
        {
            await ServiceProvider.GetService<IWorkspaceManager>().ShowPresenterAsync(this.GetType());
        }
    }

    public async Task CloseAsync()
    {
        Logger.LogInformation($"Closing View {View.GetType().Name}");
        if (View.IsWindow)
        {
            WindowingManager.CloseWindow(View);
        }
        else if (View is IView view)
        {
            await ServiceProvider.GetService<IWorkspaceManager>().ClosePresenterAsync(this.GetType());
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
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
    public IWindowingManager WindowingManager => ServiceProvider.GetRequiredService<IWindowingManager>();
    public ILogger<BasePresenter<TView>> Logger => ServiceProvider.GetRequiredService<ILogger<BasePresenter<TView>>>();

    public BasePresenter(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        View = serviceProvider.GetRequiredService<TView>();
    }

    public virtual Task InitializeAsync()
    {
        Logger.LogInformation($"Initializing presenter {this.GetType().Name}");
        View.Initialize();
        return Task.CompletedTask;
    }

    public virtual void Show()
    {
        if (View is IWindow window)
        {
            WindowingManager.ShowWindow(window);
        }
    }

    public virtual void Hide()
    {
        View.Hide();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Logger.LogInformation($"Disposing presenter {this.GetType().Name}");
            View.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
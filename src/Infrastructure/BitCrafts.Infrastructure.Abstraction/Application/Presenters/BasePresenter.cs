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
        if (View is IWindow window)
        {
            window.WindowLoaded += OnWindowLoaded;
            window.WindowClosed += OnWindowClosed;
        }
    }

    protected abstract void OnWindowClosed(object sender, EventArgs e);
    protected abstract void OnWindowLoaded(object sender, EventArgs e);

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
            Logger.LogInformation($"Showing View {View.GetType().Name}");
            WindowingManager.ShowWindow(window);
        }
    }

    public virtual void Hide()
    {
        Logger.LogInformation($"Hiding View {View.GetType().Name}");

        View.Hide();
    }

    public void Close()
    {
        if (View is IWindow window)
        {
            window.Close();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (View is IWindow window)
            {
                window.WindowLoaded -= OnWindowLoaded;
                window.WindowClosed -= OnWindowClosed;
            }

            View.Dispose();
            Logger.LogInformation($"Disposed presenter {this.GetType().Name}");
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
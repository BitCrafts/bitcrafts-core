using Avalonia.Controls;
using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Abstraction.Avalonia.Views;

public abstract class BaseView : UserControl, IView
{
    private readonly IServiceProvider _serviceProvider;
    public event EventHandler ViewLoadedEvent;
    public event EventHandler ViewClosedEvent;
    protected IEventAggregator EventAggregator { get; private set; }
    protected IServiceProvider ServiceProvider { get; private set; }
    private string _title;
    private bool _isBusy;
    private string _busyMessage;
    private Window _window;

    protected BaseView()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    public void SetParentWindow(Window window)
    {
        _window = window;
    }
    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        ViewClosedEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewLoadedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void SetTitle(string title)
    {
        _title = title;
    }

    public virtual void SetBusy(string message)
    {
        _isBusy = true;
        _busyMessage = message;
    }

    public virtual void UnsetBusy()
    {
        _isBusy = false;
        _busyMessage = null;
    }

    public string GetTitle()
    {
        return _title;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Loaded -= OnLoaded;
            Unloaded -= OnUnloaded;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
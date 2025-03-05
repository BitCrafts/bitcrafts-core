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

    protected BaseView(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        EventAggregator = serviceProvider.GetRequiredService<IEventAggregator>();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
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
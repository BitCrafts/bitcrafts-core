using Avalonia.Controls;
using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Avalonia.Views;

public abstract class BaseView : UserControl, IView
{
    private string _title;
    public bool IsBusy { get; private set; }
    public string BusyMessage { get; private set; }
    public event EventHandler ViewLoadedEvent;
    public event EventHandler ViewClosedEvent;

    protected BaseView()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    public virtual void SetTitle(string title)
    {
        _title = title;
    }

    public virtual void SetBusy(string message)
    {
        IsBusy = true;
        BusyMessage = message;
    }

    public virtual void UnsetBusy()
    {
        IsBusy = false;
        BusyMessage = null;
    }

    public virtual string GetTitle()
    {
        return _title;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
    {
        ViewClosedEvent?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewLoadedEvent?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;
        Loaded -= OnLoaded;
        Unloaded -= OnUnloaded;
    }
}
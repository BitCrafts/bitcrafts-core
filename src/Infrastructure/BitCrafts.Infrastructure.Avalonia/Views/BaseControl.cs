using Avalonia.Controls;
using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Avalonia.Views;

public abstract class BaseControl : UserControl, IView
{
    public bool IsView { get; }
    public bool IsDialog { get; }
    public bool IsWindow { get; }
    public string Title { get; protected set; }
    public bool IsBusy { get; private set; }
    public string BusyText { get; private set; }

    protected BaseControl()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        Initialized += OnInitialized;
        IsDialog = false;
        IsWindow = false;
        IsView = true;
        Title = "UnTitled Control";
        BusyText = string.Empty;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        OnDisappearing();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        OnAppeared();
    }

    private void OnInitialized(object sender, EventArgs e)
    {
        OnAppearing();
    }

    protected abstract void OnAppearing();
    protected abstract void OnAppeared();
    protected abstract void OnDisappearing();


    public void SetBusy(string message)
    {
        IsBusy = true;
        BusyText = message;
    }

    public void UnsetBusy()
    {
        IsBusy = false;
        BusyText = string.Empty;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!disposing) return;
        Loaded -= OnLoaded;
        Unloaded -= OnUnloaded;
        Initialized -= OnInitialized;
    }
}
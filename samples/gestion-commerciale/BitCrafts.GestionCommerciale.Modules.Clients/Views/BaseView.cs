using Avalonia.Controls;
using Avalonia.Interactivity;
using BitCrafts.Core.Contracts.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Views;

public abstract class BaseView<TModel> : UserControl, IView<TModel>
{
    private readonly ILogger<BaseView<TModel>> _logger;

    protected BaseView(TModel model, ILogger<BaseView<TModel>> logger)
    {
        _logger = logger;
        Model = model;
    }

    public TModel Model { get; set; }
    public event EventHandler OnViewLoaded;
    public event EventHandler OnViewClosing;

    public void Dispose()
    {
        _logger.LogInformation($"{GetType().Name} Disposed");
    }

    public void ShowView()
    {
        IsVisible = true;
        _logger.LogInformation($"BaseView: {GetType().Name} Is Visible");
    }

    public void HideView()
    {
        IsVisible = false;
        _logger.LogInformation($"BaseView: {GetType().Name} Is Hidden");
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        OnViewLoaded?.Invoke(this, EventArgs.Empty);
        _logger.LogInformation($"{GetType().Name} OnLoaded");
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        base.OnUnloaded(e);
        OnViewClosing?.Invoke(this, EventArgs.Empty);
        _logger.LogInformation($"{GetType().Name} UnLoaded");
    }
}
using Avalonia.Controls;
using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Presenters;
using BitCrafts.Core.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace BitCrafts.Core.Presenters;

public sealed class MainPresenter : BasePresenter<IMainView, IMainPresenterModel>, IMainPresenter
{
    private readonly ILogger<MainPresenter> _logger;
    private readonly IModuleManager _moduleManager;

    public MainPresenter(IMainView mainView, IMainPresenterModel model, ILogger<MainPresenter> logger,
        IModuleManager moduleManager)
        : base(mainView, model)
    {
        _logger = logger;
        _moduleManager = moduleManager;
    }

    public void InitializeModules()
    {
        LoadModules();
        View.InitializeModules();
        _logger.LogInformation("Modules initialized.");
    }

    public override void OnViewLoaded()
    {
        _logger.LogInformation("MainPresenter.OnViewLoaded");
        InitializeModules();
    }

    public override void OnViewUnloaded()
    {
        _logger.LogInformation("MainPresenter.OnViewUnloaded");
    }

    private void LoadModules()
    {
        var modules = _moduleManager.GetModuleViewTypes();
        foreach (var (moduleName, (viewContract, viewImplementation, presenterContract, presenterImplementation,
                     modelType)) in modules)
            try
            {
                var widgetInstance =
                    ApplicationStartup.ServiceProvider.GetRequiredService(viewContract) as ContentControl;
                if (widgetInstance != null)
                    View.Model.Widgets.Add((moduleName, widgetInstance));
                else
                    _logger.LogWarning($"La vue du module '{moduleName}' n’hérite pas de Widget.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Impossible d’instancier la vue du module '{moduleName}': {ex.Message}");
            }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        Log.Logger.Information("MainPresenter disposed.");
    }
}
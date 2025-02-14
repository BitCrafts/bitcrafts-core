using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Views;
using BitCrafts.Core.Views;
using Gtk;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Core.Presenters;

public class MainPresenter : IMainPresenter
{
    private readonly ILogger<MainPresenter> _logger;
    private readonly IModuleManager _moduleManager;
    private readonly List<(string moduleName, Widget widget)> _moduleWidgets;
    public IMainView View { get; private set; }

    public MainPresenter(IMainView mainView, ILogger<MainPresenter> logger, IModuleManager moduleManager)
    {
        View = mainView;
        _logger = logger;
        _moduleManager = moduleManager;
        _moduleWidgets = new List<(string moduleName, Widget widget)>();
    }

    public List<(string moduleName, Widget widget)> GetResolvedWidgets()
    {
        return _moduleWidgets;
    }

    public void Initialize()
    {
        LoadModules();
    }

    private void LoadModules()
    {
        var modules = _moduleManager.GetModuleViewTypes();
        foreach (var (moduleName, (viewContract, viewImplementation, presenterContract, presenterImplementation,
                     modelType)) in modules)
            try
            {
                var widgetInstance = ApplicationStartup.ServiceProvider.GetRequiredService(viewContract) as Widget;
                if (widgetInstance != null)
                {
                    var viewInstance = widgetInstance as IView;
                    _moduleWidgets.Add((moduleName, widgetInstance));
                }
                else
                {
                    _logger.LogWarning($"La vue du module '{moduleName}' n’hérite pas de Widget.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Impossible d’instancier la vue du module '{moduleName}': {ex.Message}");
            }
    }

    public void Dispose()
    {
        View?.Dispose();
    }
}
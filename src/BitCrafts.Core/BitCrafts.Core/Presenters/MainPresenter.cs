using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications.Views;
using BitCrafts.Core.Views;
using Gtk;
using Serilog;

namespace BitCrafts.Core.Presenters;

public class MainPresenter : IMainPresenter
{
    private readonly ILogger _logger;
    private readonly IModuleManager _moduleManager;
    private readonly List<(string moduleName, Widget widget)> _moduleWidgets;
    private readonly IIoCResolver _resolver;

    public MainPresenter()
    {
        _moduleManager = ApplicationStartup.ModuleManager;
        _logger = ApplicationStartup.Logger;
        _resolver = ApplicationStartup.IoCContainer;
        _moduleWidgets = new List<(string moduleName, Widget widget)>();
    }

    public IMainWindowView View { get; private set; }

    public List<(string moduleName, Widget widget)> GetResolvedWidgets()
    {
        return _moduleWidgets;
    }


    public void Initialize()
    {
        LoadModules();
    }

    public void SetView(IMainWindowView view)
    {
        View = view;
        Initialize();
    }

    private void LoadModules()
    {
        var modules = _moduleManager.GetModuleViewTypes();
        foreach (var (moduleName, (viewContract, viewImplementation, presenterContract, presenterImplementation,
                     modelType)) in modules)
            try
            {
                var widgetInstance = _resolver.Resolve(viewContract) as Widget;
                if (widgetInstance != null)
                {
                    var viewInstance = widgetInstance as IView;
                    _moduleWidgets.Add((moduleName, widgetInstance));
                }
                else
                {
                    _logger.Warning($"La vue du module '{moduleName}' n’hérite pas de Widget.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Impossible d’instancier la vue du module '{moduleName}': {ex.Message}");
            }
    }
}
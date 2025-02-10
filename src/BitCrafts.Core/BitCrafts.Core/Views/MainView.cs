using BitCrafts.Core.Contracts;
using Serilog;

namespace BitCrafts.Core.Views;

public class MainView : Gtk.Window, IMainView
{
    private readonly IIoCResolver _resolver;
    private readonly IModuleManager _moduleManager;
    private readonly ILogger _logger;

    public MainView(IIoCResolver resolver, IModuleManager moduleManager, ILogger logger)
    {
        _resolver = resolver;
        _moduleManager = moduleManager;
        _logger = logger;
        Title = "BitCrafts";
        DefaultWidth = 800;
        DefaultHeight = 640;
        BuildView();
    }

    private void BuildView()
    {
        var notebook = new Gtk.Notebook();

        // Page principale (exemple)
        var mainLabel = new Gtk.Label { Label_ = "Vous avez chargé la page principale" };
        notebook.AppendPage(mainLabel, new Gtk.Label { Label_ = "Accueil" });

        // Récupération de l’ensemble des modules et de leurs types de vue
        var modules = _moduleManager.GetModuleViewTypes();
        foreach (var (moduleName, (viewContract, viewImplementation, presenterContract, presenterImplementation,
                     modelType)) in modules)
        {
            try
            {
                var viewInstance = _resolver.Resolve(viewContract) as Gtk.Widget;
                if (viewInstance != null)
                {
                    var tabLabel = new Gtk.Label { Label_ = moduleName };
                    notebook.AppendPage(viewInstance, tabLabel);
                }
                else
                {
                    _logger.Warning(
                        $"La vue du module '{moduleName}' n’hérite pas de Widget. Elle ne peut pas être ajoutée au Notebook.");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Impossible d’instancier la vue du module '{moduleName}': {ex.Message}");
            }
        }

        SetChild(notebook);
    }

    public void ShowView()
    {
        Show();
    }

    public void CloseView()
    {
        Hide();
    }
}
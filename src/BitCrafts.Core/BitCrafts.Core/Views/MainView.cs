using Avalonia.Controls;
using Avalonia.Interactivity;
using BitCrafts.Core.Presenters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// ReSharper disable InconsistentNaming
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace BitCrafts.Core.Views;

public partial class MainView : Window, IMainView
{
    private readonly IConfiguration _config;
    private readonly ILogger<MainView> _logger;

    public MainView(IConfiguration config, ILogger<MainView> logger)
    {
        _config = config;
        _logger = logger;
        InitializeComponent();
    }

    public void InitializeModules()
    {
        foreach (var (moduleName, widget) in Model.Widgets)
        {
            // Créer un nouvel onglet
            var tabItem = new TabItem
            {
                Header = moduleName,
                Content = widget
            };

            var items = new List<TabItem>();
            items.Add(tabItem);
            ModulesTabControl.ItemsSource = items;
        }
    }

    public IMainPresenterModel Model { get; set; }
    public event EventHandler OnViewLoaded;
    public event EventHandler OnViewClosing;

    public void ShowView()
    {
        Show();
    }

    public void HideView()
    {
        Hide();
    }

    public void Dispose()
    {
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        OnViewLoaded?.Invoke(this, EventArgs.Empty);
        base.OnLoaded(e);
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        OnViewClosing?.Invoke(this, EventArgs.Empty);
        base.OnUnloaded(e);
    }
}
using Avalonia.Markup.Xaml;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Models;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Views;

public partial class MainControlView : BaseView<IMainControlPresenterModel>, IMainControlView
{
    private readonly ILogger<MainControlView> _logger;

    public MainControlView(IMainControlPresenterModel model, ILogger<MainControlView> logger)
        : base(model, logger)
    {
        _logger = logger;
        AvaloniaXamlLoader.Load(this);
    }
}
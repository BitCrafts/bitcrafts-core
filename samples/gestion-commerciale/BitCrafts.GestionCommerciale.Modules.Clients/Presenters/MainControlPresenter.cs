using BitCrafts.Core.Contracts.Presenters;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Models;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Presenters;
using BitCrafts.GestionCommerciale.Modules.Clients.Contracts.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.GestionCommerciale.Modules.Clients.Presenters;

public sealed class MainControlPresenter : BasePresenter<IMainControlView, IMainControlPresenterModel>,
    IMainControlPresenter
{
    private readonly ILogger<MainControlPresenter> _logger;

    public MainControlPresenter(IMainControlView view, IMainControlPresenterModel model,
        ILogger<MainControlPresenter> logger) : base(view, model)
    {
        _logger = logger;
    }

    public override void OnViewLoaded()
    {
        _logger.LogInformation("MainControlPresenter loaded");
    }

    public override void OnViewUnloaded()
    {
        _logger.LogInformation("MainControlPresenter unloaded");
    }
}
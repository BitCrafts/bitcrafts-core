using System;
using System.Linq;
using System.Threading.Tasks;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Infrastructure.Application.Avalonia.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MainView = BitCrafts.Infrastructure.Application.Avalonia.Views.MainView;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public sealed class MainPresenter : BasePresenter<IMainView>, IMainPresenter
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUiManager _uiManager;
    private IWorkspaceManager _workspaceManager;

    public MainPresenter(IServiceProvider serviceProvider, IMainView view, IUiManager uiManager,
        IWorkspaceManager workspaceManager,
        ILogger<MainPresenter> logger)
        : base("Home Manager", view, logger)
    {
        _serviceProvider = serviceProvider;
        _uiManager = uiManager;
        _workspaceManager = workspaceManager;
    }

    private void ViewOnMenuItemClicked(object sender, string e)
    {
        var module = _serviceProvider.GetServices<IModule>().FirstOrDefault(x => x.Name.Equals(e));
        var presenterType = module.GetPresenterType();
        _workspaceManager.ShowPresenter(presenterType);
    }

    protected override void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        View.InitializeModulesMenu(_serviceProvider.GetServices<IModule>());

        if (View is MainView mainView)
        {
            var workspaceManager = (AvaloniaWorkspaceManager)_workspaceManager;
            workspaceManager.SetTabControl(mainView.MainTabControl);
        }
    }

    protected override async void OnViewClosed(object sender, EventArgs e)
    {
        base.OnViewClosed(sender, e);
        await _uiManager.ShutdownAsync();
    }

    protected override void OnInitialize()
    {
        View.MenuItemClicked += ViewOnMenuItemClicked;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            View.MenuItemClicked -= ViewOnMenuItemClicked;
        }

        base.Dispose(disposing);
    }
}
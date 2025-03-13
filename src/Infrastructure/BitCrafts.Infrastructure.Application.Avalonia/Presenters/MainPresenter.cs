using System;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Infrastructure.Application.Avalonia.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public sealed class MainPresenter : BasePresenter<IMainView>, IMainPresenter
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUiManager _uiManager;
    private readonly IWorkspaceManager _workspaceManager;

    public MainPresenter(IServiceProvider serviceProvider, IMainView view, IUiManager uiManager,
        IWorkspaceManager workspaceManager,
        ILogger<MainPresenter> logger)
        : base(view, logger)
    {
        _serviceProvider = serviceProvider;
        _uiManager = uiManager;
        _workspaceManager = workspaceManager;
        var windowTitle = _serviceProvider.GetService<IConfiguration>()["ApplicationSettings:Name"] ?? "No Name Application";
        View.SetTitle(windowTitle);
    }

    private void ViewOnMenuItemClicked(object sender, string e)
    {
        IModule module = null;
        foreach (var x in _serviceProvider.GetServices<IModule>())
            if (x.Name.Equals(e))
            {
                module = x;
                break;
            }

        if (module != null)
        {
            var presenterType = module.GetPresenterType();
            _workspaceManager.ShowPresenter(presenterType);
        }
    }

    protected override void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        View.InitializeModulesMenu(_serviceProvider.GetServices<IModule>());

        if (View is not MainView mainView) return;
        var workspaceManager = (AvaloniaWorkspaceManager)_workspaceManager;
        workspaceManager.SetTabControl(mainView.MainTabControl);
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
        if (disposing) View.MenuItemClicked -= ViewOnMenuItemClicked;

        base.Dispose(disposing);
    }
}
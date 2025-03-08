using System;
using System.Linq;
using System.Threading.Tasks;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Infrastructure.Application.Avalonia.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using MainView = BitCrafts.Infrastructure.Application.Avalonia.Views.MainView;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public sealed class MainPresenter : BasePresenter<IMainView>, IMainPresenter
{
    private IWorkspaceManager _workspaceManager;

    public MainPresenter(IServiceProvider serviceProvider)
        : base("Home Manager", serviceProvider)
    {
        _workspaceManager = ServiceProvider.GetRequiredService<IWorkspaceManager>();
    }

    private void ViewOnMenuItemClicked(object sender, string e)
    {
        var module = ServiceProvider.GetServices<IModule>().FirstOrDefault(x => x.Name.Equals(e));
        var presenterType = module.GetPresenterType();
        _workspaceManager.ShowPresenter(presenterType);
    }

    protected override void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        View.InitializeModulesMenu(ServiceProvider.GetServices<IModule>());

        if (View is MainView mainView)
        {
            var workspaceManager = (AvaloniaWorkspaceManager)_workspaceManager;
            workspaceManager.SetTabControl(mainView.MainTabControl);
        }
    }

    protected override async void OnViewClosed(object sender, EventArgs e)
    {
        base.OnViewClosed(sender, e);
        await ServiceProvider.GetRequiredService<IUiManager>().ShutdownAsync();
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
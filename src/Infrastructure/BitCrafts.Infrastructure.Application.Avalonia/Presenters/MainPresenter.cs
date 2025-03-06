using System;
using System.Linq;
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
    public MainPresenter(IServiceProvider serviceProvider)
        : base("Home Manager", serviceProvider)
    {
    }

    private void ViewOnMenuItemClicked(object sender, string e)
    {
        var module = ServiceProvider.GetServices<IModule>().FirstOrDefault(x => x.Name.Equals(e));
        var presenterType = module.GetPresenterType();
        var presenter = ServiceProvider.GetRequiredService(presenterType) as IPresenter;
        ServiceProvider.GetRequiredService<IWorkspaceManager>().ShowPresenter(presenter);
    }

    protected override void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        View.InitializeModulesMenu(ServiceProvider.GetServices<IModule>());


        if (View is MainView mainView)
        {
            var workspaceManager = (AvaloniaWorkspaceManager)ServiceProvider.GetRequiredService<IWorkspaceManager>();
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
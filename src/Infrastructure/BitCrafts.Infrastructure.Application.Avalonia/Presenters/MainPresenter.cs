using System;
using System.Linq;
using System.Threading.Tasks;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Infrastructure.Application.Avalonia.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public sealed class MainPresenter : BasePresenter<IMainView>, IMainPresenter
{
    private AvaloniaWorkspaceManager _workspaceManager;

    public MainPresenter(IServiceProvider serviceProvider) : base("Main", serviceProvider)
    {
    }


    private async void ViewOnMenuItemClicked(object sender, string e)
    {
        var module = ServiceProvider.GetServices<IModule>().FirstOrDefault(x => x.Name.Equals(e));
        var presenterType = module.GetPresenterType();
        var presenter = ServiceProvider.GetRequiredService(presenterType);
        dynamic dynamicPresenter = presenter;
        await _workspaceManager.ShowPresenterAsync(dynamicPresenter);
    }

    protected override void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        _workspaceManager = ServiceProvider.GetService<IWorkspaceManager>() as AvaloniaWorkspaceManager;
        if (_workspaceManager != null)
        {
            _workspaceManager.SetControl((View as MainView)?.GetTabControl());
        }

        View.MenuItemClicked += ViewOnMenuItemClicked;
        var dicModules = ServiceProvider.GetServices<IModule>();
        View.InitializeMenu(dicModules);
    }

    protected override async void OnViewClosed(object sender, EventArgs e)
    {
        base.OnViewClosed(sender, e);
        Logger.LogInformation($"{this.GetType().Name} closed");
        await ServiceProvider.GetRequiredService<IUiManager>().ShutdownAsync();
    }
}
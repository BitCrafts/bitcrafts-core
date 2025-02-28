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
    private IWorkspaceManager _workspaceManager;

    public MainPresenter(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        var workspaceManager = ServiceProvider.GetService<IWorkspaceManager>() as AvaloniaWorkspaceManager;
        _workspaceManager = workspaceManager;
        var mainView = View as MainView;
        workspaceManager.SetControl(mainView.GetTabControl());
        View.MenuItemClicked += ViewOnMenuItemClicked;
        var dicModules = ServiceProvider.GetServices<IModule>();
        View.InitializeMenu(dicModules);
    }

    private void ViewOnMenuItemClicked(object sender, string e)
    {
        var module = ServiceProvider.GetServices<IModule>().FirstOrDefault(x => x.Name.Equals(e));
        var presenterType = module.GetPresenterType();
        var presenter = ServiceProvider.GetRequiredService(presenterType);
        Type presenterInterfaceType = presenter.GetType().GetInterfaces()
            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPresenter<>));
        dynamic dynamicPresenter = presenter;
        _workspaceManager.ShowPresenter(module.Name, dynamicPresenter);
    }

    protected override async void OnWindowClosed(object sender, EventArgs e)
    {
        Logger.LogInformation($"{this.GetType().Name} closed");
        await ServiceProvider.GetRequiredService<IUiManager>().ShutdownAsync();
    }

    protected override void OnWindowLoaded(object sender, EventArgs e)
    {
        Logger.LogInformation($"{this.GetType().Name} loaded");
    }
}
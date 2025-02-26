using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public sealed class StartupPresenter : BasePresenter<IStartupView>, IStartupPresenter
{
    private IModuleManager ModuleManager { get; }

    public StartupPresenter(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        ModuleManager = serviceProvider.GetService<IModuleManager>();
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await LoadModulesPresenters();
    }

    private async Task LoadModulesPresenters()
    {
        View.SetLoadingText("Loading Presenters");
        var presenters = new Dictionary<string, Type>();
        foreach (var module in ModuleManager.Modules)
        {
            presenters.TryAdd(module.Key, module.Value.GetPresenterType());
        }

        await Task.Delay(3000);
        var presenter = ServiceProvider.GetService<IMainPresenter>();
        await presenter.InitializeAsync();
        presenter.Show();
        WindowingManager.CloseWindow(presenter.View);
    }
}
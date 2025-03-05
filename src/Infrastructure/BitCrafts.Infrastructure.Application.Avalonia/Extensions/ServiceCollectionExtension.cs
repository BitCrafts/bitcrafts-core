using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Presenters;
using BitCrafts.Infrastructure.Application.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBitCraftsAvaloniaApplication(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IApplication, AvaloniaApplication>("Avalonia");
        services.AddSingleton<IWindowManager, AvaloniaWindowManager>();
        services.AddSingleton<IWorkspaceManager, AvaloniaWorkspaceManager>();
        services.AddSingleton<IUiManager, AvaloniaUiManager>();
        services.AddSingleton<IMainPresenter, MainPresenter>();
        services.AddSingleton<IMainView, MainView>();

        return services;
    }
}
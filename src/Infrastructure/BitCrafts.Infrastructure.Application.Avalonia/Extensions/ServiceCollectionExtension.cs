using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Application.Avalonia.Presenters;
using BitCrafts.Infrastructure.Application.Avalonia.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBitCraftsAvaloniaApplication(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IApplication, AvaloniaApplication>("Avalonia");
        services.AddSingleton<IUiManager, AvaloniaUiManager>();
        services.AddSingleton<IApplicationStartup, AvaloniaApplicationStartup>();
        services.AddSingleton<IStartupView, StartupView>();
        services.AddSingleton<IMainView, MainView>();
        services.AddSingleton<IPresenter<IStartupView>, StartupPresenter>();
        return services;
    }
}
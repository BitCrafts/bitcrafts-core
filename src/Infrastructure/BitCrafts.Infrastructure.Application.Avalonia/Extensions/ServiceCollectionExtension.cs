using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Presenters;
using BitCrafts.Infrastructure.Application.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BitCrafts.Infrastructure.Application.Avalonia.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBitCraftsAvaloniaApplication(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IApplication, AvaloniaApplication>("Avalonia");
        services.TryAddSingleton<IWindowManager, AvaloniaWindowManager>();
        services.TryAddSingleton<IWorkspaceManager, AvaloniaWorkspaceManager>();
        services.TryAddSingleton<IUiManager, AvaloniaUiManager>();
        services.TryAddSingleton<IExceptionManager, AvaloniaExceptionManager>();
        services.TryAddScoped<IMainPresenter, MainPresenter>();                
        services.TryAddTransient<IMainView, MainView>();
        services.TryAddTransient<IErrorPresenter, ErrorPresenter>(); 
        services.TryAddTransient<IErrorView, ErrorView>();         

        return services;
    }
}
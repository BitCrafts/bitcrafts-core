using BitCrafts.Infrastructure.Abstraction.Application;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBitCraftsAvaloniaApplication(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IApplication, AvaloniaApplication>("Avalonia"); 
        services.AddSingleton<IUiManager, AvaloniaUiManager>(); 
        services.AddSingleton<ISplashScreen, AvaloniaSplashScreen>(); 
        return services;
    }
}
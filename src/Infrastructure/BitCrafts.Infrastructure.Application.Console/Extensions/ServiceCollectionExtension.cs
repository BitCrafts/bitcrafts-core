using BitCrafts.Infrastructure.Abstraction.Application;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Console.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBitCraftsConsoleApplication(this IServiceCollection services)
    {
        services.AddKeyedSingleton<IApplication, ConsoleApplication>("Console");
        services.AddSingleton<IApplicationStartup, ConsoleApplicationStartup>();
        return services;
    }
}
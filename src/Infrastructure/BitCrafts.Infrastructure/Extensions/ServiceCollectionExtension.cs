using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Services;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Infrastructure.Application;
using BitCrafts.Infrastructure.Events;
using BitCrafts.Infrastructure.Modules;
using BitCrafts.Infrastructure.Repositories;
using BitCrafts.Infrastructure.Services;
using BitCrafts.Infrastructure.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;

namespace BitCrafts.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBitCraftsInfrastructure(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        Log.Logger = logger;

        services.AddSingleton<IConfiguration>(configuration);
        services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true)
        );

        services.TryAddSingleton<IBackgroundThreadDispatcher, BackgroundThreadDispatcher>();
        services.TryAddSingleton<IParallelism, Parallelism>();
        services.TryAddSingleton<IEventAggregator, EventAggregator>();
        services.TryAddSingleton<IApplicationFactory, ApplicationFactory>();
        services.TryAddSingleton<IHashingService, HashingService>();
        services.TryAddScoped<IRepositoryUnitOfWork, RepositoryUnitOfWork>();
        CreateDirectoryDirectory("Modules");
        CreateDirectoryDirectory("Settings");
        CreateDirectoryDirectory("Databases");
        CreateDirectoryDirectory("Files");
        CreateDirectoryDirectory("Temporary");
        var moduleRegistrer = new ModuleRegistrer(Log.Logger);
        moduleRegistrer.RegisterModules(services);
        return services;
    }

    private static void CreateDirectoryDirectory(string directory)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), directory);
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(Path.GetFullPath(directoryPath));
    }
}
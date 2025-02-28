using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Databases;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Services;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Infrastructure.Application;
using BitCrafts.Infrastructure.Databases;
using BitCrafts.Infrastructure.Events;
using BitCrafts.Infrastructure.Modules;
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

        services.TryAddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.TryAddSingleton<IBackgroundThreadDispatcher, BackgroundThreadDispatcher>();
        services.TryAddSingleton<IParallelism, Parallelism>();
        services.TryAddSingleton<IEventAggregator, EventAggregator>();
        services.TryAddSingleton<IApplicationFactory, ApplicationFactory>();
        services.TryAddSingleton<IDapperWrapper, DapperWrapper>();
        services.TryAddSingleton<IDatabaseManager, DatabaseManager>();
        services.TryAddSingleton<ISqlDialectFactory, SqlDialectFactory>();
        services.TryAddSingleton<IHashingService, HashingService>();
        CreateDirectoryDirectory("Modules");
        CreateDirectoryDirectory("Settings");
        CreateDirectoryDirectory("Databases");
        CreateDirectoryDirectory("Files");
        CreateDirectoryDirectory("Temporary");
        ModuleRegistrer moduleRegistrer = new ModuleRegistrer(Log.Logger);
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
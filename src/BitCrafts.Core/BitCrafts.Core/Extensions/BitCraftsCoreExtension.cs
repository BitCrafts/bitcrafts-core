using BitCrafts.Core.Contracts.Database;
using BitCrafts.Core.Database;
using BitCrafts.Core.Presenters;
using BitCrafts.Core.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BitCrafts.Core.Extensions;

public static class BitCraftsCoreExtension
{
    public static void AddBitCraftsCore(this IServiceCollection services)
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

        services.AddSingleton<IMainView, MainView>();
        services.AddSingleton<IMainPresenter, MainPresenter>();
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
    }
}
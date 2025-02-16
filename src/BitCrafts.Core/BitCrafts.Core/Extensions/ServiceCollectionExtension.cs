using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.Contracts.Database;
using BitCrafts.Core.Contracts.Services;
using BitCrafts.Core.Contracts.Threading;
using BitCrafts.Core.Database;
using BitCrafts.Core.Services;
using BitCrafts.Core.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddBitCraftsCore(this IServiceCollection services)
    {
        services.AddSingleton<IApplication, BitCraftsApplication>();
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddSingleton<IEventAggregatorService, EventAggregatorService>();
        services.AddSingleton<IDatabaseTaskScheduler, DatabaseTaskScheduler>();
        services.AddSingleton<INetworkTaskScheduler, NetworkTaskScheduler>();
        services.AddSingleton<IBackgroundTaskSceduler, BackgroundTaskScheduler>();
    }
}
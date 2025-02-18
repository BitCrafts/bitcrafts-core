using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public abstract class BaseApplication : IApplication
{
    protected readonly ILogger<BaseApplication> Logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IApplicationStartup _applicationStartup;

    protected BaseApplication(ILogger<BaseApplication> logger, IServiceProvider serviceProvider)
    {
        Logger = logger;
        _serviceProvider = serviceProvider;
        _applicationStartup = _serviceProvider.GetRequiredService<IApplicationStartup>();
    }

    public virtual async Task StartAsync()
    {
        //initialize event aggregator
        _serviceProvider.GetRequiredService<IEventAggregator>();
        //start background thread
        _serviceProvider.GetRequiredService<IBackgroundThreadScheduler>().Start();

        await _applicationStartup.StartAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _serviceProvider.GetRequiredService<IBackgroundThreadScheduler>().Stop();
            _applicationStartup.Dispose();
        }

        Logger.LogInformation("Application disposed");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
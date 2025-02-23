using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public abstract class BaseApplication : IApplication
{
    protected readonly ILogger<BaseApplication> Logger;
    private readonly IServiceProvider _serviceProvider;

    protected BaseApplication(ILogger<BaseApplication> logger, IServiceProvider serviceProvider)
    {
        Logger = logger;
        _serviceProvider = serviceProvider;
    }
  
    public async Task StartAsync()
    {
        await OnStartupAsync();
    }

    protected abstract Task OnStartupAsync();

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _serviceProvider.GetRequiredService<IBackgroundThreadScheduler>().Stop();
        }

        Logger.LogInformation("Application disposed");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
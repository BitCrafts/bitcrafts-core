using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public abstract class BaseApplication : IApplication
{
    private readonly IServiceProvider _serviceProvider;
    protected readonly ILogger<BaseApplication> Logger;

    protected BaseApplication(ILogger<BaseApplication> logger, IServiceProvider serviceProvider)
    {
        Logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync()
    {
        await OnStartupAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected abstract Task OnStartupAsync();

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            //_serviceProvider.GetRequiredService<IBackgroundThreadScheduler>().Stop();
        }

        Logger.LogInformation("Application disposed");
    }
}
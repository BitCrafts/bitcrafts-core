using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public abstract class BaseApplication : IApplication
{
    protected readonly ILogger<BaseApplication> Logger;

    protected BaseApplication(ILogger<BaseApplication> logger, IBackgroundThreadDispatcher backgroundThreadDispatcher)
    {
        Logger = logger;
    }

    public IServiceProvider ServiceProvider { get; set; }

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
        Logger.LogInformation("Application disposed");
    }
}
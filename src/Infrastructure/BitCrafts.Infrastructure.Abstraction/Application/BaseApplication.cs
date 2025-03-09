using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public abstract class BaseApplication : IApplication
{
    private readonly IBackgroundThreadDispatcher _backgroundThreadDispatcher;
    protected readonly ILogger<BaseApplication> Logger;

    protected BaseApplication(ILogger<BaseApplication> logger, IBackgroundThreadDispatcher backgroundThreadDispatcher)
    {
        Logger = logger;
        _backgroundThreadDispatcher = backgroundThreadDispatcher;
    }

    public IServiceProvider ServiceProvider { get; set; }

    public async Task StartAsync()
    {
        _backgroundThreadDispatcher.Start();
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
        if (disposing) _backgroundThreadDispatcher.Stop();

        Logger.LogInformation("Application disposed");
    }
}
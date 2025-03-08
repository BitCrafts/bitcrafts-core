using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public abstract class BaseApplication : IApplication
{
    protected readonly ILogger<BaseApplication> Logger;
    private readonly IBackgroundThreadDispatcher _backgroundThreadDispatcher;
    public IServiceProvider ServiceProvider { get; set; }

    protected BaseApplication(ILogger<BaseApplication> logger, IBackgroundThreadDispatcher backgroundThreadDispatcher)
    {
        Logger = logger;
        _backgroundThreadDispatcher = backgroundThreadDispatcher;
    }

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
        if (disposing)
        {
            _backgroundThreadDispatcher.Stop();
        }

        Logger.LogInformation("Application disposed");
    }
}
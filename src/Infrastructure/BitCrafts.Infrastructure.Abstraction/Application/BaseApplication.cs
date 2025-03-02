using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.DependencyInjection;
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
        _serviceProvider.GetRequiredService<IBackgroundThreadDispatcher>().Start();
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
            _serviceProvider.GetRequiredService<IWorkspaceManager>().Dispose();
        }

        Logger.LogInformation("Application disposed");
    }
}
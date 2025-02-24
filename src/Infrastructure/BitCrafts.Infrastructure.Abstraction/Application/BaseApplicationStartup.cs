using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public abstract class BaseApplicationStartup : IApplicationStartup
{
    protected readonly ILogger<BaseApplicationStartup> Logger;
    protected readonly IServiceProvider ServiceProvider;

    protected BaseApplicationStartup(ILogger<BaseApplicationStartup> logger,
        IServiceProvider serviceProvider)
    {
        Logger = logger;
        ServiceProvider = serviceProvider;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public abstract Task StartAsync();

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // TODO release managed resources here
        }
    }
}
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public abstract class BaseApplicationStartup : IApplicationStartup
{
    protected readonly ILogger<BaseApplicationStartup> _logger;
    protected readonly IServiceProvider _serviceProvider;

    protected BaseApplicationStartup(ILogger<BaseApplicationStartup> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // TODO release managed resources here
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual Task StartAsync()
    {
       
        return Task.CompletedTask;
    }
}
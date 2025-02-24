using BitCrafts.Infrastructure.Abstraction.Application;
using Microsoft.Extensions.Logging;

namespace BitCrafts.GestionCommerciale.Application;

public sealed class ApplicationStartup : IApplicationStartup
{
    private readonly ILogger<ApplicationStartup> _logger;

    public ApplicationStartup(ILogger<ApplicationStartup> logger)
    {
        _logger = logger;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }

    public Task StartAsync()
    {
        _logger.LogInformation("Executed in background");
        return Task.CompletedTask;
    }
}
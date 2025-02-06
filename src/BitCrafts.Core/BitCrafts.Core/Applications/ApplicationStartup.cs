using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using Serilog;

namespace BitCrafts.Core.Applications;

public class ApplicationStartup : IApplicationStartup
{
    private readonly ILogger _logger;

    public ApplicationStartup(ILogger logger)
    {
        _logger = logger;
    }

    public void Initialize()
    {
        _logger.Information("Initializing Application...");

        _logger.Information("Application Initialization Complete.");
    }

    public void Start()
    {
        _logger.Information("Starting Application...");
        _logger.Information("Application Starting complete.");
    }
}
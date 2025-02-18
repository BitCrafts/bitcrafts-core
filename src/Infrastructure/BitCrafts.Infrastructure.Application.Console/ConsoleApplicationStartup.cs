using BitCrafts.Infrastructure.Abstraction.Application;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Console;

public sealed class ConsoleApplicationStartup : BaseApplicationStartup
{
    public ConsoleApplicationStartup(ILogger<BaseApplicationStartup> logger, IServiceProvider serviceProvider) : base(
        logger, serviceProvider)
    {
    }

    public override Task StartAsync()
    {
        _logger.LogInformation("Starting Console Application");
        return base.StartAsync();
    }
}
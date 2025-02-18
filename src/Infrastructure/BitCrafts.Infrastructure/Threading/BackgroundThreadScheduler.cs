using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Threading;

public sealed class BackgroundThreadScheduler : BaseThreadScheduler, IBackgroundThreadScheduler
{
    public BackgroundThreadScheduler(ILogger<BackgroundThreadScheduler> logger)
        : base(logger, "Background Thread")
    {
    }

    public override void Start()
    {
        _logger.LogInformation("Background Thread is starting.");
        base.Start();
        _logger.LogInformation("Background Thread is started.");
    }

    public override void Stop()
    {
        _logger.LogInformation("Background Thread is stopping.");
        base.Stop();
        _logger.LogInformation("Background Thread is stopped.");
    }
}
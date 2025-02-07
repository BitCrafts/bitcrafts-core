using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.Core.Applications;

public abstract class BaseApplication : IApplication
{
    public BaseApplication()
    {
    }

    public virtual async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public abstract Task RunAsync();

    public virtual async Task ShutdownAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
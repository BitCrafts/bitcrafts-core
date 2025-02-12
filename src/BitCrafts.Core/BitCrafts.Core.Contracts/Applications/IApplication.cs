namespace BitCrafts.Core.Contracts.Applications;

public interface IApplication : IDisposable
{
    Task InitializeAsync(CancellationToken cancellationToken);

    Task RunAsync();

    Task ShutdownAsync(CancellationToken cancellationToken);
}
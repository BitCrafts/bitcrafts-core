namespace BitCrafts.Core.Contracts.Applications;

public interface INativeApplication : IDisposable
{
    void Run();
    void Shutdown();
}
namespace BitCrafts.Core.Contracts.Applications;

public interface IApplication : IDisposable
{
    void Run();
}
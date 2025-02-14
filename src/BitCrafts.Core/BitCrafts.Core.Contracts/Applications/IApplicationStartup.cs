namespace BitCrafts.Core.Contracts.Applications;

public interface IApplicationStartup : IDisposable
{
    void Start();
}
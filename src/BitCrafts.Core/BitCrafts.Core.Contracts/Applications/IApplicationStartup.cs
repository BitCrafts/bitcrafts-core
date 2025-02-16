namespace BitCrafts.Core.Contracts.Applications;

public interface IApplicationStartup : IDisposable
{
    Task StartAsync();
}
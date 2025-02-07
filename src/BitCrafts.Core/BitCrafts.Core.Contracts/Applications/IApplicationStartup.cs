namespace BitCrafts.Core.Contracts.Applications;

public interface IApplicationStartup
{
    Task InitializeAsync();
    Task StartAsync();
}
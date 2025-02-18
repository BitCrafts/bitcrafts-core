namespace BitCrafts.Infrastructure.Abstraction.Application;

public interface IApplicationStartup : IDisposable
{
    Task StartAsync();
}
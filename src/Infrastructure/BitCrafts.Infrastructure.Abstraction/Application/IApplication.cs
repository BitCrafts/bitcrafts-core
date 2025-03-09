namespace BitCrafts.Infrastructure.Abstraction.Application;

public interface IApplication : IDisposable
{
    IServiceProvider ServiceProvider { get; set; }
    Task StartAsync();
}
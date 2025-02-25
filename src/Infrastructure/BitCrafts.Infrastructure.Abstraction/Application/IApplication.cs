namespace BitCrafts.Infrastructure.Abstraction.Application;

public interface IApplication : IDisposable
{ 
    Task StartAsync();
}
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public interface IApplication : IDisposable
{ 
    Task StartAsync();
    IServiceProvider ServiceProvider { get; set; }
}
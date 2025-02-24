using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Abstraction.Modules;

public interface IModuleRegistrer : IDisposable
{
    void RegisterModules(IServiceCollection services);
}
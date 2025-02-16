using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core.Contracts;

public interface IModuleManager : IDisposable
{
    void LoadModules(IServiceCollection services); 
}
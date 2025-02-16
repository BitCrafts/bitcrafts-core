using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core.Contracts.Modules;

public interface IModule
{
    string Name { get; }
    void RegisterServices(IServiceCollection services);
}
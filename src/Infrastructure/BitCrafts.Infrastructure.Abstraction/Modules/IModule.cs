using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Abstraction.Modules;

public interface IModule
{
    string Name { get; }
    void RegisterServices(IServiceCollection services);
    Type GetViewType();
    Type GetPresenterType();
    Type GetViewImplementationType();
    Type GetPresenterImplementationType();
}
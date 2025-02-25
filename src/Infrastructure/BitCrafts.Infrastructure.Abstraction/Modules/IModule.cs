using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Abstraction.Modules;

public interface IModule
{
    string Name { get; }
    void RegisterServices(IServiceCollection services);
    Type GetViewType();
    Type GetPresenterType();
}
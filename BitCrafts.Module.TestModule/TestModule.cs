using BitCrafts.Infrastructure.Abstraction.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Module.TestModule;
public interface ITestModule
{
}

public class TestModule : IModule, ITestModule
{
    public string Name { get; } = "TestModule";

    public void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<ITestModule>(this);
        services.AddSingleton<TestModule>(this);
    }

    public Type GetViewType()
    {
        throw new NotImplementedException();
    }

    public Type GetPresenterType()
    {
        throw new NotImplementedException();
    }

    public Type GetViewImplementationType()
    {
        throw new NotImplementedException();
    }

    public Type GetPresenterImplementationType()
    {
        throw new NotImplementedException();
    }
}
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core.Contracts;

public interface IIoCRegister
{
    void Register<TService, TImplementation>(ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService;

    void Register(Type typeInterface, Type typeImplementation, ServiceLifetime lifetime);

    void RegisterInstance<TService>(TService service) where TService : class;

    void Build();
}
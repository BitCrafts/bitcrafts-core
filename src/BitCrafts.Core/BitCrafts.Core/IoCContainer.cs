using BitCrafts.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core;

public class IoCContainer : IIoCRegister, IIoCResolver
{
    private readonly ServiceCollection _services;
    private IServiceProvider _serviceProvider;

    public IoCContainer()
    {
        _services = new ServiceCollection();
    }

    public void Register<TService, TImplementation>(ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService
    {
        // Ajout du service avec sa durée de vie
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                _services.AddSingleton<TService, TImplementation>();
                break;
            case ServiceLifetime.Scoped:
                _services.AddScoped<TService, TImplementation>();
                break;
            case ServiceLifetime.Transient:
                _services.AddTransient<TService, TImplementation>();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
        }
    }

    public void Register(Type typeInterface, Type typeImplementation, ServiceLifetime lifetime)
    {
        // Ajout du service avec sa durée de vie
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                _services.AddSingleton(typeInterface, typeImplementation);
                break;
            case ServiceLifetime.Scoped:
                _services.AddScoped(typeInterface, typeImplementation);
                break;
            case ServiceLifetime.Transient:
                _services.AddTransient(typeInterface, typeImplementation);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
        }
    }

    public void RegisterInstance<TService>(TService service) where TService : class
    {
        _services.AddSingleton(service);
    }

    public void Build()
    {
        _serviceProvider = _services.BuildServiceProvider();
    }

    public TService Resolve<TService>()
    {
        if (_serviceProvider == null)
            throw new InvalidOperationException(
                "Le conteneur IoC n'a pas été construit ! Appelez Build() avant de résoudre les services.");

        var service = _serviceProvider.GetService<TService>();
        if (service == null)
            throw new Exception($"Le service {typeof(TService).Name} n'existe pas dans le conteneur.");

        return service;
    }

    public object Resolve(Type type)
    {
        var instance = _serviceProvider.GetService(type);
        if (instance == null)
            throw new Exception($"Le service {type.Name} n'existe pas dans le conteneur.");

        return instance;
    }

    public TService Resolve<TService>(Type type)
    {
        var instance = _serviceProvider.GetService(type);
        if (instance == null)
            throw new Exception($"Le service {type.Name} n'existe pas dans le conteneur.");

        return (TService)instance;
    }
}
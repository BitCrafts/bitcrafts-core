using BitCrafts.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core;

public class IoCContainer : IIoCContainer
{
    private readonly ServiceCollection _services; // Collection interne des services
    private IServiceProvider _serviceProvider; // Fournisseur pour résoudre les services
    private bool _isReBuilt; // Indique si le conteneur est construit

    public IoCContainer()
    {
        _services = new ServiceCollection();
    }

    /// <summary>
    /// Ajoute un service au conteneur.
    /// </summary>
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

    /// <summary>
    /// Enregistre une instance spécifique dans le conteneur.
    /// </summary>
    public void RegisterInstance<TService>(TService service) where TService : class
    {
        _services.AddSingleton(service);
    }

    /// <summary>
    /// Résout un service enregistré.
    /// </summary>
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

    /// <summary>
    /// Configure et construit le conteneur complet.
    /// </summary>
    public void Build()
    {
        _serviceProvider = _services.BuildServiceProvider();
    }

    /// <summary>
    /// Reconstruit le conteneur après le chargement des services des modules.
    /// </summary>
    public void Rebuild()
    {
        //  if (_isReBuilt)
        //     return;
        // Reconstruit le ServiceProvider après des modifications dans les services
        _serviceProvider = _services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateScopes = true, // Valider les scopes pour détecter les erreurs
            ValidateOnBuild = true // Valider lors de la construction
        });
        _isReBuilt = true;
    }
}
using System;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core.Contracts;

public interface IIoCContainer
{
    /// <summary>
    /// Ajoute un service ou module au conteneur.
    /// </summary>
    /// <typeparam name="TService">Type de service.</typeparam>
    /// <typeparam name="TImplementation">Type de l'implémentation.</typeparam>
    /// <param name="lifetime">Durée de vie du service (Scoped, Singleton, Transient).</param>
    void Register<TService, TImplementation>(ServiceLifetime lifetime)
        where TService : class
        where TImplementation : class, TService;

    /// <summary>
    /// Ajoute une instance spécifique au conteneur.
    /// </summary>
    /// <param name="service">L'instance du service à ajouter.</param>
    void RegisterInstance<TService>(TService service) where TService : class;

    /// <summary>
    /// Résout un service par son type.
    /// </summary>
    /// <typeparam name="TService">Le type de service à résoudre.</typeparam>
    /// <returns>L'instance correspondante du service.</returns>
    TService Resolve<TService>();

    /// <summary>
    /// Configure et construit le conteneur complet.
    /// </summary>
    void Build();

    /// <summary>
    /// Reconstruit le conteneur après le chargement des services des modules.
    /// </summary>
    void Rebuild();
}
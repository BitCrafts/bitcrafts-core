using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BitCrafts.Core.Applications;

public abstract class BaseApplication : IApplication
{
    protected readonly IIoCContainer IoCContainer;
    protected readonly IConfiguration Configuration;
    protected readonly ILogger Logger;

    protected BaseApplication(IIoCContainer container)
    {
        IoCContainer = container ?? throw new ArgumentNullException(nameof(container));

        // Étape 1 : Charger les configurations depuis appsettings.json
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables() // Support des variables d'environnement
            .Build();

        // Étape 2 : Configurer Serilog selon les données de appsettings.json
        Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();

        // Étape 3 : Injecter le logger dans IoCContainer pour qu'il soit accessible globalement
        IoCContainer.RegisterInstance<ILogger>(Logger);
        IoCContainer.RegisterInstance<IConfiguration>(Configuration);
        IoCContainer.Rebuild();
    }

    /// <summary>
    /// Méthode d'entrée principale pour l'application.
    /// </summary>
    public void Run(string[] args)
    {
        try
        {
            Logger.Information("Starting Application: {ApplicationName}", GetApplicationName());

            // Étape 1 : Initialiser l'Application
            InitializeApplication();

            // Étape 2 : Exécuter l'Application
            ExecuteApplication();

            Logger.Information("Application {ApplicationName} executed successfully.", GetApplicationName());
        }
        catch (Exception ex)
        {
            Logger.Error(ex, "An error occurred in Application: {ApplicationName}", GetApplicationName());
            OnError(ex);
        }
        finally
        {
            // Étape 3 : Libérer les Ressources
            ShutdownApplication();
            Logger.Information("Application {ApplicationName} is shutting down.", GetApplicationName());
        }
    }

    /// <summary>
    /// Nom de l'application (peut être redéfini par des classes dérivées).
    /// </summary>
    protected virtual string GetApplicationName()
    {
        return "Generic Application";
    }

    /// <summary>
    /// Méthode pour initialiser les services ou configurations (à redéfinir si nécessaire).
    /// </summary>
    protected virtual void InitializeApplication()
    {
        Logger.Information("Initializing Application...");
    }

    /// <summary>
    /// Méthode principale pour exécuter la logique spécifique (obligatoire à surcharger).
    /// </summary>
    protected abstract void ExecuteApplication();

    /// <summary>
    /// Méthode à appeler lorsqu'une erreur est non gérée (peut être redéfinie).
    /// </summary>
    protected virtual void OnError(Exception exception)
    {
        Console.WriteLine("An error has occurred: " + exception.Message);
    }

    /// <summary>
    /// Méthode appelée lors de l'arrêt de l'application (à redéfinir si nécessaire).
    /// </summary>
    protected virtual void ShutdownApplication()
    {
        Logger.Information("Releasing resources and shutting down...");
    }
}
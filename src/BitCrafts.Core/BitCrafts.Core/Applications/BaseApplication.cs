using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.Core.Applications;

public abstract class BaseApplication : IApplication
{
    /*
        protected static IIoCContainer IoCContainer { get; private set; }
        protected readonly IConfiguration Configuration;
        protected readonly ILogger Logger;

        protected BaseApplication()
        {
            IoCContainer = new IoCContainer();

            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.WithProcessId()
                .Enrich.WithProcessName()
                .Enrich.WithThreadId()
                .Enrich.WithEnvironmentUserName() // Ajoute le nom de l'utilisateur de l'environnement
                .Enrich.FromLogContext() // Permet d'enrichir le contexte de journalisation
                .CreateLogger();
            Log.Logger = Logger;

            IoCContainer.RegisterInstance<ILogger>(Logger);
            IoCContainer.RegisterInstance<IConfiguration>(Configuration);
            IoCContainer.Register<IApplicationStartup, ApplicationStartup>(ServiceLifetime.Singleton);
            IoCContainer.Build();
        }

        public void Run(string[] args)
        {
            try
            {
                IoCContainer.Resolve<IApplicationStartup>().Initialize();

                IoCContainer.Resolve<IApplicationStartup>().Start();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "An error occurred in Application");
            }
            finally
            {
                Log.Logger.Information("Releasing resources and shutting down...");
                ShutdownApplication();
                Log.Logger.Information("Application is shutting down.");
            }
        }

        protected abstract void InitializeApplication(string[] args);

        protected abstract void ExecuteApplication();

        protected abstract void ShutdownApplication();*/
    public BaseApplication()
    {
    }

    public virtual async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public abstract Task RunAsync();

    public virtual async Task ShutdownAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
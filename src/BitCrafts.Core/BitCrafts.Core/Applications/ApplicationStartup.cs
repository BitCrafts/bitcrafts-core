using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BitCrafts.Core.Applications;

public class ApplicationStartup : IApplicationStartup
{
    public static IIoCContainer IoCContainer { get; private set; }
    private string[] _args;

    public ApplicationStartup(string[] args)
    {
        _args = args ?? Array.Empty<string>();
        IoCContainer = new IoCContainer();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .Enrich.WithThreadId()
            .Enrich.WithEnvironmentUserName()
            .Enrich.FromLogContext()
            .CreateLogger();
        Log.Logger = logger;

        IoCContainer.RegisterInstance<ILogger>(logger);
        IoCContainer.RegisterInstance<IIoCContainer>(IoCContainer);
        IoCContainer.RegisterInstance<IConfiguration>(configuration);
        IoCContainer.Register<IModuleManager, ModuleManager>(ServiceLifetime.Singleton);
        IoCContainer.Build();
    }

    public void Initialize()
    {
        Log.Logger.Information("Initializing Application...");
        IoCContainer.Resolve<IModuleManager>().LoadModules();
        var args = Environment.GetCommandLineArgs();
        IoCContainer.Rebuild();
        if (args.Any(arg => arg.ToLowerInvariant().Contains("gui-gtk")))
        {
            // Instanciation de l'application GTK via l'IoC container
            var gtkApp = IoCContainer.Resolve<IGtkApplication>();
            gtkApp.InitializeAsync(CancellationToken.None).Wait();
            gtkApp.RunAsync().Wait();
            // Vous pouvez stocker gtkApp ou effectuer des traitements spécifiques ici.
        }
        else if (args.Any(arg => arg.ToLowerInvariant().Contains("console")))
        {
            // Instanciation de l'application Console via l'IoC container
            var consoleApp = IoCContainer.Resolve<IConsoleApplication>();
            consoleApp.InitializeAsync(CancellationToken.None).Wait();
            consoleApp.RunAsync().Wait();
        }

        Log.Logger.Information("Application Initialization Complete.");
    }

    public void Start()
    {
        Log.Logger.Information("Starting Application...");
        Log.Logger.Information("Application Starting complete.");
    }
}
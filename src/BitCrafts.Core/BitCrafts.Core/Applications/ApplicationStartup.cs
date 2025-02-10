using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BitCrafts.Core.Applications;

public class ApplicationStartup : IApplicationStartup
{
    private IApplication _application;
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

    public async Task InitializeAsync()
    {
        Log.Logger.Information("Initializing Application...");
        IoCContainer.Resolve<IModuleManager>().LoadModules();
        var args = Environment.GetCommandLineArgs();
        IoCContainer.Build();
        if (args.Any(arg => arg.ToLowerInvariant().Equals("console")))
        {
            _application = IoCContainer.Resolve<IConsoleApplication>();
        }
        else
        {
            _application = IoCContainer.Resolve<IGtkApplication>();
        }

        await _application.InitializeAsync(CancellationToken.None);
        Log.Logger.Information("Application Initialization Complete.");
    }

    public async Task StartAsync()
    {
        
        Log.Logger.Information("Starting Application...");
        await _application.RunAsync();
        Log.Logger.Information("Application Starting complete.");
    }
}
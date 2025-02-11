using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BitCrafts.Core.Applications;

public class ApplicationStartup : IApplicationStartup
{
    private IApplication _application;
    public static IoCContainer IoCContainer { get; private set; }
    public static IModuleManager ModuleManager { get; private set; }
    public static IConfiguration Configuration { get; private set; }
    public static ILogger Logger { get; private set; }

    private string[] _args;

    public ApplicationStartup(string[] args)
    {
        _args = args ?? Array.Empty<string>();
        IoCContainer = new IoCContainer();
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();
        Log.Logger = Logger = logger;
        ModuleManager = new ModuleManager(IoCContainer, Configuration, Logger);
    }

    public async Task InitializeAsync()
    {
        Log.Logger.Information("Initializing Application...");
        ApplicationStartup.ModuleManager.LoadModules();
        _application = new GtkApplication();
        await _application.InitializeAsync(CancellationToken.None);
        Log.Logger.Information("Application Initialization Complete.");
        IoCContainer.RegisterInstance(ModuleManager);
        IoCContainer.RegisterInstance(Logger);
        var resolver = IoCContainer as IIoCResolver;
        IoCContainer.RegisterInstance(resolver);
        ApplicationStartup.IoCContainer.Build();
    }

    public async Task StartAsync()
    {
        Log.Logger.Information("Starting Application...");
        await _application.RunAsync();
        Log.Logger.Information("Application Starting complete.");
    }
}
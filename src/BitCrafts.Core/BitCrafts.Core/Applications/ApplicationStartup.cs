using BitCrafts.Core.ConsoleApplication.Extensions;
using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BitCrafts.Core.Applications;

public class ApplicationStartup : IApplicationStartup
{
    public IServiceProvider ServiceProvider { get; private set; }
    public IServiceCollection Services { get; private set; }
    public IApplication Application { get; private set; }

    public ApplicationStartup()
    {
        CreateDirectoryDirectory("Modules");
        CreateDirectoryDirectory("Settings");
        CreateDirectoryDirectory("Databases");
        CreateDirectoryDirectory("Files");
        CreateDirectoryDirectory("Temporary");
        Services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables()
            .Build();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        Log.Logger = logger;

        Services.AddSingleton<IConfiguration>(configuration);

        Services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true)
        );
        var appType = configuration.GetSection("ApplicationSettings").GetValue<string>("Type")?.ToLower();

        switch (appType)
        {
            case "console":
            default:
                Services.AddConsoleApplication();
                break;
        }

        Services.AddBitCraftsCore();
    }

    public async Task StartAsync()
    {
        using (var moduleManager = new ModuleManager())
        {
            moduleManager.LoadModules(Services);
            ServiceProvider = Services.BuildServiceProvider();
            Log.Logger.Information("Modules loaded.");
            Application = ServiceProvider.GetService<IApplication>();
            Application.Run();
        }

        await Task.CompletedTask;
    }

    public void Dispose()
    {
        Log.Logger.Information("Disposing Application...");
        Application?.Dispose();
    }

    private void CreateDirectoryDirectory(string directory)
    {
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), directory);
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(Path.GetFullPath(directoryPath));
    }
}
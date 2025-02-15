using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BitCrafts.Core.Applications;

public class ApplicationStartup : IApplicationStartup
{
    public ApplicationStartup()
    {
        ServiceProvider = null;
        Services = new ServiceCollection();
        ModuleManager = new ModuleManager();
        Application = new AvaloniaApplication();
        Services.AddSingleton(ModuleManager);
        Services.AddBitCraftsCore();
        CreateModulesDirectory();
    }

    internal static IServiceProvider ServiceProvider { get; private set; }
    internal static IServiceCollection Services { get; private set; }
    internal static IModuleManager ModuleManager { get; private set; }
    internal static IApplication Application { get; private set; }

    public void Start()
    {
        Log.Logger.Information("Starting Application...");
        ModuleManager.LoadModules(Services);
        BuildServiceProvider();
        Application.Run();
    }

    public void Dispose()
    {
        Log.Logger.Information("Disposing Application...");
        Application?.Dispose();
    }

    private void CreateModulesDirectory()
    {
        var modulesPath = Path.Combine(Directory.GetCurrentDirectory(), "Modules");
        if (!Directory.Exists(modulesPath))
            Directory.CreateDirectory(Path.GetFullPath(modulesPath));
    }

    internal static void BuildServiceProvider()
    {
        ServiceProvider = Services.BuildServiceProvider();
    }
}
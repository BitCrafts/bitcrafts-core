using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BitCrafts.Core.Applications;

public class ApplicationStartup : IApplicationStartup
{
    public static IServiceProvider ServiceProvider { get; private set; }
    public static IServiceCollection Services { get; private set; }
    public static IModuleManager ModuleManager { get; private set; }
    public static IApplication Application { get; private set; }


    public ApplicationStartup()
    {
        ServiceProvider = null;
        Services = new ServiceCollection();
        ModuleManager = new ModuleManager();
        Application = new GtkApplication();
        Services.AddSingleton<IModuleManager>(ModuleManager);
        Services.AddSingleton<IApplication>(Application);
        Services.AddBitCraftsCore();
        CreateModulesDirectory();
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

    public void Start()
    {
        Log.Logger.Information("Starting Application..."); 
        ModuleManager.LoadModules(Services);
        Application.Run();
    }

    public void Dispose()
    {
        Log.Logger.Information("Disposing Application...");
        Application?.Dispose();
    }
}
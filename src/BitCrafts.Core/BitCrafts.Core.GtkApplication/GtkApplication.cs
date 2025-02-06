using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using Serilog;

namespace BitCrafts.Core.GtkApplication;

public class GtkApplication : BaseApplication
{

    protected override void InitializeApplication(string[] args)
    {
        IoCContainer.Resolve<IApplicationStartup>().Initialize();
        IoCContainer.Rebuild();
        Logger.Information("Console application-specific initialization...");
    }

    protected override void ExecuteApplication()
    {
        Logger.Information("Running Application Startup...");
        IoCContainer.Resolve<IApplicationStartup>().Start();
    }

    protected override void ShutdownApplication()
    {
        Logger.Information("Cleaning up resources...");
    }
}
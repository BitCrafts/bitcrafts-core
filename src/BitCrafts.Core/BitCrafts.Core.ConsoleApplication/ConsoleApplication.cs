using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Core.ConsoleApplication;

public class ConsoleApplication : BaseApplication
{
    protected override void InitializeApplication(string[] args)
    {
        IoCContainer.Rebuild();
        Logger.Information("Console initialization...");
    }

    protected override void ExecuteApplication()
    {
        Logger.Information("Running Console Application Logic...");
    }

    protected override void ShutdownApplication()
    {
        Logger.Information("Cleaning up console-specific resources...");
    }
}
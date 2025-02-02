using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core.ConsoleApplication;

public class ConsoleApplication : BaseApplication
{
    public ConsoleApplication(IIoCContainer container)
        : base(container)
    {
    }

    protected override string GetApplicationName()
    {
        // Lire le nom depuis la configuration
        return Configuration["ApplicationSettings:Name"] ?? "Console Application";
    }

    protected override void InitializeApplication()
    {
        base.InitializeApplication();
        IoCContainer.Register<IUserInteraction, ConsoleUserInteraction>(ServiceLifetime.Singleton);
        IoCContainer.Rebuild();
        Logger.Information("Console application-specific initialization...");
    }

    protected override void ExecuteApplication()
    {
        Logger.Information("Running Console Application Logic...");
        IoCContainer.Resolve<IUserInteraction>().StartInteractionLoop();
    }

    protected override void ShutdownApplication()
    {
        Logger.Information("Cleaning up console-specific resources...");
        base.ShutdownApplication();
    }
}
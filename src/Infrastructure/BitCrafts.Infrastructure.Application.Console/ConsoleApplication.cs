using BitCrafts.Infrastructure.Abstraction.Application;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Console;

public sealed class ConsoleApplication : BaseApplication
{
    public ConsoleApplication(ILogger<ConsoleApplication> logger, IServiceProvider serviceProvider)
        : base(logger, serviceProvider)
    {
    }

    public override async Task StartAsync()
    {
        await base.StartAsync();
        Logger.LogInformation("Application console démarrée.");
        System.Console.WriteLine("Application console démarrée. Appuyez sur une touche pour quitter.");
        System.Console.ReadKey();
    }
}
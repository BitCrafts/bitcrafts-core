using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.ConsoleDemo;

internal class Program
{
    private static async Task Main(string[] args)
    {
        IApplicationStartup apptStartup = new ApplicationStartup(args);
        await apptStartup.InitializeAsync();
        await apptStartup.StartAsync();
    }
}
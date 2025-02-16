using BitCrafts.Core.Applications;

namespace BitCrafts.GestionCommerciale.Application;

internal class Program
{
    [STAThread]
    private static async Task Main(string[] args)
    {
        using var apptStartup = new ApplicationStartup();
        await apptStartup.StartAsync();
    }
}
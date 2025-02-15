using BitCrafts.Core.Applications;

namespace BitCrafts.GestionCommerciale.Application;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        using var apptStartup = new ApplicationStartup();
        apptStartup.Start();
    }
}
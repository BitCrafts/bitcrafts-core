using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.ConsoleDemo;

internal class Program
{
    private static void Main(string[] args)
    {
        IApplicationStartup apptStartup = new ApplicationStartup(args);
        apptStartup.Initialize();
        apptStartup.Start();
    }
}
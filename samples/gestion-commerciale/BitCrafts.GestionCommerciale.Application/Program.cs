using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.GestionCommerciale.Application;

class Program
{
    static void Main(string[] args)
    {
        using var apptStartup = new ApplicationStartup();
        apptStartup.Start();
    }
}
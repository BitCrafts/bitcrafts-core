using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.Core.ConsoleApplication;

public class ConsoleApplication : BaseApplication, IConsoleApplication
{
    public ConsoleApplication()
    {
    }

    public override Task RunAsync()
    {
        throw new NotImplementedException();
    }
}
using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.Core.Applications;

public sealed class BitCraftsApplication : IApplication
{
    private readonly INativeApplication _nativeApplication;

    public BitCraftsApplication(INativeApplication nativeApplication)
    {
        _nativeApplication = nativeApplication;
    }

    public void Dispose()
    {
        _nativeApplication.Dispose();
    }

    public void Run()
    {
        _nativeApplication.Run();
    }

    public void Shutdown()
    {
        _nativeApplication.Shutdown();
    }
}
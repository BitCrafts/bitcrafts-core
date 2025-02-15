using Avalonia;
using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.Core.Applications;

public class AvaloniaApplication : IApplication
{
    private readonly AppBuilder _appBuilder;

    public AvaloniaApplication()
    {
        _appBuilder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }

    public void Dispose()
    {
    }

    public void Run()
    {
        _appBuilder.StartWithClassicDesktopLifetime(Environment.GetCommandLineArgs());
    }

    public void Shutdown()
    {
    }
}
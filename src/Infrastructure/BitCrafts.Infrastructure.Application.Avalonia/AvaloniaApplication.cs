using System;
using System.Threading.Tasks;
using Avalonia;
using BitCrafts.Infrastructure.Abstraction.Application;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaApplication : BaseApplication, IApplication
{
    private AppBuilder _appbuilder;

    public AvaloniaApplication(ILogger<BaseApplication> logger, IServiceProvider serviceProvider) : base(logger,
        serviceProvider)
    {
        _appbuilder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }

    public override async Task StartAsync()
    {
        await base.StartAsync();
        Logger.LogInformation("Starting Avalonia Application...");
        _appbuilder.StartWithClassicDesktopLifetime(Environment.GetCommandLineArgs());
    }
}
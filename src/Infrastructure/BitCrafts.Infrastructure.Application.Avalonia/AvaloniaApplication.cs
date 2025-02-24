using System;
using System.Threading.Tasks;
using Avalonia;
using BitCrafts.Infrastructure.Abstraction.Application;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaApplication : BaseApplication, IApplication
{
    private readonly AppBuilder _appbuilder;

    public AvaloniaApplication(ILogger<BaseApplication> logger, IServiceProvider serviceProvider) : base(logger,
        serviceProvider)
    {
        ServiceProvider = serviceProvider;
        _appbuilder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }

    public static IServiceProvider ServiceProvider { get; private set; }

    protected override Task OnStartupAsync()
    {
        Logger.LogInformation("Starting Avalonia Application...");
        _appbuilder.StartWithClassicDesktopLifetime(Environment.GetCommandLineArgs());
        return Task.CompletedTask;
    }
}
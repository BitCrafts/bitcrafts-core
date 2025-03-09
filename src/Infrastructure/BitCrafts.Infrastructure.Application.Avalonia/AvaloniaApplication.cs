using System;
using System.Threading.Tasks;
using Avalonia;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaApplication : BaseApplication
{
    private readonly AppBuilder _appbuilder;

    public AvaloniaApplication(ILogger<BaseApplication> logger, IServiceProvider serviceProvider,
        IBackgroundThreadDispatcher backgroundThreadDispatcher) : base(logger, backgroundThreadDispatcher)
    {
        ServiceProvider = serviceProvider;
        _appbuilder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }

    protected override Task OnStartupAsync()
    {
        try
        {
            App.ServiceProvider = ServiceProvider;
            Logger.LogInformation("Starting Avalonia Application...");
            _appbuilder.StartWithClassicDesktopLifetime(Environment.GetCommandLineArgs());
        }
        catch (Exception exc)
        {
            Logger.LogCritical(exc, "Failed to start Avalonia Application");
        }

        return Task.CompletedTask;
    }
}
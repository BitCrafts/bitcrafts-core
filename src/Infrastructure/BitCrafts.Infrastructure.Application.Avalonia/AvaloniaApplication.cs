using System;
using System.Threading.Tasks;
using Avalonia;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaApplication : BaseApplication, IApplication
{
    private readonly AppBuilder _appbuilder;
    public static IServiceProvider ServiceProvider { get; private set; }
    public static IUiManager UiManager { get; private set; }

    public AvaloniaApplication(ILogger<BaseApplication> logger, IServiceProvider serviceProvider) : base(logger,
        serviceProvider)
    {
        ServiceProvider = serviceProvider;
        UiManager = ServiceProvider.GetRequiredService<IUiManager>();
        _appbuilder = AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }


    protected override Task OnStartupAsync()
    {
        Logger.LogInformation("Starting Avalonia Application...");
        _appbuilder.StartWithClassicDesktopLifetime(Environment.GetCommandLineArgs());
        return Task.CompletedTask;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            UiManager.Dispose();
        }
        base.Dispose(disposing);
    }
}
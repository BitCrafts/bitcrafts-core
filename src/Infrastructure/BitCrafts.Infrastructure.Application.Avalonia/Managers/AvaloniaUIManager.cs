using System;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Presenters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaUiManager : IUiManager
{
    private readonly IServiceProvider _serviceProvider;
    private IClassicDesktopStyleApplicationLifetime _applicationLifetime;

    public AvaloniaUiManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task ShutdownAsync()
    {
        _serviceProvider.GetService<ILogger<AvaloniaUiManager>>().LogInformation("Shutting down application...");
        return Task.CompletedTask;
    }


    public void Dispose()
    {
        _serviceProvider.GetService<ILogger<AvaloniaUiManager>>().LogInformation("Disposing AvaloniaUIManager");
        _serviceProvider.GetService<IWorkspaceManager>().Dispose();
    }

    public void SetNativeApplication(IClassicDesktopStyleApplicationLifetime applicationLifetime)
    {
        _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        _applicationLifetime.ShutdownRequested += ApplicationLifetimeOnShutdownRequested;
        _applicationLifetime.Exit += ApplicationLifetimeOnExit;
        _applicationLifetime.Startup += ApplicationLifetimeOnStartup;
    }

    private void ApplicationLifetimeOnStartup(object sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
        _serviceProvider.GetService<ILogger<AvaloniaUiManager>>().LogInformation("ApplicationLifetime Startup");
        _serviceProvider.GetRequiredService<IExceptionManager>();
        _serviceProvider.GetRequiredService<IWindowManager>().ShowWindow<IMainPresenter>();
    }

    private void ApplicationLifetimeOnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        _serviceProvider.GetService<ILogger<AvaloniaUiManager>>().LogInformation("ApplicationLifetime Exit");
        _serviceProvider.GetRequiredService<IWindowManager>().Dispose();
    }

    private void ApplicationLifetimeOnShutdownRequested(object sender, ShutdownRequestedEventArgs e)
    {
        _serviceProvider.GetService<ILogger<AvaloniaUiManager>>()
            .LogInformation("ApplicationLifetime ShutdownRequested");
    }
}
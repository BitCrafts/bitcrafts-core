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
    private readonly ILogger<AvaloniaUiManager> _logger;
    private readonly IWorkspaceManager _workspaceManager;
    private readonly IExceptionManager _exceptionManager;
    private readonly IWindowManager _windowManager; 
    private IClassicDesktopStyleApplicationLifetime _applicationLifetime;

    public AvaloniaUiManager(ILogger<AvaloniaUiManager> logger, IWorkspaceManager workspaceManager,
        IExceptionManager exceptionManager,
        IWindowManager windowManager)
    {
        _logger = logger;
        _workspaceManager = workspaceManager;
        _exceptionManager = exceptionManager;
        _windowManager = windowManager;
    }

    public Task ShutdownAsync()
    {
        _logger.LogInformation("Shutting down application...");
        return Task.CompletedTask;
    }


    public void Dispose()
    {
        _logger.LogInformation("Disposing AvaloniaUIManager");
        _workspaceManager.Dispose();
        _windowManager.Dispose();
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
        _logger.LogInformation("ApplicationLifetime Startup");
        _windowManager.ShowWindow<IMainPresenter>();
    }

    private void ApplicationLifetimeOnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        _logger.LogInformation("ApplicationLifetime Exit");
        Dispose();
    }

    private void ApplicationLifetimeOnShutdownRequested(object sender, ShutdownRequestedEventArgs e)
    {
        _logger
            .LogInformation("ApplicationLifetime ShutdownRequested");
    }
}
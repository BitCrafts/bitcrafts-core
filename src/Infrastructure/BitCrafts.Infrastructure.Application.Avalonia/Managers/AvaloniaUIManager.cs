using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaUiManager : IUiManager
{
    private readonly IServiceProvider _serviceProvider;
    private IClassicDesktopStyleApplicationLifetime _applicationLifetime;
    private AvaloniaWindowingManager _windowingManager;
    private bool _isInitialized;
    private IStartupPresenter _startupPresenter;

    public AvaloniaUiManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync()
    {
        if (_isInitialized)
            return;
        _isInitialized = true;

        _startupPresenter = _serviceProvider.GetService<IStartupPresenter>();
        await _startupPresenter.ShowAsync();
    }

    public async Task ShutdownAsync()
    {
        _serviceProvider.GetService<ILogger<AvaloniaUiManager>>().LogInformation("Shutting down application...");
        await Task.Delay(100);
        _applicationLifetime.Shutdown();
    }

    public void Dispose()
    {
        _startupPresenter.Dispose();
        _serviceProvider.GetService<ILogger<AvaloniaUiManager>>().LogInformation("Disposing AvaloniaUIManager");
    }

    public void SetNativeApplication(IClassicDesktopStyleApplicationLifetime applicationLifetime)
    {
        if (_isInitialized)
            return;

        _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        _applicationLifetime.ShutdownRequested += ApplicationLifetimeOnShutdownRequested;
        _applicationLifetime.Exit += ApplicationLifetimeOnExit;
        _applicationLifetime.Startup += ApplicationLifetimeOnStartup; 
        _windowingManager = _serviceProvider.GetService<IWindowingManager>() as AvaloniaWindowingManager;
        if (_windowingManager != null) _windowingManager.SetNativeApplication(_applicationLifetime);
    }

    private void ApplicationLifetimeOnStartup(object sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
        _serviceProvider.GetService<ILogger<AvaloniaUiManager>>().LogInformation("ApplicationLifetime Startup");
    }

    private void ApplicationLifetimeOnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        _serviceProvider.GetService<ILogger<AvaloniaUiManager>>().LogInformation("ApplicationLifetime Exit");
    }

    private void ApplicationLifetimeOnShutdownRequested(object sender, ShutdownRequestedEventArgs e)
    {
        _serviceProvider.GetService<ILogger<AvaloniaUiManager>>()
            .LogInformation("ApplicationLifetime ShutdownRequested");
    }
}
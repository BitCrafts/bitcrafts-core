using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaUiManager : IUiManager
{
    private readonly IServiceProvider _serviceProvider;
    private IClassicDesktopStyleApplicationLifetime _applicationLifetime;
    private readonly IWindowingManager _windowingManager;
    private bool _isInitialized;

    public AvaloniaUiManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _windowingManager = serviceProvider.GetService<IWindowingManager>();
        ((AvaloniaWindowingManager)_windowingManager).SetNativeApplication(_applicationLifetime);
    }

    public async Task StartAsync()
    {
        if (_isInitialized)
            return;
        _isInitialized = true;
        
        var startupApp = _serviceProvider.GetRequiredService<IApplicationStartup>();
        await startupApp.StartAsync();
    }
 
    public void Dispose()
    {
    }

    public void SetNativeApplication(IClassicDesktopStyleApplicationLifetime applicationLifetime)
    {
        if (_isInitialized)
            return;

        _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        _applicationLifetime.ShutdownMode = ShutdownMode.OnMainWindowClose;
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Infrastructure.Application.Avalonia.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaUiManager : IUiManager
{
    private readonly IServiceProvider _serviceProvider;
    private IClassicDesktopStyleApplicationLifetime _applicationLifetime;
    private bool _isInitialized;

    public AvaloniaUiManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync()
    {
        if (_isInitialized)
            return;
        _isInitialized = true;
        var startupApp = _serviceProvider.GetRequiredService<IApplicationStartup>();
        await startupApp.StartAsync();
    }

    public void SetMainWindow(IWindow window)
    {
        if (window == null)
            return;
        if (_applicationLifetime.MainWindow != null)
        {
            _applicationLifetime.MainWindow.Close();
            _applicationLifetime.MainWindow = null;
        }

        _applicationLifetime.MainWindow = (Window)window;
        _applicationLifetime.MainWindow.Show();
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

    /*private void LoadModuleViews()
    {
       
    }*/
}
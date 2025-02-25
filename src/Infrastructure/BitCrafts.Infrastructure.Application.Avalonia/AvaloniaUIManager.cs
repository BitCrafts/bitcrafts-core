using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.UI;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Infrastructure.Application.Avalonia.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaUiManager : IUiManager
{
    private readonly IStartupWindow _startupWindow;
    private readonly IModuleManager _moduleManager;
    private readonly IServiceProvider _serviceProvider;
    private IClassicDesktopStyleApplicationLifetime _applicationLifetime;
    private bool _isInitialized;

    public AvaloniaUiManager(IServiceProvider serviceProvider)
    {
        _moduleManager = serviceProvider.GetService<IModuleManager>();
        _serviceProvider = serviceProvider;
        _startupWindow = serviceProvider.GetService<IStartupWindow>();
    }

    public async Task StartAsync()
    {
        if (_isInitialized)
            return;

        _isInitialized = true;
        SetMainWindow(_startupWindow);
        await Task.Delay(300);
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
        foreach (var module in _moduleManager.Modules)
        {
            var moduleName = module.Value.Name;
            var viewType = module.Value.GetViewType();
            _views.TryAdd(moduleName, viewType);
            var view = _serviceProvider.GetRequiredService(viewType) as IView;
            if (view == null)
                throw new InvalidOperationException($"Failed to resolve view for module: {moduleName}");

            var userControl = (UserControl)view;
            if (userControl == null)
                throw new InvalidOperationException($"View for module {moduleName} does not provide a UserControl.");

            ModuleControles.Add(moduleName, userControl);
        }
    }*/
}
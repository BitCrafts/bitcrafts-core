using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.UI;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Infrastructure.Application.Avalonia.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaUiManager : IUiManager
{
    private readonly IApplicationStartup _applicationStartup;
    private readonly IModuleManager _moduleManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly ISplashScreen _splashScreen;
    private readonly MainWindow _mainWindow;
    private readonly Dictionary<string, Type> _views = new();

    private IClassicDesktopStyleApplicationLifetime? _applicationLifetime;
    private bool _isInitialized;

    public AvaloniaUiManager(
        IApplicationStartup applicationStartup,
        IModuleManager moduleManager,
        IServiceProvider serviceProvider)
    {
        _applicationStartup = applicationStartup ?? throw new ArgumentNullException(nameof(applicationStartup));
        _moduleManager = moduleManager ?? throw new ArgumentNullException(nameof(moduleManager));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

        _splashScreen = new AvaloniaSplashScreen();
        _mainWindow = new MainWindow();
    }

    public void SetNativeApplication(IClassicDesktopStyleApplicationLifetime applicationLifetime)
    {
        if (_isInitialized)
            return;

        _applicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
        _applicationLifetime.MainWindow = _splashScreen.GetNativeObject<Window>();
        _applicationLifetime.ShutdownMode = ShutdownMode.OnMainWindowClose;
    }

    public async Task StartAsync()
    {
        if (_isInitialized)
            return;

        _isInitialized = true;


        await _splashScreen.ShowAsync();


        await UpdateSplashScreenAsync("Loading modules...", LoadModuleViews);


        await UpdateSplashScreenAsync("Loading main window...", () =>
        {
            _applicationLifetime!.MainWindow = _mainWindow;
            return Task.CompletedTask;
        });

        _splashScreen.Close();
        _applicationLifetime.MainWindow.Show();
    }

    private async Task LoadModuleViews()
    {
        var modules = new Dictionary<string, UserControl>();

        foreach (var module in _moduleManager.Modules)
        {
            string moduleName = module.Value.Name;
            _splashScreen.SetText($"Loading {moduleName} Module...");

            var viewType = module.Value.GetViewType();
            _views.TryAdd(moduleName, viewType);

            var view = _serviceProvider.GetRequiredService(viewType) as IView;
            if (view == null)
                throw new InvalidOperationException($"Failed to resolve view for module: {moduleName}");

            var userControl = (UserControl)view;
            if (userControl == null)
                throw new InvalidOperationException($"View for module {moduleName} does not provide a UserControl.");

            modules.Add(moduleName, userControl);

            await Task.Delay(3000);
        }

        _mainWindow.InitializeMenuList(modules);
    }

    private async Task UpdateSplashScreenAsync(string statusMessage, Func<Task> action)
    {
        if (string.IsNullOrWhiteSpace(statusMessage))
            throw new ArgumentException("Status message cannot be null or empty.", nameof(statusMessage));

        _splashScreen.SetText(statusMessage);
        await action();
    }

    public void Dispose()
    {
        _splashScreen.Dispose();
    }
}
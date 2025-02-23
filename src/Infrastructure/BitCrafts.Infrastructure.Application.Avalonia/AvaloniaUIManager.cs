using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.UI;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaUiManager : IUiManager
{
    private readonly ISplashScreen _splashScreen;
    private IClassicDesktopStyleApplicationLifetime _applicationLifetime;
    private bool _isInitialized;
    private readonly Dictionary<IWindow, Window> _windows = new();
    private readonly Dictionary<IView, UserControl> _views = new();


    public AvaloniaUiManager(ISplashScreen splashScreen)
    {
        _splashScreen = splashScreen;
    }

    public void SetNativeApplication(IClassicDesktopStyleApplicationLifetime applicationLifetime)
    {
        if (_isInitialized)
            return;
        _applicationLifetime = applicationLifetime;
        _applicationLifetime.MainWindow = _splashScreen.GetNativeObject<Window>();
        _applicationLifetime.ShutdownMode = ShutdownMode.OnMainWindowClose;
    }

    public async Task StartAsync()
    {
        if (_isInitialized)
            return;
        _isInitialized = true;
        await _splashScreen.ShowAsync();
    }

    public async Task ShowWindowAsync(IWindow window)
    {
        _applicationLifetime.MainWindow = window.GetNativeWindow<Window>();
        _applicationLifetime.MainWindow.Show();
    }

    public async Task CloseWindowAsync(IWindow window)
    {
        throw new System.NotImplementedException();
    }

    public async Task LoadViewAsync(IView view)
    {
        throw new System.NotImplementedException();
    }

    public async Task UnloadViewAsync(IView view)
    {
        throw new System.NotImplementedException();
    }

    public void Dispose()
    {
        _splashScreen?.Dispose();
    }
}
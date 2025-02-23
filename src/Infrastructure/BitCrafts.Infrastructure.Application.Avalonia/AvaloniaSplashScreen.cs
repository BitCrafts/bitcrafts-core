using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Infrastructure.Application.Avalonia.Windows;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public sealed class AvaloniaSplashScreen : ISplashScreen
{
    private readonly IApplicationStartup _applicationStartup;
    private readonly IBackgroundThreadScheduler _backgroundThreadScheduler;
    private readonly IUiManager _uiManager;
    private readonly Window _splashWindow;

    public AvaloniaSplashScreen(IApplicationStartup applicationStartup,
        IBackgroundThreadScheduler backgroundThreadScheduler)
    {
        _applicationStartup = applicationStartup;
        _backgroundThreadScheduler = backgroundThreadScheduler; 
        _splashWindow = new SplashScreen
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            Topmost = true
        };
    }

    public async Task ShowAsync()
    {
        _splashWindow.Show();
        _backgroundThreadScheduler.Schedule(() => { _applicationStartup.StartAsync(); });
        await Task.Delay(2000);
        _splashWindow.Close();
        await _uiManager.ShowWindowAsync(new MainWindow());
    }

    public void Close()
    {
        _splashWindow.Close();
    }

    public T GetNativeObject<T>() where T : class
    {
        return _splashWindow as T;
    }

    public void Dispose()
    {
        Close();
    }
}
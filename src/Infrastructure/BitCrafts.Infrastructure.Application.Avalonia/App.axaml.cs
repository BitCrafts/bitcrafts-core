using System;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public class App : global::Avalonia.Application
{
    private IServiceScope _rootScope;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        _rootScope = AvaloniaApplication.ServiceProvider.CreateScope();
        var uiManager = (AvaloniaUiManager)_rootScope.ServiceProvider.GetRequiredService<IUiManager>();
        var desktop = ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        if (desktop != null)
        {
            desktop.ShutdownMode = ShutdownMode.OnLastWindowClose;
            uiManager.SetNativeApplication(desktop);
            desktop.Exit += (_, __) => _rootScope.Dispose();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
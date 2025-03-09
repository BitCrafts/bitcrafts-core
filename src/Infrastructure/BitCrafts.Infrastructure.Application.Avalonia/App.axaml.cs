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
    public static IServiceProvider ServiceProvider { get; set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();
        var uiManager = (AvaloniaUiManager)ServiceProvider.GetRequiredService<IUiManager>();
        var desktop = (IClassicDesktopStyleApplicationLifetime)ApplicationLifetime;
        if (desktop == null) return;
        desktop.ShutdownMode = ShutdownMode.OnLastWindowClose;
        uiManager.SetNativeApplication(desktop);
    }
}
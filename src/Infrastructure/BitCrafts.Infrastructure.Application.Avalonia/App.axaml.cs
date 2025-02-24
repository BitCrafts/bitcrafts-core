using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public class App : global::Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        var uiManager =
            (AvaloniaUiManager)AvaloniaApplication.ServiceProvider.GetRequiredService<IUiManager>();
        var desktop = ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        uiManager.SetNativeApplication(desktop);
        await uiManager.StartAsync();
        base.OnFrameworkInitializationCompleted();
    }
}
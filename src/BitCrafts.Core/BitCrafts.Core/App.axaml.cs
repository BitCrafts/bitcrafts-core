using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BitCrafts.Core.Applications;
using BitCrafts.Core.Presenters;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core;

public class App : Application
{
    private IMainPresenter _presenter;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            _presenter = ApplicationStartup.ServiceProvider.GetRequiredService<IMainPresenter>();
            var window = _presenter.GetNativeWidget<Window>();
            desktop.MainWindow = window;
            desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;
            desktop.ShutdownRequested += DesktopOnShutdownRequested;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DesktopOnShutdownRequested(object sender, ShutdownRequestedEventArgs e)
    {
        _presenter.Dispose();
    }
}
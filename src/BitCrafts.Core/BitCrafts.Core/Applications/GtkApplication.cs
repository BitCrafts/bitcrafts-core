using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.Presenters;
using BitCrafts.Core.Views;
using Gio;
using Gtk;
using Microsoft.Extensions.DependencyInjection;
using Application = Gtk.Application;
using Task = System.Threading.Tasks.Task;

namespace BitCrafts.Core.Applications;

public class GtkApplication : BaseApplication, IGtkApplication
{
    private Application _app;

    public override Task InitializeAsync(CancellationToken cancellationToken)
    {
        ApplicationStartup.IoCContainer.Register<IMainWindowView, MainWindowView>(ServiceLifetime.Singleton);
        ApplicationStartup.IoCContainer.Register<IMainPresenter, MainPresenter>(ServiceLifetime.Singleton);
        return Task.CompletedTask;
    }

    public override async Task RunAsync()
    {
        ApplicationStartup.IoCContainer.Build();

        _app = Application.New("com.bitcrafts.gtkapp", ApplicationFlags.DefaultFlags);
        _app.OnActivate += (o, e) =>
        {
            if (ApplicationStartup.IoCContainer.Resolve<IMainWindowView>() is Window window)
            {
                window.OnDestroy += WindowOnOnDestroy;
                _app.AddWindow(window);
                if (window is IMainWindowView view)
                {
                    view.InitializeView();
                    view.ShowView();
                }
            }
        };
        _app.Run(0, null);

        await Task.FromResult(0);
    }

    public override Task ShutdownAsync(CancellationToken cancellationToken)
    {
        if (_app != null)
            _app.Quit();
        return Task.CompletedTask;
    }

    private void WindowOnOnDestroy(Widget sender, EventArgs args)
    {
        ShutdownAsync(CancellationToken.None).Wait();
    }
}
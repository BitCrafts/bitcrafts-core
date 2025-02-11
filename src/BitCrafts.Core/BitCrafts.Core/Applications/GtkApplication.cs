using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.Presenters;
using BitCrafts.Core.Views;
using Gio;
using Gtk;
using Microsoft.Extensions.DependencyInjection;
using Task = System.Threading.Tasks.Task;

namespace BitCrafts.Core.Applications;

public class GtkApplication : BaseApplication, IGtkApplication
{
    private Gtk.Application _app;

    public GtkApplication()
    {
    }

    public override Task InitializeAsync(CancellationToken cancellationToken)
    {
        ApplicationStartup.IoCContainer.Register<IMainWindowView, MainWindowView>(ServiceLifetime.Singleton);
        ApplicationStartup.IoCContainer.Register<IMainPresenter, MainPresenter>(ServiceLifetime.Singleton);
        return Task.CompletedTask;
    }

    public override async Task RunAsync()
    {
        ApplicationStartup.IoCContainer.Build();

        _app = Gtk.Application.New("com.bitcrafts.gtkapp", ApplicationFlags.DefaultFlags);
        _app.OnActivate += (o, e) =>
        {
            var window = ApplicationStartup.IoCContainer.Resolve<IMainWindowView>() as Window;
            var view = window as IMainWindowView; 
            if (window != null)
            {
                window.OnDestroy += (oe, ee) => _app.Quit();
                _app.AddWindow(window); 
                view.InitializeView();
                view.ShowView();
                
               
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
}
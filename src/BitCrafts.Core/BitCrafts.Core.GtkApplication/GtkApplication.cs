using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.GtkApplication.Presenters;
using BitCrafts.Core.GtkApplication.Views;
using Gio;
using Gtk;
using Microsoft.Extensions.DependencyInjection;
using Task = System.Threading.Tasks.Task;

namespace BitCrafts.Core.GtkApplication;

public class GtkApplication : BaseApplication, IGtkApplication
{
    private readonly IIoCContainer _ioCContainer;

    public GtkApplication(IIoCContainer ioCContainer)
    {
        _ioCContainer = ioCContainer;
    }

    public override Task InitializeAsync(CancellationToken cancellationToken)
    {
        _ioCContainer.Register<IMainView, MainView>(ServiceLifetime.Singleton);
        _ioCContainer.Register<IMainPresenter, MainPresenter>(ServiceLifetime.Singleton);
        _ioCContainer.Rebuild();
        return Task.CompletedTask;
    }

    public override Task RunAsync()
    {
        return Task.Run(() =>
        {
            var app = Gtk.Application.New("com.bitcrafts.gtkapp", ApplicationFlags.DefaultFlags);
            app.OnActivate += (o, e) =>
            {
                var window = _ioCContainer.Resolve<IMainView>() as Window;
                var mainView = window as MainView;
                window.OnDestroy += (oe, ee) => app.Quit();
                app.AddWindow(window);
                window.Show();
            };

            app.Run(0, null);
        });
        return Task.CompletedTask;
    }
}
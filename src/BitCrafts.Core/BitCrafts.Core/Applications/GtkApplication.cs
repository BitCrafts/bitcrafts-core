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
    public GtkApplication()
    {
    }

    public override Task InitializeAsync(CancellationToken cancellationToken)
    {
        ApplicationStartup.IoCContainer.Register<IMainView, MainView>(ServiceLifetime.Singleton);
        ApplicationStartup.IoCContainer.Register<IMainPresenter, MainPresenter>(ServiceLifetime.Singleton);
        return Task.CompletedTask;
    }

    public override async Task RunAsync()
    {
        ApplicationStartup.IoCContainer.Build();

        var app = Gtk.Application.New("com.bitcrafts.gtkapp", ApplicationFlags.DefaultFlags);
        app.OnActivate += (o, e) =>
        {
            var mainView = ApplicationStartup.IoCContainer.Resolve<IMainView>() as Window;
            if (mainView != null)
            {
                mainView.OnDestroy += (oe, ee) => app.Quit();
                app.AddWindow(mainView);
                mainView.Show();
            }
        };
        app.Run(0, null);
        await Task.FromResult(0);
    }
}
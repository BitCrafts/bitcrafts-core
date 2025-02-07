using BitCrafts.Core.Applications;
using BitCrafts.Core.Contracts.Applications;
using Gio;
using Gtk;
using Task = System.Threading.Tasks.Task;

namespace BitCrafts.Core.GtkApplication;

public class GtkApplication : BaseApplication, IGtkApplication
{
    public GtkApplication()
    {
    }

    public override Task RunAsync()
    {
        var app = Gtk.Application.New("com.bitcrafts.gtkapp", ApplicationFlags.DefaultFlags);
        app.OnActivate += (o, e) =>
        {
            Window window = new Window();
            window.SetDefaultSize(400, 300);
            window.OnDestroy += (oe, ee) => app.Quit();
            app.AddWindow(window);
            window.Show();
        };

        // Ajoutez ici vos widgets et logiques

        app.Run(0, null);
        return Task.CompletedTask;
    }
}
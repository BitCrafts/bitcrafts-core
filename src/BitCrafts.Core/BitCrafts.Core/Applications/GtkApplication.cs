using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.Presenters;
using BitCrafts.Core.Views;
using Gio;
using Gtk;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BitCrafts.Core.Applications;

public class GtkApplication : IApplication
{
    private Gtk.Application _app;

    public GtkApplication()
    {
        TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
    }

    public void Run()
    {
        ApplicationStartup.BuildServiceProvider();
        RunApp();
    }

    private void RunApp()
    {
        var config = ApplicationStartup.ServiceProvider.GetRequiredService<IConfiguration>();
        var appId = config["ApplicationSettings:Id"];
        _app = Gtk.Application.New(appId, ApplicationFlags.DefaultFlags);
        _app.OnActivate += (o, e) =>
        {
            var mainPresenter = ApplicationStartup.ServiceProvider.GetRequiredService<IMainPresenter>();
            mainPresenter.Initialize();
            if (mainPresenter.View is IMainView mainView)
            {
                var window = mainView as Gtk.ApplicationWindow;
                if (window != null)
                {
                    window.Title = config["ApplicationSettings:Name"];
                    window.OnDestroy += (o1, e1) => Dispose();
                    _app.AddWindow(window);
                    window.Show();
                }
            }
        };
        _app.Run(0, null);
    }

    public void Dispose()
    {
        TaskScheduler.UnobservedTaskException -= TaskSchedulerOnUnobservedTaskException;
        AppDomain.CurrentDomain.UnhandledException -= CurrentDomainOnUnhandledException;
        GC.SuppressFinalize(this);
    }

    private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        try
        {
            if (e.ExceptionObject is Exception exception)
                Log.Logger.Fatal(exception, "Une exception non gérée a été levée.");
            else
                Log.Logger.Fatal(
                    "Une exception non gérée a été levée, mais l'objet d'exception n'est pas disponible.");
        }
        catch (Exception ex)
        {
            Log.Logger.Fatal(ex, "Erreur lors du traitement d'une exception non gérée.");
        }
        finally
        {
            if (e.IsTerminating)
            {
                //TODO: add code to properly terminate the application.
            }
        }
    }

    private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        try
        {
            Log.Logger.Fatal(e.Exception, "Une exception non observée a été levée.");
        }
        finally
        {
            e.SetObserved();
        }
    }
}
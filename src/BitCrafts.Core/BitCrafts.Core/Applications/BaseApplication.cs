using BitCrafts.Core.Contracts.Applications;

namespace BitCrafts.Core.Applications;

public abstract class BaseApplication : IApplication
{
    protected BaseApplication()
    {
        TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
    }

    public virtual async Task InitializeAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public abstract Task RunAsync();

    public virtual async Task ShutdownAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        try
        {
            if (e.ExceptionObject is Exception exception)
                ApplicationStartup.Logger.Fatal(exception, "Une exception non gérée a été levée.");
            else
                ApplicationStartup.Logger.Fatal(
                    "Une exception non gérée a été levée, mais l'objet d'exception n'est pas disponible.");
        }
        catch (Exception ex)
        {
            ApplicationStartup.Logger.Fatal(ex, "Erreur lors du traitement d'une exception non gérée.");
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
            ApplicationStartup.Logger.Fatal(e.Exception, "Une exception non observée a été levée.");
        }
        finally
        {
            e.SetObserved();
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            TaskScheduler.UnobservedTaskException -= TaskSchedulerOnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomainOnUnhandledException;
        }
    }
}
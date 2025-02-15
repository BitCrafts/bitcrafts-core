using Serilog;

namespace BitCrafts.Core;

public static class GlobalExceptionHandler
{
    public static void HandleUnhandledException(Exception exception)
    {
        Log.Logger.Fatal(exception, "An unhandled exception occurred.");
    }

    public static void HandleTaskException(AggregateException exception)
    {
        Log.Logger.Fatal(exception, "An unobserved task exception occurred.");
    }

    public static void ConfigureGlobalExceptionHandling()
    {
        // Configure Task Scheduler
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            HandleTaskException(e.Exception);
            e.SetObserved();
        };

        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            if (e.ExceptionObject is Exception ex)
                HandleUnhandledException(ex);
        };
    }
}
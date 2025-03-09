using System;
using System.Threading.Tasks;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaExceptionManager : IExceptionManager
{
    private readonly ILogger<AvaloniaExceptionManager> _logger;

    public AvaloniaExceptionManager(ILogger<AvaloniaExceptionManager> logger)
    {
        _logger = logger;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
    }

    public void HandleException(Exception exception)
    {
        _logger.LogCritical(exception, "Unhandled exception");
    }

    private void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        HandleException(e.Exception);
        e.SetObserved();
    }

    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        HandleException((Exception)e.ExceptionObject);
    }
}
using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Application.Avalonia.Presenters;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaExceptionManager : IExceptionManager
{
    private readonly IWindowManager _windowManager;
    private readonly ILogger<AvaloniaExceptionManager> _logger;

    public AvaloniaExceptionManager(IWindowManager windowManager, ILogger<AvaloniaExceptionManager> logger)
    {
        _windowManager = windowManager;
        _logger = logger;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
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

    public void HandleException(Exception exception)
    {
        _logger.LogCritical(exception, "Unhandled exception");
    }
}
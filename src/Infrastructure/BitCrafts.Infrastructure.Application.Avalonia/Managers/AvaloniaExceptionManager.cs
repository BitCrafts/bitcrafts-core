using System;
using System.Threading.Tasks;
using Avalonia.Threading;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Application.Avalonia.Presenters;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaExceptionManager : IExceptionManager
{
    private readonly IWindowManager _windowManager;
    private readonly IErrorPresenter _presenter;

    public AvaloniaExceptionManager(IWindowManager windowManager, IErrorPresenter presenter)
    {
        _windowManager = windowManager;
        _presenter = presenter;
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
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            _presenter.SetException(exception);
            _windowManager.ShowWindow(_presenter as IPresenter, true);
        });
    }
}
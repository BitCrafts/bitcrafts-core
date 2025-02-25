using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Abstraction.Threading;

public abstract class BaseThreadDispatcher : IThreadDispatcher, IDisposable
{
    private readonly ILogger<BaseThreadDispatcher> _logger;
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private readonly Thread _dispatcherThread;
    private readonly BlockingCollection<Action> _taskQueue = new BlockingCollection<Action>();
    private bool _isDisposed;

    protected BaseThreadDispatcher(ILogger<BaseThreadDispatcher> logger, string threadName)
    {
        _logger = logger;
        _dispatcherThread = new Thread(DispatcherThreadStart)
        {
            IsBackground = true,
            Name = threadName
        };
        _logger.LogInformation($"{threadName} Created.");
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Start()
    {
        if (!_isDisposed)
        {
            _dispatcherThread.Start();
            _logger.LogInformation(
                $"{_dispatcherThread.Name} Started with ID {_dispatcherThread.ManagedThreadId}.");
        }
        else
        {
            _logger.LogWarning($"{_dispatcherThread.Name} Already started.");
        }
    }

    public void Stop()
    {
        _logger.LogInformation(
            $"Stopping {_dispatcherThread.Name} - {_dispatcherThread.ManagedThreadId}.");
        _cancellationTokenSource.Cancel();
        _dispatcherThread.Join();
        _taskQueue.Dispose();
        _cancellationTokenSource.Dispose();
    }

    public void Invoke(Action action)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, GetType());

        _taskQueue.Add(action);
    }

    public Task InvokeAsync(Action action)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, GetType());

        var tcs = new TaskCompletionSource<object>();
        _taskQueue.Add(() =>
        {
            try
            {
                action();
                tcs.SetResult(null);
            }
#pragma warning disable CA1031
            catch (Exception ex)
#pragma warning restore CA1031
            {
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }

    public Task<T> InvokeAsync<T>(Func<T> func)
    {
        ObjectDisposedException.ThrowIf(_isDisposed, GetType());

        var tcs = new TaskCompletionSource<T>();
        _taskQueue.Add(() =>
        {
            try
            {
                var result = func();
                tcs.SetResult(result);
            }
#pragma warning disable CA1031
            catch (Exception ex)
#pragma warning restore CA1031
            {
                tcs.SetException(ex);
            }
        });

        return tcs.Task;
    }

    private void DispatcherThreadStart()
    {
        try
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
                if (_taskQueue.TryTake(out var task, Timeout.Infinite, _cancellationTokenSource.Token))
                    task?.Invoke();
        }
        catch (OperationCanceledException)
        {
            // Graceful shutdown
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            Stop();
        }

        _isDisposed = true;
        _logger.LogInformation($"{_dispatcherThread.Name} Disposed");
    }
}
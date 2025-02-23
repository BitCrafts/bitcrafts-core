using System.Collections.Concurrent;
using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Threading;

public abstract class BaseThreadScheduler : TaskScheduler, IThreadScheduler
{
    protected readonly ILogger _logger;
    private readonly BlockingCollection<Task> _tasks = new BlockingCollection<Task>();
    private readonly Thread _thread;

    protected BaseThreadScheduler(ILogger logger, string threadName)
    {
        _logger = logger;
        _thread = new Thread(Execute)
        {
            IsBackground = true,
            Name = threadName
        };
    }

    public virtual void Start()
    {
        _thread.Start();
    }

    private void Execute()
    {
        foreach (var task in _tasks.GetConsumingEnumerable())
        {
            TryExecuteTask(task);
        }
    }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        return _tasks.ToArray();
    }

    protected override void QueueTask(Task task)
    {
        _tasks.Add(task);
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        // Ne permet pas l'exécution en ligne
        return false;
    }

    public void Dispose()
    {
        _tasks.CompleteAdding();
        _thread.Join();
        _tasks.Dispose();
    }

    public virtual void Stop()
    {
        Dispose();
    }

    public void Schedule(Action action)
    {
        Task.Factory.StartNew(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'exécution d'une tâche dans le scheduler.");
                throw;
            }
        }, CancellationToken.None, TaskCreationOptions.None, this);
    }

    public Task ScheduleAsync(Action action, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.Factory.StartNew(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'exécution d'une tâche dans le scheduler.");
                throw;
            }
        }, cancellationToken, TaskCreationOptions.None, this);
    }

    public Task<T> ScheduleAsync<T>(Func<T> function, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.Factory.StartNew(() =>
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'exécution d'une tâche dans le scheduler.");
                throw;
            }
        }, cancellationToken, TaskCreationOptions.None, this);
    }
}
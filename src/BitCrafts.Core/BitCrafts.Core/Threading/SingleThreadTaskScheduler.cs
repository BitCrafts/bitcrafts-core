using System.Collections.Concurrent;

namespace BitCrafts.Core.Threading;

public class SingleThreadTaskScheduler : TaskScheduler, IDisposable
{
    private readonly BlockingCollection<Task> _tasks = new BlockingCollection<Task>();
    private readonly Thread _thread;

    public SingleThreadTaskScheduler(string threadName = "SingleThreadScheduler")
    {
        _thread = new Thread(Execute)
        {
            IsBackground = true,
            Name = threadName
        };
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
}
using BitCrafts.Infrastructure.Abstraction.Threading;

namespace BitCrafts.Infrastructure.Threading;

public sealed class LimitedThreadScheduler : TaskScheduler
{
    private readonly LinkedList<Task> _tasks = new();
    private int _runningTasks;


    public LimitedThreadScheduler(IParallelism parallelism)
    {
        MaximumConcurrencyLevel = parallelism.GetOptimalParallelism();
    }

    public override int MaximumConcurrencyLevel { get; }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        lock (_tasks)
        {
            return _tasks.ToList();
        }
    }

    protected override void QueueTask(Task task)
    {
        lock (_tasks)
        {
            _tasks.AddLast(task);
            TryExecuteNext();
        }
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        if (Interlocked.CompareExchange(ref _runningTasks, MaximumConcurrencyLevel, MaximumConcurrencyLevel) <
            MaximumConcurrencyLevel)
            return TryExecuteTask(task);

        return false;
    }

    private void TryExecuteNext()
    {
        lock (_tasks)
        {
            if (_runningTasks >= MaximumConcurrencyLevel || !_tasks.Any())
                return;

            var task = _tasks.First?.Value;
            _tasks.RemoveFirst();

            Interlocked.Increment(ref _runningTasks);

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    if (task != null)
                        TryExecuteTask(task);
                }
                finally
                {
                    Interlocked.Decrement(ref _runningTasks);
                    TryExecuteNext();
                }
            });
        }
    }
}
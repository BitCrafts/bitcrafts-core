using BitCrafts.Infrastructure.Abstraction.Threading;

namespace BitCrafts.Infrastructure.Threading;

public sealed class LimitedThreadScheduler : TaskScheduler
{
    private readonly LinkedList<Task> _tasks = new LinkedList<Task>();
    private readonly int _maxDegreeOfParallelism;
    private int _runningTasks = 0;


    public LimitedThreadScheduler(IParallelism parallelism)
    {
        _maxDegreeOfParallelism = parallelism.GetOptimalParallelism(false);
    }

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
        if (Interlocked.CompareExchange(ref _runningTasks, _maxDegreeOfParallelism, _maxDegreeOfParallelism) <
            _maxDegreeOfParallelism)
        {
            return TryExecuteTask(task);
        }

        return false;
    }

    private void TryExecuteNext()
    {
        lock (_tasks)
        {
            if (_runningTasks >= _maxDegreeOfParallelism || !_tasks.Any())
                return;

            Task task = _tasks.First?.Value;
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

    public override int MaximumConcurrencyLevel => _maxDegreeOfParallelism;
}
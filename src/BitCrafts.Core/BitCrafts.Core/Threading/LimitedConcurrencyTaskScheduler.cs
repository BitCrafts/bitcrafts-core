using BitCrafts.Core.Contracts.Services;

namespace BitCrafts.Core.Threading;

public class LimitedConcurrencyTaskScheduler : TaskScheduler
{
    private readonly LinkedList<Task> _tasks = new LinkedList<Task>();
    private readonly int _maxDegreeOfParallelism;
    private int _runningTasks = 0;


    public LimitedConcurrencyTaskScheduler(IParallelismService parallelismService)
    {
        _maxDegreeOfParallelism = parallelismService.GetOptimalParallelism(false);
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

            Task task = _tasks.First.Value;
            _tasks.RemoveFirst();

            Interlocked.Increment(ref _runningTasks);

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
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
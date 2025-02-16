using BitCrafts.Core.Contracts.Threading;

namespace BitCrafts.Core.Threading;

public sealed class NetworkTaskScheduler : SingleThreadTaskScheduler, INetworkTaskScheduler
{
    public NetworkTaskScheduler() :
        base("Network Thread")
    {
    }

    public Task ScheduleAsync(Action action)
    {
        return Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, this);
    }

    public Task<T> ScheduleAsync<T>(Func<T> function)
    {
        return Task.Factory.StartNew(function, CancellationToken.None, TaskCreationOptions.None, this);
    }
}
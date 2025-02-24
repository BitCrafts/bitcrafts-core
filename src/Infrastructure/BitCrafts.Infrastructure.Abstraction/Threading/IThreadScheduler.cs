namespace BitCrafts.Infrastructure.Abstraction.Threading;

public interface IThreadScheduler
{
    void Schedule(Action action);
    Task ScheduleAsync(Action action, CancellationToken cancellationToken = new());
    Task<T> ScheduleAsync<T>(Func<T> function, CancellationToken cancellationToken = new());
}
namespace BitCrafts.Infrastructure.Abstraction.Threading;

public interface IThreadScheduler
{
    Task ScheduleAsync(Action action, CancellationToken cancellationToken = new CancellationToken());
    Task<T> ScheduleAsync<T>(Func<T> function, CancellationToken cancellationToken = new CancellationToken());
}
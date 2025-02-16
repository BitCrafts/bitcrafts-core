namespace BitCrafts.Core.Contracts.Threading;

public interface ITaskScheduler
{
    Task ScheduleAsync(Action action);
    Task<T> ScheduleAsync<T>(Func<T> function);
}
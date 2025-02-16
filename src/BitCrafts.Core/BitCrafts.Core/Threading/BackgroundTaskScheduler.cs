using BitCrafts.Core.Contracts.Threading;
using Serilog;

namespace BitCrafts.Core.Threading;

public sealed class BackgroundTaskScheduler : SingleThreadTaskScheduler, IBackgroundTaskSceduler
{
    public BackgroundTaskScheduler()
        : base("Background Thread")
    {
    }

    public Task ScheduleAsync(Action action)
    {
        return Task.Factory.StartNew(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Erreur lors de l'exécution d'une tâche dans le scheduler.");
                throw;
            }
        }, CancellationToken.None, TaskCreationOptions.None, this);
    }

    public Task<T> ScheduleAsync<T>(Func<T> function)
    {
        return Task.Factory.StartNew(() =>
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, "Erreur lors de l'exécution d'une tâche dans le scheduler.");
                throw;
            }
        }, CancellationToken.None, TaskCreationOptions.None, this);
    }
}
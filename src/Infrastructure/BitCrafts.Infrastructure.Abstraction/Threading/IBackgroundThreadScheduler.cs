namespace BitCrafts.Infrastructure.Abstraction.Threading;

public interface IBackgroundThreadScheduler : IThreadScheduler
{
    void Start();
    void Stop();
}
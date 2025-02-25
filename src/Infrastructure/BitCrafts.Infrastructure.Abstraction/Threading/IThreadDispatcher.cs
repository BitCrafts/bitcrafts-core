namespace BitCrafts.Infrastructure.Abstraction.Threading;

public interface IThreadDispatcher
{
    void Start();
    void Stop();
    void Invoke(Action action);
    Task InvokeAsync(Action action);
    Task<T> InvokeAsync<T>(Func<T> func);
}
namespace BitCrafts.Infrastructure.Abstraction.UseCases;

public abstract class BaseUseCase<TInput> : IUseCase<TInput>
{
    public async Task Execute(TInput eventRequest)
    {
        await ExecuteCore(eventRequest);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected abstract Task ExecuteCore(TInput eventRequest);

    protected virtual void Dispose(bool disposing)
    {
    }
}

public abstract class BaseUseCase : IUseCase
{
    public async Task Execute()
    {
        await ExecuteCore();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected abstract Task ExecuteCore();

    protected virtual void Dispose(bool disposing)
    {
    }
}
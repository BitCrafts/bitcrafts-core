using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Infrastructure.Abstraction.UseCases;

public abstract class BaseUseCase<TInput, TOutput> : IUseCase<TInput, TOutput>
{
    public async Task<TOutput> Execute(TInput eventRequest)
    {
        var response = await ExecuteCore(eventRequest);
        return response;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected abstract Task<TOutput> ExecuteCore(TInput eventRequest);

    protected virtual void Dispose(bool disposing)
    {
    }
}
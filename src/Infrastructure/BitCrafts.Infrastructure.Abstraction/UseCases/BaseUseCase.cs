using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Infrastructure.Abstraction.UseCases;

public abstract class BaseUseCase<TEventRequest, TEventResponse> : IUseCase<TEventRequest, TEventResponse>
    where TEventRequest : IEventRequest
    where TEventResponse : IEventResponse
{
    public async Task<TEventResponse> Execute(TEventRequest eventRequest)
    {
        var response = await ExecuteCore(eventRequest);
        response.Request = eventRequest;
        return response;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected abstract Task<TEventResponse> ExecuteCore(TEventRequest eventRequest);

    protected virtual void Dispose(bool disposing)
    {
    }
}
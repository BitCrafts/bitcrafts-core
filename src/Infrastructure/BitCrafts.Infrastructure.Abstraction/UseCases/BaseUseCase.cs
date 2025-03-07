using BitCrafts.Infrastructure.Abstraction.Events;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Abstraction.UseCases;

public abstract class BaseUseCase<TEventRequest, TEventResponse> : IUseCase<TEventRequest, TEventResponse>
    where TEventRequest : IEventRequest
    where TEventResponse : IEventResponse
{
    private readonly IServiceProvider _provider;
    protected IServiceProvider ServiceProdiver => _provider;
    protected IEventAggregator EventAggregator { get; private set; }

    protected BaseUseCase(IServiceProvider provider)
    {
        _provider = provider;
        EventAggregator = _provider.GetRequiredService<IEventAggregator>();
    }


    public async Task<TEventResponse> ExecuteAsync(TEventRequest eventRequest)
    {
        var response = await ExecuteCoreAsync(eventRequest);
        response.Request = eventRequest;
        return response;
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected abstract Task<TEventResponse> ExecuteCoreAsync(TEventRequest eventRequest);

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) EventAggregator.Unsubscribe<TEventRequest>(ExecuteAsync);
    }
}
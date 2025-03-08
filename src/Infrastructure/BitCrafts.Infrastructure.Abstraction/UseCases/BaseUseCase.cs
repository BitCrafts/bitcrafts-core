using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Abstraction.UseCases;

public abstract class BaseUseCase<TEventRequest, TEventResponse> : IUseCase<TEventRequest, TEventResponse>
    where TEventRequest : IEventRequest
    where TEventResponse : IEventResponse
{
    private readonly IServiceProvider _provider;
    protected IEventAggregator EventAggregator { get; private set; }
    private IBackgroundThreadDispatcher BackgroundDispatcher { get; set; }

    protected BaseUseCase(IServiceProvider provider)
    {
        _provider = provider;
        EventAggregator = _provider.GetRequiredService<IEventAggregator>();
        BackgroundDispatcher = _provider.GetService<IBackgroundThreadDispatcher>();
    }


    public async Task<TEventResponse> Execute(TEventRequest eventRequest)
    {
        var response = await BackgroundDispatcher.InvokeTaskAsync(() => ExecuteCore(eventRequest));
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
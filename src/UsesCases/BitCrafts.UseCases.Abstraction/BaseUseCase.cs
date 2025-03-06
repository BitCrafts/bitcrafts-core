using BitCrafts.Infrastructure.Abstraction.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.UseCases.Abstraction;

public abstract class BaseUseCase<TEventRequest, TEventResponse> : IUseCase<TEventRequest, TEventResponse>
    where TEventRequest : IEventRequest
    where TEventResponse : IEventResponse
{
    private readonly IServiceProvider _provider;
    protected IServiceProvider ServiceProdiver => _provider;
    protected IEventAggregator EventAggregator { get; private set; }
    protected ILogger<BaseUseCase<TEventRequest, TEventResponse>> Logger { get; private set; }

    protected BaseUseCase(IServiceProvider provider)
    {
        _provider = provider;
        Logger = _provider.GetRequiredService<ILogger<BaseUseCase<TEventRequest, TEventResponse>>>();
        EventAggregator = _provider.GetRequiredService<IEventAggregator>();
    }


    public async Task ExecuteAsync(TEventRequest createEvent)
    {
        try
        {
            var response = await ExecuteCoreAsync(createEvent); 
            response.Request = createEvent;
            EventAggregator.Publish<TEventResponse>(response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"Error while executing use case {GetType().Name}");
        }
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected abstract Task<TEventResponse> ExecuteCoreAsync(TEventRequest createEvent);

    protected virtual void Dispose(bool disposing)
    {
        if (disposing) EventAggregator.Unsubscribe<TEventRequest>(ExecuteAsync);
    }
}
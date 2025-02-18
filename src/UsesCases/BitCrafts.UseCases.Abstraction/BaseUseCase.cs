using BitCrafts.Infrastructure.Abstraction.Events;
using Microsoft.Extensions.Logging;

namespace BitCrafts.UseCases.Abstraction;

public abstract class BaseUseCase<TEventRequest, TEventResponse> : IUseCase<TEventRequest, TEventResponse>
    where TEventRequest : IEventRequest
    where TEventResponse : IEventResponse
{
    private IEventAggregator EventAggregator { get; set; }
    protected ILogger<BaseUseCase<TEventRequest, TEventResponse>> Logger { get; private set; }

    protected BaseUseCase(ILogger<BaseUseCase<TEventRequest, TEventResponse>> logger, IEventAggregator eventAggregator)
    {
        Logger = logger;
        EventAggregator = eventAggregator;
        EventAggregator.Subscribe<TEventRequest>(ExecuteAsync);
    }

    public async Task ExecuteAsync(TEventRequest createEvent)
    {
        try
        {
            await ExecuteCoreAsync(createEvent);
            var response = Activator.CreateInstance<TEventResponse>();
            response.Request = createEvent;
            EventAggregator.Publish<TEventResponse>(response);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"Error while executing use case {this.GetType().Name}");
        }
    }

    protected abstract Task ExecuteCoreAsync(TEventRequest createEvent);

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            EventAggregator.Unsubscribe<TEventRequest>(ExecuteAsync);
        }
    }
 

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
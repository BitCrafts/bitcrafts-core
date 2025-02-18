using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.UseCases.Abstraction;

public interface IUseCase<TEventRequest, TEventResponse> : IDisposable
    where TEventRequest : IEventRequest
    where TEventResponse : IEventResponse
{
    Task ExecuteAsync(TEventRequest eventRequest);
}
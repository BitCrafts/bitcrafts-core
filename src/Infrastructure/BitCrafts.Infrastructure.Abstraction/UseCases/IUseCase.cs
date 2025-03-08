using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Infrastructure.Abstraction.UseCases;

public interface IUseCase<TEventRequest, TEventResponse> : IDisposable
    where TEventRequest : IEventRequest
    where TEventResponse : IEventResponse
{
    Task<TEventResponse> Execute(TEventRequest eventRequest);
}
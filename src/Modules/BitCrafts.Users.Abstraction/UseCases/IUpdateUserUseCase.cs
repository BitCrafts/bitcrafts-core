using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Users.Abstraction.UseCases;

public interface IUpdateUserUseCase<TRequest, TResponse>
    where TRequest : class, IEventRequest
    where TResponse : class, IEventResponse
{
    Task ExecuteAsync(TRequest request);
}
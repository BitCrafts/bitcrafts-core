using BitCrafts.Core.Contracts.Events;

namespace BitCrafts.Core.Contracts.Services;

public interface IEventAggregatorService
{
    void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent;
    void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
}
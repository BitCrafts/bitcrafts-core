namespace BitCrafts.Infrastructure.Abstraction.Events;

public interface IEventAggregator
{
    void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;
    void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IEvent;
    void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
}
namespace BitCrafts.Infrastructure.Abstraction.Events;

public interface IEventAggregator
{
    void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent;
    void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    void Unsubscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IEvent;
}
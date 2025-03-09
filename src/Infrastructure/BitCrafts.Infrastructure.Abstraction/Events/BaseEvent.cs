namespace BitCrafts.Infrastructure.Abstraction.Events;

public abstract class BaseEvent : IEvent
{
    public DateTime Timestamp { get; protected set; } = DateTime.Now;
}
namespace BitCrafts.Core.Contracts.Events;

public abstract class BaseEvent : IEvent
{
    public DateTime Timestamp { get; set; } = DateTime.Now;
}
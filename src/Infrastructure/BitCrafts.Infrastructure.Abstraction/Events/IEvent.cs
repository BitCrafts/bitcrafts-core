namespace BitCrafts.Infrastructure.Abstraction.Events;

public interface IEvent
{
    DateTime Timestamp { get; }
}
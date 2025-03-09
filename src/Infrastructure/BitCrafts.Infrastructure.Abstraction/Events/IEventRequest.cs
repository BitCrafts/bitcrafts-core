namespace BitCrafts.Infrastructure.Abstraction.Events;

public interface IEventRequest : IEvent
{
    IEventResponse Response { get; set; }
}
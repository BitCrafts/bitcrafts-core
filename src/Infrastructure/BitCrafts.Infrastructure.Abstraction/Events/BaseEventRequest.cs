namespace BitCrafts.Infrastructure.Abstraction.Events;

public abstract class BaseEventRequest : BaseEvent, IEventRequest
{
    public IEventResponse Response { get; set; }
}
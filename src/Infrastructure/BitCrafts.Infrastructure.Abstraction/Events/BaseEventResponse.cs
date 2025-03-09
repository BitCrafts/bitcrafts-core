namespace BitCrafts.Infrastructure.Abstraction.Events;

public abstract class BaseEventResponse : BaseEvent, IEventResponse
{
    public IEventRequest Request { get; set; }
}
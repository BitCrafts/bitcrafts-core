namespace BitCrafts.Infrastructure.Abstraction.Events;

public interface IEventResponse : IEvent
{
    IEventRequest Request { get; set; }
}
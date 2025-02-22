using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Users.Abstraction.Events;

public class UserDeleteEventResponse : BaseEventResponse, IEventResponse
{
    public bool Deleted { get; set; }
}
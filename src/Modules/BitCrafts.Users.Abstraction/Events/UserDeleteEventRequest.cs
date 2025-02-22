using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Users.Abstraction.Events;

public class UserDeleteEventRequest : BaseEventRequest,IEventRequest
{
    public int UserId { get; set; }
}
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class UserCreateEventRequest : BaseEventRequest,IEventRequest
{
    public IUser User { get; set; }
}
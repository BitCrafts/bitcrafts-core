using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class UserUpdateEventRequest : BaseEventRequest, IEventRequest
{
    public IUser User { get; set; }
}
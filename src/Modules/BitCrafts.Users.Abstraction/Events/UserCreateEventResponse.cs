using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class UserCreateEventResponse :   BaseEventResponse,IEventResponse
{
    public IUser User { get; set; }
}
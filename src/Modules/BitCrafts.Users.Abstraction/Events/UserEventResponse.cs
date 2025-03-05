using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class UserEventResponse : BaseEventResponse, IEventResponse
{
    public User User { get; set; }
}
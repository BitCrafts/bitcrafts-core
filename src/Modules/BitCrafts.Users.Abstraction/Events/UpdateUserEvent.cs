using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class UpdateUserEvent : BaseEvent
{
    public User User { get; private set; }

    public UpdateUserEvent(User user)
    {
        User = user;
    }
}
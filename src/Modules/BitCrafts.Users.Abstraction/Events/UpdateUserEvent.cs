using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class UpdateUserEvent : BaseEvent
{
    public UpdateUserEvent(User user, bool updated)
    {
        User = user;
        Updated = updated;
    }

    public User User { get; private set; }
    public bool Updated { get; set; }
}
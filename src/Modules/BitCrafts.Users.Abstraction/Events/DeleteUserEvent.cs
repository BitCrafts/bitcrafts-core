using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class DeleteUserEvent : BaseEvent
{
    public User User { get; set; }

    public DeleteUserEvent(User user)
    {
        User = user;
    }
}
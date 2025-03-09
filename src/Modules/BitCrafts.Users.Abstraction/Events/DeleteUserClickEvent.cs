using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class DeleteUserClickEvent : BaseEvent
{
    public DeleteUserClickEvent(User user)
    {
        User = user;
    }

    public User User { get; set; }
}
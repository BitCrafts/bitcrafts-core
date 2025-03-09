using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class UpdateUserClickEvent : BaseEvent
{
    public UpdateUserClickEvent(User user)
    {
        User = user;
    }

    public User User { get; set; }
}
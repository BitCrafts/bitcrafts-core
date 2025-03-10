using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Users.Abstraction.Events;

public class DeleteUserEvent : BaseEvent
{
    public DeleteUserEvent(int userId, bool deleted)
    {
        UserId = userId;
        Deleted = deleted;
    }

    public int UserId { get; set; }
    public bool Deleted { get; set; }
}
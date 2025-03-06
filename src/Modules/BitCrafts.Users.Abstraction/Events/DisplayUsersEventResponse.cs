using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class DisplayUsersEventResponse : BaseEventResponse
{
    public IEnumerable<User> Users { get; private set; }

    public DisplayUsersEventResponse(IEnumerable<User> users)
    {
        Users = users;
    }
}
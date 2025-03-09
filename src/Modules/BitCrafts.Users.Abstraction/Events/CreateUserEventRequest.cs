using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class CreateUserEventRequest : BaseEventRequest
{
    public User User { get; set; }
    public string Password { get; set; }
}
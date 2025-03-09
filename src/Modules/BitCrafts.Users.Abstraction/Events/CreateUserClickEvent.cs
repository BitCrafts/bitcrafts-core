using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Events;

public class CreateUserClickEvent : BaseEvent
{
    public CreateUserClickEvent(User user, string password)
    {
        User = user;
        Password = password;
    }

    public User User { get; set; }
    public string Password { get; set; }
}
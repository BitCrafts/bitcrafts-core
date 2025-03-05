using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Entities;

public class UserAccount : BaseEntity, IEntity
{
    public int UserId { get; set; }
    public string HashedPassword { get; set; }
    public string PasswordSalt { get; set; }
}
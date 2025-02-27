using BitCrafts.Infrastructure.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Entities;

public class UserAccount : BaseEntity<int>, IUserAccount
{
    public int UserId { get; set; }
    public string HashedPassword { get; set; }
    public string PasswordSalt { get; set; }
}
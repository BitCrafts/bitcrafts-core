using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Entities;

public interface IUserAccount : IEntity<int>
{
    int UserId { get; set; }
    string HashedPassword { get; set; }
    string PasswordSalt { get; set; }
}
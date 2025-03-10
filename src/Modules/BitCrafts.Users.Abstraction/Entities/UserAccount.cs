using System.ComponentModel.DataAnnotations;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Entities;

public class UserAccount : BaseEntity
{
    public int UserId { get; set; }

    [MaxLength(255)] public string HashedPassword { get; set; }

    [MaxLength(255)] public string PasswordSalt { get; set; }
}
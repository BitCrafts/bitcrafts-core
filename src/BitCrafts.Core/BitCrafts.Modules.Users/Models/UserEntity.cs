using System.ComponentModel.DataAnnotations;
using BitCrafts.Modules.Users.Contracts.Models;

namespace BitCrafts.Modules.Users.Models;

public class UserEntity: IUserEntity
{
    [Key]
    public int PrimaryKey { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
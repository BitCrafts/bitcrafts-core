using BitCrafts.Modules.Users.Contracts.Models;

namespace BitCrafts.Modules.Users.Models;

public class UserModel : IUserModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
using BitCrafts.Infrastructure.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Entities;

public class User : BaseEntity<int>, IUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string NationalNumber { get; set; }
    public string PassportNumber { get; set; }
}
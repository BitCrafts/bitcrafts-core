using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Entities;

public class User : BaseEntity, IEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string NationalNumber { get; set; }
    public string PassportNumber { get; set; }

    public UserAccount UserAccount { get; set; }

    public User()
    {
        UserAccount = new UserAccount();
    }
}
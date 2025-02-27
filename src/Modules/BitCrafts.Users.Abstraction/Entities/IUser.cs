using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Entities;

public interface IUser : IEntity<int>
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; } 
    string PhoneNumber { get; set; }
    DateTime BirthDate { get; set; }
    string NationalNumber { get; set; }
    string PassportNumber { get; set; }
}
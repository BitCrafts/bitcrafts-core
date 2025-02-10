namespace BitCrafts.Modules.Users.Contracts.Models;

public interface IUserModel
{
    int Id { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string Phone { get; set; }
}
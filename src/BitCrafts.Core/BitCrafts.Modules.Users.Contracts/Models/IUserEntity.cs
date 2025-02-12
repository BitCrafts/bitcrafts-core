using BitCrafts.Core.Contracts.Entities;

namespace BitCrafts.Modules.Users.Contracts.Models;

public interface IUserEntity : IEntity<int>
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string Phone { get; set; }
}
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Customers.Abstraction.Entities;

public interface ICustomer : IAuditableEntity, IEntity<int>
{
    string FirstName { get; set; }
    string LastName { get; set; }
    string Email { get; set; }
    string Phone { get; set; }

    int GroupId { get; set; }
}
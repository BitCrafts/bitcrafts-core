using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Customers.Entities;

public class Customer : BaseAuditableEntity<int>, ICustomer
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public int GroupId { get; set; }
}
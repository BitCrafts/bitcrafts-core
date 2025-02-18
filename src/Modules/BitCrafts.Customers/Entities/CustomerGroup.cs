using BitCrafts.Customers.Abstraction.Entities;
using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Customers.Entities;

public class CustomerGroup : BaseAuditableEntity<int>, ICustomerGroup
{
    public string Name { get; set; } = string.Empty;
}
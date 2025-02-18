using BitCrafts.Infrastructure.Abstraction.Entities;

namespace BitCrafts.Customers.Abstraction.Entities;

public interface ICustomerGroup : IAuditableEntity, IEntity<int>
{
    string Name { get; set; }
}
using BitCrafts.Infrastructure.Abstraction.Attributes;

namespace BitCrafts.Infrastructure.Abstraction.Entities;

public abstract class BaseEntity<T> : IEntity<T>
{
    [PrimaryKey(true)] public T Id { get; set; }
}
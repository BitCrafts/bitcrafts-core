using BitCrafts.Core.Contracts.Attributes;

namespace BitCrafts.Core.Contracts.Entities;

public abstract class BaseEntity<T> : IEntity<T>
{
    [PrimaryKey(true)] public T Id { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace BitCrafts.Infrastructure.Abstraction.Entities;

public abstract class BaseEntity<T> : IEntity<T>
{
    [Key] public T Id { get; set; }
}

public abstract class BaseEntity : IEntity
{
     [Key] public int Id { get; set; }
}
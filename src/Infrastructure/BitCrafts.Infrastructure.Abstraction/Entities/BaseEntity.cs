using System.ComponentModel.DataAnnotations;
using BitCrafts.Infrastructure.Abstraction.Attributes;

namespace BitCrafts.Infrastructure.Abstraction.Entities;

public abstract class BaseEntity<T> : IEntity<T>
{
    [PrimaryKey(true)] [Key] public T Id { get; set; }
}
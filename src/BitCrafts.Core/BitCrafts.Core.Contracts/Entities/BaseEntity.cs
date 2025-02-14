using System.ComponentModel.DataAnnotations;

namespace BitCrafts.Core.Contracts.Entities;

public abstract class BaseEntity<T> : IEntity<T>
{
    [Key] public T Id { get; set; }
}
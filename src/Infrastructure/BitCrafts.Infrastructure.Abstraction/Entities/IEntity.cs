using System.ComponentModel.DataAnnotations;

namespace BitCrafts.Infrastructure.Abstraction.Entities;

public interface IEntity
{
    [Key] int Id { get; set; }
}

public interface IEntity<T>
{
    [Key] T Id { get; set; }
}
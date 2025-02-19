using System.ComponentModel.DataAnnotations;
using BitCrafts.Infrastructure.Abstraction.Attributes;

namespace BitCrafts.Infrastructure.Abstraction.Entities;

public interface IEntity<T>
{
    [PrimaryKey(true)] [Key] T Id { get; set; }
}
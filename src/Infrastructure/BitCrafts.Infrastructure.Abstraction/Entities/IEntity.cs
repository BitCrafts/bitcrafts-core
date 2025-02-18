namespace BitCrafts.Infrastructure.Abstraction.Entities;

public interface IEntity<T>
{
    T Id { get; set; }
}
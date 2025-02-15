namespace BitCrafts.Core.Contracts.Entities;

public interface IEntity<T>
{
    T Id { get; set; }
}
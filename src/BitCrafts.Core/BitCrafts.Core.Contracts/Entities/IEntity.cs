namespace BitCrafts.Core.Contracts.Entities;

public interface IEntity<T>
{
    T PrimaryKey { get; set; }
    byte[] RowVersion { get; set; }

}

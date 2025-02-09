using System.ComponentModel.DataAnnotations;
using BitCrafts.Core.Contracts.Entities;

namespace BitCrafts.Core.Entities;

public abstract class BaseEntity<T> : IEntity<T>, IAuditableEntity, ISoftDeletableEntity
{
    [Key] public T PrimaryKey { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = string.Empty;
    public string UpdatedBy { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedBy { get; set; } = string.Empty;
    public string DeletedReason { get; set; } = string.Empty;
    [Timestamp] public byte[] RowVersion { get; set; }
}
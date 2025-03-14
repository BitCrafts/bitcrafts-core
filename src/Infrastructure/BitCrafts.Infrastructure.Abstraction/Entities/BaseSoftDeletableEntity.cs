namespace BitCrafts.Infrastructure.Abstraction.Entities;

public abstract class BaseSoftDeletableEntity<T> : BaseEntity<T>, ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedBy { get; set; }
    public string DeletedReason { get; set; }
}

public abstract class BaseSoftDeletableEntity : BaseEntity, ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string DeletedBy { get; set; }
    public string DeletedReason { get; set; }
}
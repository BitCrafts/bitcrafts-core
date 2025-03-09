namespace BitCrafts.Infrastructure.Abstraction.Entities;

public interface ISoftDeletableEntity
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }
    string DeletedBy { get; set; }
    string DeletedReason { get; set; }
}
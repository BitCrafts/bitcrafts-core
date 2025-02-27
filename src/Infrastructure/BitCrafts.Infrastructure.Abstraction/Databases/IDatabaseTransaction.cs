using System.Data.Common;

namespace BitCrafts.Infrastructure.Abstraction.Databases;

public interface IDatabaseTransaction : IDisposable
{
    DbTransaction DbTransaction { get; }
    Task CommitAsync();
    Task RollbackAsync();
}

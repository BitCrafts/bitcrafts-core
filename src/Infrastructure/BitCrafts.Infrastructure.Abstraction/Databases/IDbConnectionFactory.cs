using System.Data;

namespace BitCrafts.Infrastructure.Abstraction.Databases;

public interface IDbConnectionFactory
{
    bool IsSqliteProvider { get; }
    bool IsMemoryProvider { get; }

    IDbConnection Create();
}
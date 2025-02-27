using System.Data;

namespace BitCrafts.Infrastructure.Abstraction.Databases;

public interface IDbConnectionFactory
{
    bool IsSqliteProvider { get; }
    bool IsMemoryProvider { get; }
    bool IsMySqlProvider { get; }
    bool IsMariaDbProvider { get; }

    IDbConnection Create();
}